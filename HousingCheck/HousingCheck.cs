using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Linq;
using System.Text;
using Advanced_Combat_Tracker;
using FFXIV_ACT_Plugin.Common;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class Extensions
{
    public static T[] SubArray<T>(this T[] array, int offset, int length)
    {
        T[] result = new T[length];
        Array.Copy(array, offset, result, 0, length);
        return result;
    }
}

namespace HousingCheck
{
    public enum ApiVersion { V1, V2 }

    public class HousingCheck : IActPluginV1
    {
        public const int OPCODE = 894;
        /// <summary>
        /// 房屋列表，用于和控件双向绑定
        /// </summary>
        public ObservableCollection<HousingOnSaleItem> HousingList = new ObservableCollection<HousingOnSaleItem>();
        
        /// <summary>
        /// 库啵，库啵啵？
        /// </summary>
        public BindingSource housingBindingSource;

        /// <summary>
        /// 插件对象
        /// </summary>
        FFXIV_ACT_Plugin.FFXIV_ACT_Plugin ffxivPlugin;

        List<(DateTime, string)> LogQueue = new List<(DateTime, string)>();

        /// <summary>
        /// 是否加载完成
        /// </summary>
        bool initialized = false;

        /// <summary>
        /// 有手动上报任务
        /// </summary>
        bool ManualUpload = false;

        /// <summary>
        /// 有自动上报任务
        /// </summary>
        bool HousingListUpdated = false;

        /// <summary>
        /// 房区快照
        /// </summary>
        HousingSnapshotStorage SnapshotStorage = new HousingSnapshotStorage();

        /// <summary>
        /// 进行房区快照上报用的存储
        /// </summary>
        Dictionary<Tuple<HouseArea, int>, HousingSlotSnapshot> WillUploadSnapshot = new Dictionary<Tuple<HouseArea, int>, HousingSlotSnapshot>();
        
        /// <summary>
        /// 用户上次操作的时间
        /// </summary>
        long LastOperateTime = 0;

        /// <summary>
        /// 无操作自动保存的时间
        /// </summary>
        long AutoSaveAfter = 20;

        /// <summary>
        /// 自动保存worker
        /// </summary>
        private BackgroundWorker AutoSaveThread;

        /// <summary>
        /// Log队列
        /// </summary>
        private BackgroundWorker LogQueueWorker;

        /// <summary>
        /// 状态信息
        /// </summary>
        Label statusLabel;
        PluginControl control; 

        private object GetFfxivPlugin()
        {
            ffxivPlugin = null;

            var plugins = ActGlobals.oFormActMain.ActPlugins;
            
            foreach (var plugin in plugins)
                if (plugin.pluginFile.Name.ToUpper().Contains("FFXIV_ACT_Plugin".ToUpper()) &&
                    plugin.lblPluginStatus.Text.ToUpper().Contains("FFXIV Plugin Started.".ToUpper()))
                    ffxivPlugin = (FFXIV_ACT_Plugin.FFXIV_ACT_Plugin)plugin.pluginObj;

            if (ffxivPlugin == null)
                throw new Exception("Could not find FFXIV plugin. Make sure that it is loaded before CutsceneSkip.");

            return ffxivPlugin;
        }

