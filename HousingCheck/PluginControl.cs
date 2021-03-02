using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Advanced_Combat_Tracker;

namespace HousingCheck
{
    public partial class PluginControl : UserControl
    {

        private static readonly string SettingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\HousingCheck.config.xml");
        public bool upload;
        public PluginControl()
        {
            InitializeComponent();
            numericUpDownTimeout.Location = new Point(label1.Location.X + label1.Width, numericUpDownTimeout.Location.Y);
        }

        private void checkBoxUpload_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            upload = checkBox.Checked;
            this.textBoxUpload.ReadOnly = !upload;
            this.textBoxUpload.Enabled = upload;
        }

        public void LoadSettings()
        {
            if (File.Exists(SettingsFile))
            {
                XmlDocument xdo = new XmlDocument();
                xdo.Load(SettingsFile);
                XmlNode head = xdo.SelectSingleNode("Config");
                textBoxUpload.Text = head?.SelectSingleNode("OtterURL")?.InnerText;
                checkBoxUpload.Checked = bool.Parse(head?.SelectSingleNode("AutoUpload")?.InnerText ?? "false");
                checkBoxML.Checked = bool.Parse(head?.SelectSingleNode("ML")?.InnerText ?? "true");
                numericUpDownTimeout.Value = decimal.Parse(head?.SelectSingleNode("Timeout")?.InnerText ?? "45");
            }

        }
        public void SaveSettings()
        {
            FileStream fs = new FileStream(SettingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            XmlTextWriter xWriter = new XmlTextWriter(fs, Encoding.UTF8) { Formatting = Formatting.Indented, Indentation = 1, IndentChar = '\t' };
            xWriter.WriteStartDocument(true);
            xWriter.WriteStartElement("Config");    // <Config>
            xWriter.WriteElementString("OtterURL", textBoxUpload.Text);
            xWriter.WriteElementString("AutoUpload", checkBoxUpload.Checked.ToString());
            xWriter.WriteElementString("ML", checkBoxML.Checked.ToString());
            xWriter.WriteElementString("Timeout", numericUpDownTimeout.Value.ToString());
            xWriter.WriteEndElement();              // </Config>
            xWriter.WriteEndDocument();             // Tie up loose ends (shouldn't be any)
            xWriter.Flush();                        // Flush the file buffer to disk
            xWriter.Close();
        }
    }
}
