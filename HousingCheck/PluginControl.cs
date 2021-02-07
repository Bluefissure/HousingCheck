using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HousingCheck
{
    public partial class PluginControl : UserControl
    {

        public bool upload;
        public PluginControl()
        {
            InitializeComponent();
        }

        private void checkBoxUpload_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            upload = checkBox.Checked;
            this.textBoxUpload.ReadOnly = !upload;
            this.textBoxUpload.Enabled = upload;
        }
    }
}