        public void DeInitPlugin()
        {
            if (initialized)
            {
                var subs = ffxivPlugin.GetType().GetProperty("DataSubscription").GetValue(ffxivPlugin, null);
                var networkReceivedDelegateType = typeof(NetworkReceivedDelegate);
                var networkReceivedDelegate = Delegate.CreateDelegate(networkReceivedDelegateType, (object)this, "NetworkReceived", true);
                subs.GetType().GetEvent("NetworkReceived").RemoveEventHandler(subs, networkReceivedDelegate);
                AutoSaveThread.CancelAsync();
                LogQueueWorker.CancelAsync();
                control.SaveSettings();
                SaveHousingList();
                statusLabel.Text = "Exit :|";
            }
            else
            {
                statusLabel.Text = "Error :(";
            }
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            statusLabel = pluginStatusText;
            GetFfxivPlugin();
            control = new PluginControl();
            pluginScreenSpace.Text = "房屋信息记录";
            housingBindingSource = new BindingSource { DataSource = HousingList };
            control.dataGridView1.DataSource = housingBindingSource;
            control.dataGridView1.UserDeletedRow += OnTableUpdated;
            foreach(DataGridViewColumn col in control.dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            control.Dock = DockStyle.Fill;
            pluginScreenSpace.Controls.Add(control);

            var subs = ffxivPlugin.GetType().GetProperty("DataSubscription").GetValue(ffxivPlugin, null);
            var networkReceivedDelegateType = typeof(NetworkReceivedDelegate);
            var networkReceivedDelegate = Delegate.CreateDelegate(networkReceivedDelegateType, (object)this, "NetworkReceived", true);
            subs.GetType().GetEvent("NetworkReceived").AddEventHandler(subs, networkReceivedDelegate);
            initialized = true;
            
            AutoSaveThread = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            AutoSaveThread.DoWork += RunAutoUploadWorker;
            AutoSaveThread.RunWorkerAsync();

            LogQueueWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            LogQueueWorker.DoWork += RunLogQueueWorker;
            LogQueueWorker.RunWorkerAsync();

            statusLabel.Text = "Working :D";
            control.LoadSettings();
            control.buttonUploadOnce.Click += ButtonUploadOnce_Click;
            control.buttonCopyToClipboard.Click += ButtonCopyToClipboard_Click;
            control.buttonSaveToFile.Click += ButtonSaveToFile_Click;
            PrepareDir();
            //恢复上次列表
            LoadHousingList();
        }

        /// <summary>
        /// 播放提示音
        /// </summary>
        void PlayAlert()
        {
            Console.Beep(3000, 1000);
        }

        int GetServerId()
        {
            return (int)ffxivPlugin.DataRepository.GetCombatantList()
                .FirstOrDefault(x => x.ID == ffxivPlugin.DataRepository.GetCurrentPlayerID()).CurrentWorldID;
        }

        void Log(string type, string message, bool important = false)
        {
            var time = (DateTime.Now).ToString("HH:mm:ss");
            var text = $"[{time}] [{type}] {message.Trim()}";
            control.textBoxLog.Text += text + Environment.NewLine;
            control.textBoxLog.SelectionStart = control.textBoxLog.TextLength;
            control.textBoxLog.ScrollToCaret(); 
            text = $"00|{DateTime.Now.ToString("O")}|0|HousingCheck-{message}|";        //解析插件数据格式化
            //ActGlobals.oFormActMain.ParseRawLogLine(true, DateTime.Now, $"{text}");
            if (important)
            {
                LogQueue.Add((DateTime.Now, text));
            }
        }

        void Log(string type, Exception ex, string msg = "")
        {
            Log(type, msg + ex.ToString(), false);
        }

        string HousingListToJson()
        {
            return JsonConvert.SerializeObject(
                    HousingList.Where(x => x.CurrentStatus).ToArray()
                );
        }

        void NetworkReceived(string connection, long epoch, byte[] message)
        {
            var opcode = BitConverter.ToUInt16(message, 18);
            //if (message.Length == 2440) Log("Debug", "opcode=" + opcode);
            if (opcode != OPCODE || message.Length != 2440) return;

            HousingSlotSnapshot snapshot;
            List<HousingOnSaleItem> updatedHousingList = new List<HousingOnSaleItem>();
            try
            {
                //解析数据包
                snapshot = new HousingSlotSnapshot(message);
                snapshot.ServerId = GetServerId();
                //存入存储
                SnapshotStorage.Insert(snapshot);
                WillUploadSnapshot[new Tuple<HouseArea, int>(snapshot.Area, snapshot.Slot)] = snapshot;

                //本区房屋列表
                var housingList = snapshot.HouseList;

                //本区原先在售的房屋列表
                List<HousingOnSaleItem> oldOnSaleList = new List<HousingOnSaleItem>();
                foreach(HousingOnSaleItem housing in HousingList)
                {
                    if (housing.Area == snapshot.Area && housing.Slot == snapshot.Slot)
                    {
                        oldOnSaleList.Add(housing);
                    }
                }

                foreach (var a in housingList)
                {
                    HousingItem house = a.Value;
                    HousingOnSaleItem onSaleItem = new HousingOnSaleItem(house);
                    bool isExists = false;

                    //查找并更新原有房屋
                    var oldOnSaleItems = oldOnSaleList.Where(x => x.Id == house.Id);
                    foreach(var oldOnSaleItem in oldOnSaleItems)
                    {
                        updatedHousingList.Add(onSaleItem);
                        isExists = true;
                    }

                    if (isExists)
                    {
                        HousingListUpdated = true;
                    }

                    if (house.IsEmpty)
                    {
                        Log("Info", string.Format("{0} 第{1}区 {2}号 {3}房在售 当前价格: {4}",
                            onSaleItem.AreaStr, onSaleItem.DisplaySlot, onSaleItem.DisplayId,
                            onSaleItem.SizeStr, onSaleItem.Price), true);

                        if (!isExists)
                        {
                            if (onSaleItem.Size == HouseSize.M || onSaleItem.Size == HouseSize.L)
                            {
                                PlayAlert();
                            }

                            updatedHousingList.Add(onSaleItem);
                            HousingListUpdated = true;
                        }
                        else
                        {
                            Log("Info", "重复土地，已更新。");
                        }
                    }
                }

                LastOperateTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds(); //更新上次操作的时间

                Log("Info", string.Format("{0} 第{1}区查询完成",
                    HousingItem.GetHouseAreaStr(snapshot.Area),
                    snapshot.Slot + 1), true);     //输出翻页日志

                //刷新UI
                control.Invoke(new Action<List<HousingOnSaleItem>>(UpdateTable), updatedHousingList);
            }
            catch (Exception ex)
            {
                Log("Error", ex, "查询房屋列表出错：");
                return;
            }
        }

        private void ButtonUploadOnce_Click(object sender, EventArgs e)
        {
            Log("Info", $"准备上报");
            ManualUpload = true;
        }

        private void ButtonCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ListToString());
            Log("Info", $"复制成功");
        }

