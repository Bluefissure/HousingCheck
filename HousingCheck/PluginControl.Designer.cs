
namespace HousingCheck
{
    partial class PluginControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBoxTable = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.housingCheckBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBoxUpload = new System.Windows.Forms.GroupBox();
            this.selectApiVersion = new System.Windows.Forms.ComboBox();
            this.pluginControlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxUploadSnapshot = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUploadToken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUpload = new System.Windows.Forms.TextBox();
            this.checkBoxUpload = new System.Windows.Forms.CheckBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.buttonSaveToFile = new System.Windows.Forms.Button();
            this.buttonUploadOnce = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingCheckBindingSource)).BeginInit();
            this.groupBoxUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pluginControlBindingSource)).BeginInit();
            this.groupBoxLog.SuspendLayout();
            this.groupBoxControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTable
            // 
            this.groupBoxTable.Controls.Add(this.dataGridView1);
            this.groupBoxTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTable.Location = new System.Drawing.Point(16, 9);
            this.groupBoxTable.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.groupBoxTable.Name = "groupBoxTable";
            this.groupBoxTable.Padding = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.groupBoxTable.Size = new System.Drawing.Size(663, 909);
            this.groupBoxTable.TabIndex = 0;
            this.groupBoxTable.TabStop = false;
            this.groupBoxTable.Text = "在售列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(8, 25);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 50;
            this.dataGridView1.Size = new System.Drawing.Size(647, 875);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBoxUpload
            // 
            this.groupBoxUpload.Controls.Add(this.selectApiVersion);
            this.groupBoxUpload.Controls.Add(this.label4);
            this.groupBoxUpload.Controls.Add(this.checkBoxUploadSnapshot);
            this.groupBoxUpload.Controls.Add(this.label3);
            this.groupBoxUpload.Controls.Add(this.textBoxUploadToken);
            this.groupBoxUpload.Controls.Add(this.label2);
            this.groupBoxUpload.Controls.Add(this.label1);
            this.groupBoxUpload.Controls.Add(this.textBoxUpload);
            this.groupBoxUpload.Controls.Add(this.checkBoxUpload);
            this.groupBoxUpload.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxUpload.Location = new System.Drawing.Point(8, 9);
            this.groupBoxUpload.Margin = new System.Windows.Forms.Padding(8, 9, 8, 17);
            this.groupBoxUpload.Name = "groupBoxUpload";
            this.groupBoxUpload.Padding = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.groupBoxUpload.Size = new System.Drawing.Size(386, 154);
            this.groupBoxUpload.TabIndex = 1;
            this.groupBoxUpload.TabStop = false;
            this.groupBoxUpload.Text = "上报设置";
            // 
            // selectApiVersion
            // 
            this.selectApiVersion.DataSource = this.pluginControlBindingSource;
            this.selectApiVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectApiVersion.FormattingEnabled = true;
            this.selectApiVersion.Location = new System.Drawing.Point(316, 28);
            this.selectApiVersion.Name = "selectApiVersion";
            this.selectApiVersion.Size = new System.Drawing.Size(60, 25);
            this.selectApiVersion.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "上报API版本:";
            // 
            // checkBoxUploadSnapshot
            // 
            this.checkBoxUploadSnapshot.AutoSize = true;
            this.checkBoxUploadSnapshot.Checked = true;
            this.checkBoxUploadSnapshot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUploadSnapshot.Enabled = false;
            this.checkBoxUploadSnapshot.Location = new System.Drawing.Point(123, 31);
            this.checkBoxUploadSnapshot.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBoxUploadSnapshot.Name = "checkBoxUploadSnapshot";
            this.checkBoxUploadSnapshot.Size = new System.Drawing.Size(99, 21);
            this.checkBoxUploadSnapshot.TabIndex = 7;
            this.checkBoxUploadSnapshot.Text = "上报房区快照";
            this.checkBoxUploadSnapshot.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 122);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "如服务器未开启身份验证请留空";
            // 
            // textBoxUploadToken
            // 
            this.textBoxUploadToken.Enabled = false;
            this.textBoxUploadToken.Location = new System.Drawing.Point(87, 94);
            this.textBoxUploadToken.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxUploadToken.Name = "textBoxUploadToken";
            this.textBoxUploadToken.ReadOnly = true;
            this.textBoxUploadToken.Size = new System.Drawing.Size(289, 23);
            this.textBoxUploadToken.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Token:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "上报地址:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBoxUpload
            // 
            this.textBoxUpload.Enabled = false;
            this.textBoxUpload.Location = new System.Drawing.Point(87, 63);
            this.textBoxUpload.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUpload.Name = "textBoxUpload";
            this.textBoxUpload.ReadOnly = true;
            this.textBoxUpload.Size = new System.Drawing.Size(289, 23);
            this.textBoxUpload.TabIndex = 1;
            this.textBoxUpload.TextChanged += new System.EventHandler(this.textBoxUpload_TextChanged);
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.Location = new System.Drawing.Point(12, 31);
            this.checkBoxUpload.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxUpload.Name = "checkBoxUpload";
            this.checkBoxUpload.Size = new System.Drawing.Size(99, 21);
            this.checkBoxUpload.TabIndex = 0;
            this.checkBoxUpload.Text = "开启自动上报";
            this.checkBoxUpload.UseVisualStyleBackColor = true;
            this.checkBoxUpload.CheckedChanged += new System.EventHandler(this.checkBoxUpload_CheckedChanged);
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.textBoxLog);
            this.groupBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLog.Location = new System.Drawing.Point(8, 241);
            this.groupBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Padding = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.groupBoxLog.Size = new System.Drawing.Size(386, 677);
            this.groupBoxLog.TabIndex = 2;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "日志";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(8, 25);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(370, 643);
            this.textBoxLog.TabIndex = 0;
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Controls.Add(this.buttonCopyToClipboard);
            this.groupBoxControl.Controls.Add(this.buttonSaveToFile);
            this.groupBoxControl.Controls.Add(this.buttonUploadOnce);
            this.groupBoxControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxControl.Location = new System.Drawing.Point(8, 163);
            this.groupBoxControl.Margin = new System.Windows.Forms.Padding(4, 17, 4, 4);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxControl.Size = new System.Drawing.Size(386, 78);
            this.groupBoxControl.TabIndex = 3;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "操作";
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(261, 29);
            this.buttonCopyToClipboard.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(114, 33);
            this.buttonCopyToClipboard.TabIndex = 0;
            this.buttonCopyToClipboard.Text = "复制到剪贴板";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // buttonSaveToFile
            // 
            this.buttonSaveToFile.Location = new System.Drawing.Point(136, 29);
            this.buttonSaveToFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.buttonSaveToFile.Size = new System.Drawing.Size(114, 33);
            this.buttonSaveToFile.TabIndex = 0;
            this.buttonSaveToFile.Text = "保存到文件";
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            // 
            // buttonUploadOnce
            // 
            this.buttonUploadOnce.Location = new System.Drawing.Point(12, 29);
            this.buttonUploadOnce.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUploadOnce.Name = "buttonUploadOnce";
            this.buttonUploadOnce.Size = new System.Drawing.Size(114, 33);
            this.buttonUploadOnce.TabIndex = 0;
            this.buttonUploadOnce.Text = "手动上报一次";
            this.buttonUploadOnce.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxLog);
            this.panel1.Controls.Add(this.groupBoxControl);
            this.panel1.Controls.Add(this.groupBoxUpload);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(687, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8, 9, 16, 10);
            this.panel1.Size = new System.Drawing.Size(410, 928);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBoxTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(16, 9, 8, 10);
            this.panel2.Size = new System.Drawing.Size(687, 928);
            this.panel2.TabIndex = 5;
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(1097, 928);
            this.groupBoxTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingCheckBindingSource)).EndInit();
            this.groupBoxUpload.ResumeLayout(false);
            this.groupBoxUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pluginControlBindingSource)).EndInit();
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
            this.groupBoxControl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTable;
        private System.Windows.Forms.BindingSource housingCheckBindingSource;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBoxUpload;
        private System.Windows.Forms.CheckBox checkBoxUpload;
        private System.Windows.Forms.GroupBox groupBoxLog;
        public System.Windows.Forms.TextBox textBoxLog;
        public System.Windows.Forms.TextBox textBoxUpload;
        private System.Windows.Forms.GroupBox groupBoxControl;
        public System.Windows.Forms.Button buttonCopyToClipboard;
        public System.Windows.Forms.Button buttonSaveToFile;
        public System.Windows.Forms.Button buttonUploadOnce;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBoxUploadToken;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox checkBoxUploadSnapshot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource pluginControlBindingSource;
        public System.Windows.Forms.ComboBox selectApiVersion;
    }
}
