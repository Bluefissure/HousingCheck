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
    public class HousingCheck : IActPluginV1
    {
        public ObservableCollection<HousingOnSaleItem> HousingList = new ObservableCollection<HousingOnSaleItem>();
        public BindingSource bindingSource1;
        FFXIV_ACT_Plugin.FFXIV_ACT_Plugin ffxivPlugin;
        bool initialized = false;
        bool ManualUpload = false;
        //上报队列
        bool HousingListUpdated = false;
        Dictionary<Tuple<HouseArea, int>, HousingSlotSnapshot> AutoUploadSnapshot = new Dictionary<Tuple<HouseArea, int>, HousingSlotSnapshot>();
        long LastOperateTime = 0;
        long AutoSaveAfter = 20; //20秒无操作自动保存
        HousingSnapshotStorage SnapshotStorage = new HousingSnapshotStorage();
        private BackgroundWorker AutoSaveThread;
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
                control.SaveSettings();
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
            bindingSource1 = new BindingSource { DataSource = HousingList };
            control.dataGridView1.DataSource = bindingSource1;
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
            AutoSaveThread.DoWork += AutoSaveWorker;
            AutoSaveThread.RunWorkerAsync();
            statusLabel.Text = "Working :D";
            control.LoadSettings();
            control.buttonUploadOnce.Click += ButtonUploadOnce_Click;
            control.buttonCopyToClipboard.Click += ButtonCopyToClipboard_Click;
            control.buttonSaveToFile.Click += ButtonSaveToFile_Click;
            PrepareDir();
        }

        int GetServerId()
        {
            return (int)ffxivPlugin.DataRepository.GetCombatantList()
                .FirstOrDefault(x => x.ID == ffxivPlugin.DataRepository.GetCurrentPlayerID()).CurrentWorldID;
        }

        void Log(string type, string message)
        {
            var time = (DateTime.Now).ToString("HH:mm:ss");
            var text = $"[{time}] [{type}] {message.Trim()}";
            control.textBoxLog.Text += text + Environment.NewLine;
            control.textBoxLog.SelectionStart = control.textBoxLog.TextLength;
            control.textBoxLog.ScrollToCaret(); 
            text = $"00|{DateTime.Now.ToString("O")}|0|HousingCheck-{message}|";        //解析插件数据格式化
            ActGlobals.oFormActMain.ParseRawLogLine(false, DateTime.Now, $"{text}");    //插入ACT日志
        }

        string HousingListToJson()
        {
            var saveList = new List<HousingOnSaleItem>();
            foreach(var house in HousingList)
            {
                if (house.CurrentStatus)
                {
                    saveList.Add(house);
                }
            }
            return JsonConvert.SerializeObject(saveList.ToArray());
        }

        void NetworkReceived(string connection, long epoch, byte[] message)
        {
            var opcode = BitConverter.ToUInt16(message, 18);
            if (opcode != 0x164 && message.Length != 2440) return;
            HousingSlotSnapshot snapshot;
            try
            {
                //解析数据包
                snapshot = new HousingSlotSnapshot(message);
                snapshot.ServerId = GetServerId();
                /*foreach(var house in snapshot.HouseList.Values)
                {
                    Log("Debug", house.Id + ": " + house.Flags.ToString());
                }*/
                //存入存储
                SnapshotStorage.Insert(snapshot);
                AutoUploadSnapshot[new Tuple<HouseArea, int>(snapshot.Area, snapshot.Slot)] = snapshot;
                
                foreach(HousingOnSaleItem item in HousingList)
                {
                    if(item.Area == snapshot.Area && item.Slot == snapshot.Slot)
                    {
                        item.CurrentStatus = false;
                    }
                }

                HousingItem[] onSaleList = snapshot.GetOnSale();
                int listIndex;
                //更新空房列表
                foreach(HousingItem house in onSaleList)
                {
                    HousingOnSaleItem onSaleItem = new HousingOnSaleItem(house);
                    Log("Info", string.Format("{0} 第{1}区 {2}号 {3}房在售 当前价格: {4}",
                        onSaleItem.AreaStr, onSaleItem.Slot, onSaleItem.Id,
                        onSaleItem.SizeStr, onSaleItem.Price));

                    if (onSaleItem.Size == HouseSize.M || onSaleItem.Size == HouseSize.L)
                    {
                        Console.Beep(3000, 1000);
                    }

                    if ((listIndex = HousingList.IndexOf(onSaleItem)) != -1)
                    {
                        HousingList[listIndex].Update(house);
                        Log("Info", "重复土地，已更新。");
                    }
                    else
                    {
                        bindingSource1.Add(onSaleItem);
                    }
                    HousingListUpdated = true;
                }
                LastOperateTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds(); //更新上次操作的时间
                bindingSource1.ResetBindings(false); //刷新列表
                Log("Info", string.Format("{0} 第{1}区查询完成",
                    HousingItem.GetHouseAreaStr(snapshot.Area),
                    snapshot.Slot + 1));     //输出翻页日志
            }
            catch (Exception ex)
            {
                Log("Error", ex.Message);
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

                stringBuilder.Append($"{line.Area} 第{line.Slot}区 {line.Id}号{line.Size}房在售，当前价格:{line.Price} {Environment.NewLine}");

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

        private void AutoSaveWorker(object sender, DoWorkEventArgs e)
        {
            long actionTime;
            bool dataUpdated;
            int snapshotCount;
            bool autoUpload;
            bool manualUpload;
            bool uploadSnapshot;
            while (true)
            {
                if (AutoSaveThread.CancellationPending)
                {
                    break;
                }
                Monitor.Enter(this);
                actionTime = LastOperateTime + AutoSaveAfter;
                dataUpdated = HousingListUpdated;
                snapshotCount = AutoUploadSnapshot.Count;
                autoUpload = control.upload;
                manualUpload = ManualUpload;
                uploadSnapshot = control.checkBoxUploadSnapshot.Checked;
                Monitor.Exit(this);
                if (actionTime <= new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds())
                {
                    if (dataUpdated || snapshotCount > 0) //如果有更新
                    {
                        if (dataUpdated)
                        {
                            //保存列表文件
                            Monitor.Enter(this);
                            SaveHousingList();
                            Log("Info", "房屋信息已保存");
                            Monitor.Exit(this);
                            if (autoUpload)
                            {
                                //自动上报
                                UploadOnSaleList();
                            }
                        }

                        if (autoUpload && uploadSnapshot && snapshotCount > 0)
                        {
                            //上传快照
                            UploadSnapshot();
                        }
                    }
                }
                else if (manualUpload)
                {
                    UploadOnSaleList();
                    if (uploadSnapshot && snapshotCount > 0)
                    {
                        UploadSnapshot();
                    }
                    Monitor.Enter(this);
                    ManualUpload = false;
                    Monitor.Exit(this);
                }
                Thread.Sleep(500);
            }
        }

        public bool UploadData(string type, string json)
        {
            var wb = new WebClient();
            Monitor.Enter(this);
            var token = control.textBoxUploadToken.Text.Trim();
            if (token != "")
            {
                wb.Headers[HttpRequestHeader.Authorization] = "Token " + token;
            }
            wb.Headers[HttpRequestHeader.ContentType] = "application/json";
            var url = control.textBoxUpload.Text.TrimEnd('/') + "/" + type;
            Monitor.Exit(this);
            try
            {
                var response = wb.UploadData(url, "POST",
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json))
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
                            Monitor.Enter(this);
                            Log("Error", "上传出错：" + jsonRes["errorMessage"]);
                            Monitor.Exit(this);
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
                            Monitor.Enter(this);
                            Log("Error", "上传出错：" + res);
                            Monitor.Exit(this);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Monitor.Enter(this);
                Log("Error", "上传出错：" + ex.Message);
                Monitor.Exit(this);
            }
            return false;
        }

        private void UploadOnSaleList()
        {
            Monitor.Enter(this);
            string json = JsonConvert.SerializeObject(HousingList);
            Log("Info", "正在上传空房列表");
            HousingListUpdated = false;
            Monitor.Exit(this);
            bool res = UploadData("info", json);

            if (res)
            {
                Monitor.Enter(this);
                Log("Info", "房屋列表上报成功");
                Monitor.Exit(this);
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
                Monitor.Enter(this);
                List<HousingSlotSnapshotJSONObject> snapshotJSONObjects = new List<HousingSlotSnapshotJSONObject>();
                foreach(var snapshot in AutoUploadSnapshot.Values)
                {
                    snapshotJSONObjects.Add(snapshot.ToJsonObject());
                }
                string json = JsonConvert.SerializeObject(snapshotJSONObjects);
                Log("Info", "正在上传房区快照");
                AutoUploadSnapshot.Clear();
                Monitor.Exit(this);
                bool res = UploadData("info", json);
                if (res)
                {
                    Monitor.Enter(this);
                    Log("Info", "房区快照上报成功");
                    Monitor.Exit(this);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            catch(Exception ex)
            {
                Log("Error", ex.Message + Environment.NewLine + ex.StackTrace);
            }
            //Log("Info", $"上报消息给 {post_url}");
        }
    }
}