        private void PrepareDir()
        {
            string appdataPath = Path.Combine(Environment.CurrentDirectory, "AppData");
            if (!Directory.Exists(appdataPath)) Directory.CreateDirectory(appdataPath);
            string savingPath = Path.Combine(appdataPath, "HousingCheck");
            if (!Directory.Exists(savingPath)) Directory.CreateDirectory(savingPath);
            string snapshotPath = Path.Combine(savingPath, "snapshots");
            if (!Directory.Exists(snapshotPath)) Directory.CreateDirectory(snapshotPath);
        }

        private void SaveHousingList()
        {
            string savePath = Path.Combine(new string[] { Environment.CurrentDirectory, "AppData", "HousingCheck", "list.json" });

            StreamWriter writer = new StreamWriter(savePath, false, Encoding.UTF8);
            writer.Write(HousingListToJson());
            writer.Close();
            Log("Info", $"房屋列表已保存到{savePath}");
        }

        private void LoadHousingList()
        {
            string savePath = Path.Combine(new string[] { Environment.CurrentDirectory, "AppData", "HousingCheck", "list.json" });
            if (!File.Exists(savePath)) return;
            StreamReader reader = new StreamReader(savePath, Encoding.UTF8);
            string jsonStr = reader.ReadToEnd();
            reader.Close();
            try
            {
                var list = JsonConvert.DeserializeObject<HousingOnSaleItem[]>(jsonStr);
                foreach(var item in list)
                {
                    housingBindingSource.Add(item);
                }
                Log("Info", "已恢复上次保存的房屋列表");
            }
            catch(Exception ex)
            {
                Log("Error", ex, "恢复上次保存的房屋列表失败：");
            }
            reader.Close();
        }

