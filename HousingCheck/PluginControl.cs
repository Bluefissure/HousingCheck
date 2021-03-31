using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Advanced_Combat_Tracker;
using System.Collections.Generic;

namespace HousingCheck
{
    public partial class PluginControl : UserControl
    {
        public ApiVersion UploadApiVersion
        {
            get {
                return (selectApiVersion.SelectedItem == null) ? ApiVersion.V2 : (ApiVersion)selectApiVersion.SelectedValue;
            }
            set { }
        }

        public bool EnableUploadSnapshot {
            get
            {
                return checkBoxUploadSnapshot.Checked;
            }
            set
            {
                checkBoxUploadSnapshot.Checked = value;
            }
        }

        public string UploadUrl
        {
            get
            {
                return textBoxUpload.Text;
            }
            set
            {
                textBoxUpload.Text = value;
            }
        }

        public string UploadToken
        {
            get
            {
                return textBoxUploadToken.Text;
            }
            set
            {
                textBoxUploadToken.Text = value;
            }
        }

        private static readonly string SettingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\HousingCheck.config.xml");
        public bool upload;
        public Dictionary<string, ApiVersion> apiVersionList = new Dictionary<string, ApiVersion>();
        public PluginControl()
        {
            InitializeComponent();

            //API版本选择
            apiVersionList.Add("V1", ApiVersion.V1);
            apiVersionList.Add("V2", ApiVersion.V2);
            selectApiVersion.DataSource = new BindingSource(apiVersionList, null);
            selectApiVersion.DisplayMember = "Key";
            selectApiVersion.ValueMember = "Value";
            selectApiVersion.SelectedIndex = 1;
        }

        private void checkBoxUpload_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            upload = checkBox.Checked;

            this.textBoxUpload.ReadOnly = !upload;
            this.textBoxUpload.Enabled = upload;

            this.textBoxUploadToken.ReadOnly = !upload;
            this.textBoxUploadToken.Enabled = upload;

            //this.checkBoxML.Enabled = upload;
            this.checkBoxUploadSnapshot.Enabled = upload;
        }

        public void LoadSettings()
        {
            if (File.Exists(SettingsFile))
            {
                try
                {
                    XmlDocument xdo = new XmlDocument();
                    xdo.Load(SettingsFile);
                    XmlNode head = xdo.SelectSingleNode("Config");
                    textBoxUpload.Text = head?.SelectSingleNode("UploadURL")?.InnerText;
                    selectApiVersion.SelectedIndex = int.Parse(head?.SelectSingleNode("UploadApiVersion")?.InnerText);
                    textBoxUploadToken.Text = head?.SelectSingleNode("UploadToken")?.InnerText;
                    checkBoxUpload.Checked = bool.Parse(head?.SelectSingleNode("AutoUpload")?.InnerText ?? "false");
                    //checkBoxML.Checked = bool.Parse(head?.SelectSingleNode("UploadMLOnly")?.InnerText ?? "true");
                    checkBoxUploadSnapshot.Checked = bool.Parse(head?.SelectSingleNode("UploadSnapshot")?.InnerText ?? "true");
                }
                catch(Exception ex)
                {
                    File.Delete(SettingsFile);
                }
            }
        }

        public void SaveSettings()
        {
            FileStream fs = new FileStream(SettingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            XmlTextWriter xWriter = new XmlTextWriter(fs, Encoding.UTF8) { Formatting = Formatting.Indented, Indentation = 1, IndentChar = '\t' };
            xWriter.WriteStartDocument(true);
            xWriter.WriteStartElement("Config");    // <Config>
            xWriter.WriteElementString("UploadURL", textBoxUpload.Text);
            xWriter.WriteElementString("UploadApiVersion", selectApiVersion.SelectedIndex.ToString());
            xWriter.WriteElementString("UploadToken", textBoxUploadToken.Text);
            xWriter.WriteElementString("AutoUpload", checkBoxUpload.Checked.ToString());
            //xWriter.WriteElementString("UploadMLOnly", checkBoxML.Checked.ToString());
            xWriter.WriteElementString("UploadSnapshot", checkBoxUploadSnapshot.Checked.ToString());
            xWriter.WriteEndElement();              // </Config>
            xWriter.WriteEndDocument();             // Tie up loose ends (shouldn't be any)
            xWriter.Flush();                        // Flush the file buffer to disk
            xWriter.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUpload_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
