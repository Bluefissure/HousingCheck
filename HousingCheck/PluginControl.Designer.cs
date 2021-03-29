
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
            this.checkBoxML = new System.Windows.Forms.CheckBox();
            this.textBoxUpload = new System.Windows.Forms.TextBox();
            this.checkBoxUpload = new System.Windows.Forms.CheckBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.buttonSaveToFile = new System.Windows.Forms.Button();
            this.buttonUploadOnce = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingCheckBindingSource)).BeginInit();
            this.groupBoxUpload.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.groupBoxControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTable
            // 
            this.groupBoxTable.Controls.Add(this.dataGridView1);
            this.groupBoxTable.Location = new System.Drawing.Point(21, 26);
            this.groupBoxTable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxTable.Name = "groupBoxTable";
            this.groupBoxTable.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxTable.Size = new System.Drawing.Size(628, 853);
            this.groupBoxTable.TabIndex = 0;
            this.groupBoxTable.TabStop = false;
            this.groupBoxTable.Text = "在售列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 26);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 102;
            this.dataGridView1.Size = new System.Drawing.Size(613, 819);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBoxUpload
            // 
            this.groupBoxUpload.Controls.Add(this.label1);
            this.groupBoxUpload.Controls.Add(this.checkBoxML);
            this.groupBoxUpload.Controls.Add(this.textBoxUpload);
            this.groupBoxUpload.Controls.Add(this.checkBoxUpload);
            this.groupBoxUpload.Location = new System.Drawing.Point(656, 26);
            this.groupBoxUpload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxUpload.Name = "groupBoxUpload";
            this.groupBoxUpload.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxUpload.Size = new System.Drawing.Size(375, 196);
            this.groupBoxUpload.TabIndex = 1;
            this.groupBoxUpload.TabStop = false;
            this.groupBoxUpload.Text = "上报设置";
            // 
            // checkBoxML
            // 
            this.checkBoxML.AutoSize = true;
            this.checkBoxML.Checked = true;
            this.checkBoxML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxML.Location = new System.Drawing.Point(133, 26);
            this.checkBoxML.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxML.Name = "checkBoxML";
            this.checkBoxML.Size = new System.Drawing.Size(86, 21);
            this.checkBoxML.TabIndex = 2;
            this.checkBoxML.Text = "只上报M/L";
            this.checkBoxML.UseVisualStyleBackColor = true;
            // 
            // textBoxUpload
            // 
            this.textBoxUpload.Enabled = false;
            this.textBoxUpload.Location = new System.Drawing.Point(8, 76);
            this.textBoxUpload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxUpload.Multiline = true;
            this.textBoxUpload.Name = "textBoxUpload";
            this.textBoxUpload.ReadOnly = true;
            this.textBoxUpload.Size = new System.Drawing.Size(359, 109);
            this.textBoxUpload.TabIndex = 1;
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.Location = new System.Drawing.Point(8, 26);
            this.checkBoxUpload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.groupBoxLog.Location = new System.Drawing.Point(657, 316);
            this.groupBoxLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxLog.Size = new System.Drawing.Size(373, 562);
            this.groupBoxLog.TabIndex = 2;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "日志";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(8, 26);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(358, 527);
            this.textBoxLog.TabIndex = 0;
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Controls.Add(this.buttonCopyToClipboard);
            this.groupBoxControl.Controls.Add(this.buttonSaveToFile);
            this.groupBoxControl.Controls.Add(this.buttonUploadOnce);
            this.groupBoxControl.Location = new System.Drawing.Point(657, 230);
            this.groupBoxControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxControl.Size = new System.Drawing.Size(373, 78);
            this.groupBoxControl.TabIndex = 3;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "操作";
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(252, 29);
            this.buttonCopyToClipboard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(114, 33);
            this.buttonCopyToClipboard.TabIndex = 0;
            this.buttonCopyToClipboard.Text = "复制到剪贴板";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // buttonSaveToFile
            // 
            this.buttonSaveToFile.Location = new System.Drawing.Point(130, 29);
            this.buttonSaveToFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.buttonSaveToFile.Size = new System.Drawing.Size(114, 33);
            this.buttonSaveToFile.TabIndex = 0;
            this.buttonSaveToFile.Text = "保存到文件";
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            // 
            // buttonUploadOnce
            // 
            this.buttonUploadOnce.Location = new System.Drawing.Point(7, 29);
            this.buttonUploadOnce.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonUploadOnce.Name = "buttonUploadOnce";
            this.buttonUploadOnce.Size = new System.Drawing.Size(114, 33);
            this.buttonUploadOnce.TabIndex = 0;
            this.buttonUploadOnce.Text = "手动上报一次";
            this.buttonUploadOnce.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "上报地址：";
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxControl);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.groupBoxUpload);
            this.Controls.Add(this.groupBoxTable);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(1097, 928);
            this.groupBoxTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingCheckBindingSource)).EndInit();
            this.groupBoxUpload.ResumeLayout(false);
            this.groupBoxUpload.PerformLayout();
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
            this.groupBoxControl.ResumeLayout(false);
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
        public System.Windows.Forms.CheckBox checkBoxML;
        private System.Windows.Forms.GroupBox groupBoxControl;
        public System.Windows.Forms.Button buttonCopyToClipboard;
        public System.Windows.Forms.Button buttonSaveToFile;
        public System.Windows.Forms.Button buttonUploadOnce;
        private System.Windows.Forms.Label label1;
    }
}