        private void ButtonSaveToFile_Click(object sender, EventArgs e)
        {
            PrepareDir();
            string fileName = $"HousingCheck-{DateTime.Now.ToString("u").Replace(":", "").Replace(" ", "").Replace("-", "")}.csv";
            string savePath = Path.Combine(new string[] { Environment.CurrentDirectory, "AppData", "HousingCheck", "snapshots", fileName });

            StreamWriter writer = new StreamWriter(savePath, false, Encoding.UTF8);
            SnapshotStorage.SaveCsv(writer);
            writer.Close();
            Log("Info", $"已保存到{savePath}");
            //Log("Debug", fileName);
        }

        private string ListToString()
        {
            ArrayList area = new ArrayList(new string[] { "海雾村", "薰衣草苗圃", "高脚孤丘", "白银乡" });
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var line in HousingList)
            {
                if (!line.CurrentStatus) 
                    continue;

                stringBuilder.Append($"{line.AreaStr} 第{line.DisplaySlot}区 {line.DisplayId}号{line.SizeStr}房在售，当前价格:{line.Price} {Environment.NewLine}");

                if (line.Area == HouseArea.海雾村 && area.IndexOf("海雾村") != -1)
                {
                    area.Remove("海雾村");
                }
                else if (line.Area == HouseArea.薰衣草苗圃 && area.IndexOf("薰衣草苗圃") != -1)
                {
                    area.Remove("薰衣草苗圃");
                }
                else if (line.Area == HouseArea.高脚孤丘 && area.IndexOf("高脚孤丘") != -1)
                {
                    area.Remove("高脚孤丘");
                }
                else if (line.Area == HouseArea.白银乡 && area.IndexOf("白银乡") != -1)
                {
                    area.Remove("白银乡");
                }
            }
            foreach (var line in area)
            {
                stringBuilder.Append($"{line} 无空房 {Environment.NewLine}");
            }

