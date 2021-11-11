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
using System.Collections.ObjectModel;
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
        public ObservableCollection<HousingItem> HousingList = new ObservableCollection<HousingItem>();
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
                throw new Exception("Could not find FFXIV plugin. Make sure that it is loaded before HousingCheck.");

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
                if (control.checkBoxAutoSaveAndLoad.Checked == true)
                {
                    JsonSave();
                }
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
            control.buttonUploadOnce.Click += ButtonUploadOnce_Click;
            control.buttonCopyToClipboard.Click += ButtonCopyToClipboard_Click;
            control.buttonSaveToFile.Click += ButtonSaveToFile_Click;
            control.buttonJsonSave.Click += ButtonJsonSave_Click;
            control.buttonJsonLoad.Click += ButtonJsonLoad_Click;
            if (control.checkBoxAutoSaveAndLoad.Checked == true)
            {
                JsonLoad();
            }
            string tips = "本插件免费，发布及更新地址 https://file.bluefissure.com/FFXIV/ 或 https://bbs.nga.cn/read.php?tid=25465725 ，勿从其他渠道（闲鱼卖家或神秘群友）获取以避免虚拟财产受到损失。";
            MessageBox.Show(tips);
            Log("Info", tips);
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

        void NetworkReceivedSync(byte[] message)
        {
            //Log("Debug", $"OPCODE:{opcode}");
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
            else
                return;
            int slot = data_header[2];
            if (slot > 30)
                return;
            for (int i = 8; i < data_list.Length; i += 40)
            {
                var house_id = (i - 8) / 40;
                var name_header = data_list.SubArray(i, 8);
                int price = BitConverter.ToInt32(name_header, 0);
                if (price < 0 || price > 100000000)
                    return;
                string size = (price > 30000000) ? "L" : ((price > 10000000) ? "M" : "S");
                var name_array = data_list.SubArray(i + 8, 32);
                if (name_array[0] == 0)
                {
                    string text = $"{area} 第{slot + 1}区 {house_id + 1}号 {size}房在售 当前价格:{price} {Environment.NewLine}";
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
                        HousingList[HousingList.IndexOf(housignItem)].ExistenceTime = DateTime.Now;     //更新时间
                        HousingList[HousingList.IndexOf(housignItem)].Price = price;                    //更新价格
                        Log("Info", "重复土地，已更新。");
                        control.dataGridView1.Refresh();                                                //刷新控件
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
            Log("Info", $"查询第{slot + 1}区");     //输出翻页日志
        }

        void NetworkReceived(string connection, long epoch, byte[] message)
        {
            var opcode = BitConverter.ToUInt16(message, 18);
            if (opcode != 0x0292) return;
            control.Invoke(new Action<byte[]>(NetworkReceivedSync), message);
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

        private void ButtonSaveToFile_Click(object sender, EventArgs e)
        {
            string fileName = $"HousingCheck-{DateTime.Now.ToString("u").Replace(":", "").Replace(" ", "").Replace("-", "")}.txt";
            File.AppendAllText(
                Path.Combine(Environment.CurrentDirectory, fileName),   //ACT根目录
                ListToString()
                );
            Log("Info", $"已保存到{Environment.CurrentDirectory}, {fileName}");
            //Log("Debug", fileName);
        }

        private void ButtonJsonSave_Click(object sender, EventArgs e)
        {
            JsonSave();
        }
        private void ButtonJsonLoad_Click(object sender, EventArgs e)
        {
            JsonLoad();
        }

        private string ListToString()
        {
            ArrayList area = new ArrayList(new string[] { "海雾村", "薰衣草苗圃", "高脚孤丘", "白银乡" });
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (var line in HousingList)
            {
                bool isTimedout = DateTime.Compare(line.ExistenceTime.AddMinutes((double)control.numericUpDownTimeout.Value), DateTime.Now) <= 0;
                if (isTimedout && control.numericUpDownTimeout.Value != 0)
                {
                    continue;
                }

                stringBuilder.Append($"{line.Area} 第{line.Slot}区 {line.Id}号{line.Size}房在售，当前价格:{line.Price} {Environment.NewLine}");

                if (line.Area == "海雾村" && area.IndexOf("海雾村") != -1)
                {
                    area.Remove("海雾村");
                }
                else if (line.Area == "薰衣草苗圃" && area.IndexOf("薰衣草苗圃") != -1)
                {
                    area.Remove("薰衣草苗圃");
                }
                else if (line.Area == "高脚孤丘" && area.IndexOf("高脚孤丘") != -1)
                {
                    area.Remove("高脚孤丘");
                }
                else if (line.Area == "白银乡" && area.IndexOf("白银乡") != -1)
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

        private void JsonSave()
        {
            HousingList.OrderBy(HousingList => HousingList.Area).ThenBy(HousingList => HousingList.Slot).ThenBy(HousingList => HousingList.Id);

            using (StreamWriter file = File.CreateText(control.ItemsFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, HousingList);
                Log("Info", $"列表已保存");
            }
        }

        private void JsonLoad()
        {
            if (File.Exists(control.ItemsFile))
            {
                string s = File.ReadAllText(control.ItemsFile);
                ObservableCollection<HousingItem> TempList = new ObservableCollection<HousingItem>();
                TempList = JsonConvert.DeserializeObject<ObservableCollection<HousingItem>>(s);
                foreach (var item in TempList)
                {
                    if (HousingList.IndexOf(item) == -1)
                    {
                        bindingSource1.Add(item);
                    }
                    //Log("Debug", $"{item.AddTime}");
                }
                Log("Info", $"列表已加载");
            }
            else
            {
                Log("Info", $"{control.ItemsFile}不存在");
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
