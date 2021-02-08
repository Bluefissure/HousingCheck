using System;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
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
        public List<HousingItem> HousingList = new List<HousingItem>();
        public BindingSource bindingSource1;
        FFXIV_ACT_Plugin.FFXIV_ACT_Plugin ffxivPlugin;
        bool initialized = false;
        bool OtterUploadFlag = false;       
        string OtterText = "";      //上报队列
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
            pluginScreenSpace.Text = "Housing Check";
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
        }


        private void AddMessageToTextBoxLog(string line)
        {
            List<string> lines = new List<string>();
            lines.AddRange(control.textBoxLog.Lines);
            lines.Add(line);
            control.textBoxLog.Lines = lines.ToArray();
            control.textBoxLog.Refresh();
        }
        void Log(string type, string message)
        {
            var time = (DateTime.Now).ToString("HH:mm:ss");
            var text = $"[{time}] [{type}] {message}";
            AddMessageToTextBoxLog(text);
        }

        void NetworkReceived(string connection, long epoch, byte[] message)
        {
            var opcode = BitConverter.ToUInt16(message, 18);
            if (opcode != 0x164 && message.Length != 2440) return;
            var data_list = message.SubArray(32, message.Length - 32);
            var data_header = data_list.SubArray(0, 8);
            string area = "";
            if (data_header[4] == 0x53)
                area = "海雾村";
            else if (data_header[4] == 0x54)
                area = "薰衣草苗圃";
            else if (data_header[4] == 0x55)
                area = "高脚孤丘";
            else if (data_header[4] == 0x81)
                area = "白银乡";
            int slot = data_header[2];
            for (int i = 8; i < data_list.Length; i += 40)
            {
                var house_id = (i - 8) / 40;
                var name_header = data_list.SubArray(i, 8);
                int price = BitConverter.ToInt32(name_header, 0);
                string size = (price > 30000000) ? "L" : ((price > 10000000) ? "M" : "S");
                var name_array = data_list.SubArray(i + 8, 32);
                if (name_array[0] == 0)
                {
                    string text = $"{area} 第{slot + 1}区 {house_id + 1}号 {size}房在售 当前价格:{price}{Environment.NewLine}";
                    Log("Info", text);
                    var housignItem = new HousingItem(
                            area,
                            slot + 1,
                            house_id + 1,
                            size,
                            price
                        );
                    if(HousingList.IndexOf(housignItem) == -1)
                    {
                        bindingSource1.Add(housignItem);
                    }
                    else
                    {
                        Log("Info", "重复土地，已过滤。");
                    }
                    if(size == "M" || size == "L")
                    {
                        Console.Beep(3000, 1000);
                    }
                    if (control.upload)
                    {
                        if (!control.checkBoxML.Checked || (size == "M" || size == "L"))
                        {
                            OtterText += text;
                            OtterUploadFlag = true;
                        }

                    }


                }
            }
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