            return stringBuilder.ToString();
        }

        public void UpdateTable(List<HousingOnSaleItem> items)
        {
            foreach(HousingOnSaleItem item in items)
            {
                int listIndex;
                if((listIndex = HousingList.IndexOf(item)) != -1){
                    housingBindingSource[listIndex] = item;
                }
                else
                {
                    housingBindingSource.Add(item);
                }
            }
        }

        private void OnTableUpdated(object sender, DataGridViewRowEventArgs e)
        {
            HousingListUpdated = true;
            LastOperateTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds(); //更新上次操作的时间
        }

        private void RunLogQueueWorker(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (AutoSaveThread.CancellationPending)
                {
                    break;
                }
                if (LogQueue.Count > 0)
                {
                    while(LogQueue.Count > 0)
                    {
                        var data = LogQueue.First();
                        LogQueue.RemoveAt(0);
                        ActGlobals.oFormActMain.ParseRawLogLine(false, data.Item1, data.Item2);
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        private void RunAutoUploadWorker(object sender, DoWorkEventArgs e)
        {
            long actionTime;
            bool dataUpdated;
            int snapshotCount;
            bool autoUpload;
            bool manualUpload;
            bool uploadSnapshot;
            ApiVersion apiVersion;
            while (true)
            {
                if (AutoSaveThread.CancellationPending)
                {
                    break;
                }
                actionTime = LastOperateTime + AutoSaveAfter;
                dataUpdated = HousingListUpdated;
                snapshotCount = WillUploadSnapshot.Count;
                autoUpload = control.upload;
                manualUpload = ManualUpload;
                uploadSnapshot = control.EnableUploadSnapshot;
                apiVersion = control.UploadApiVersion;

                if (manualUpload)
                {
                    UploadOnSaleList(apiVersion);
                    HousingListUpdated = false;
                    if (apiVersion == ApiVersion.V2 && uploadSnapshot && snapshotCount > 0)
                    {
                        UploadSnapshot();
                    }
                    ManualUpload = false;
                }
                else if (actionTime <= new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds())
                {
                    if (dataUpdated || snapshotCount > 0) //如果有更新
                    {
                        if (dataUpdated)
                        {
                            //保存列表文件
                            SaveHousingList();
                            Log("Info", "房屋信息已保存");
                            if (autoUpload)
                            {
                                //自动上报
                                UploadOnSaleList(apiVersion);
                            }
                            HousingListUpdated = false;
                        }

                        if (autoUpload && apiVersion == ApiVersion.V2 && 
                            uploadSnapshot && snapshotCount > 0)
                        {
                            //上传快照
                            UploadSnapshot();
                        }
                    }
                }
                Thread.Sleep(500);
            }
        }

        public bool UploadData(string type, string postContent, string mime = "application/json")
        {
            var wb = new WebClient();
            var token = control.UploadToken.Trim();
            if (token != "")
            {
                wb.Headers[HttpRequestHeader.Authorization] = "Token " + token;
            }
            wb.Headers[HttpRequestHeader.ContentType] = mime;

            string url;
            switch (control.UploadApiVersion)
            {
                case ApiVersion.V1:
                    url = control.UploadUrl;
                    break;
                case ApiVersion.V2:
                default:
                    url = control.UploadUrl.TrimEnd('/') + "/" + type;
                    break;
            }
            try
            {
                var response = wb.UploadData(url, "POST",
                    Encoding.UTF8.GetBytes(postContent)
                );
                string res = Encoding.UTF8.GetString(response);
                if(res.Length > 0)
                {
                    if(res[0] == '{')
                    {
                        var jsonRes = JsonConvert.DeserializeObject<Dictionary<string, string>>(res);
                        if(jsonRes["statusText"].ToLower() == "ok")
                        {
                            return true;
                        } 
                        else
                        {
                            Log("Error", "上传出错：" + jsonRes["errorMessage"]);
                        }
                    } 
                    else
                    {
                        if(res.ToLower() == "ok")
                        {
                            return true;
                        }
                        else
                        {
                            Log("Error", "上传出错：" + res);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log("Error", ex, "上传出错：");
            }
            return false;
        }

        private void UploadOnSaleList(ApiVersion apiVersion = ApiVersion.V2)
        {
            string postContent = "";
            string mime = "application/json";
            switch (apiVersion)
            {
                case ApiVersion.V2:
                    postContent = JsonConvert.SerializeObject(HousingList);
                    break;
                case ApiVersion.V1:
                    postContent = "text=" + WebUtility.UrlEncode(ListToString());
                    mime = "application/x-www-form-urlencoded";
                    break;
            }
            if(postContent.Length == 0)
            {
                Log("Error", "上报数据为空");
            }
            //Log("Debug", postContent);
            Log("Info", "正在上传空房列表");
            bool res = UploadData("info", postContent, mime);

            if (res)
            {
                Log("Info", "房屋列表上报成功");
            }
            else
            {
                Thread.Sleep(2000);
            }
        }

        private void UploadSnapshot()
        {
            try
            {
                List<HousingSlotSnapshotJSONObject> snapshotJSONObjects = new List<HousingSlotSnapshotJSONObject>();
                foreach(var snapshot in WillUploadSnapshot.Values)
                {
                    snapshotJSONObjects.Add(snapshot.ToJsonObject());
                }
                string json = JsonConvert.SerializeObject(snapshotJSONObjects);
                Log("Info", "正在上传房区快照");
                WillUploadSnapshot.Clear();
                bool res = UploadData("info", json);
                if (res)
                {
                    Log("Info", "房区快照上报成功");
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            catch(Exception ex)
            {
                Log("Error", ex, "房区快照上报出错：");
            }
            //Log("Info", $"上报消息给 {post_url}");
        }
    }
}
