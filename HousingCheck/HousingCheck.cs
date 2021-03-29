using System;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;
using System.Linq;
using System.Text;
using Advanced_Combat_Tracker;
using FFXIV_ACT_Plugin;
using FFXIV_ACT_Plugin.Common;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Xml;
using System.Collections.ObjectModel;
using System.Collections.Generic;

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
        bool OtterUploadFlag = false;       
        string OtterText = "";      //上报队列
        HousingSnapshotStorage SnapshotStorage = new HousingSnapshotStorage();
        private BackgroundWorker OtterThread;
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
                OtterThread.CancelAsync();
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
            pluginScreenSpace.Controls.Add(control);

            var subs = ffxivPlugin.GetType().GetProperty("DataSubscription").GetValue(ffxivPlugin, null);
            var networkReceivedDelegateType = typeof(NetworkReceivedDelegate);
            var networkReceivedDelegate = Delegate.CreateDelegate(networkReceivedDelegateType, (object)this, "NetworkReceived", true);
            subs.GetType().GetEvent("NetworkReceived").AddEventHandler(subs, networkReceivedDelegate);
            initialized = true;
            OtterThread = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            OtterThread.DoWork += OtterUpload;
            OtterThread.RunWorkerAsync();
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
                }
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
            OtterText = ListToString();
            OtterUploadFlag = true;
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
        }

        private void ButtonSaveToFile_Click(object sender, EventArgs e)
        {
            PrepareDir();
            string fileName = $"HousingCheck-{DateTime.Now.ToString("u").Replace(":", "").Replace(" ", "").Replace("-", "")}.csv";
            string savePath = Path.Combine(new string[] { Environment.CurrentDirectory, "AppData", "HousingCheck", fileName });

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

        private void OtterUpload(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (OtterThread.CancellationPending)
                {
                    break;
                }
                if (OtterUploadFlag)
                {
                    try
                    {
                        string urls = control.textBoxUpload.Text;
                        foreach (string url in urls.Split('\n'))
                        {
                            string post_url = url.Trim();
                            if (post_url == "") continue;
                            Log("Info", $"上报消息给 {post_url}");
                            var wb = new WebClient();
                            var data = new NameValueCollection
                            {
                                { "text", OtterText }
                            };
                            var response = wb.UploadValues(post_url, "POST", data);
                            string responseInString = Encoding.UTF8.GetString(response);
                            if (responseInString == "OK")
                            {
                                OtterText = "";
                                OtterUploadFlag = false;
                                Log("Info", "上报成功");
                            }
                            else
                            {
                                Log("Error", "上报失败");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Log("Error", "上报失败: " + ex.Message);
                    }
                    Thread.Sleep(5100);
                }
                else
                {
                    Thread.Sleep(300);
                }
            }
        }
    }
}
