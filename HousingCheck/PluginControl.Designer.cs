
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
            this.groupBoxTable.Location = new System.Drawing.Point(20, 10);
            this.groupBoxTable.Margin = new System.Windows.Forms.Padding(10);
            this.groupBoxTable.Name = "groupBoxTable";
            this.groupBoxTable.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxTable.Size = new System.Drawing.Size(853, 1070);
            this.groupBoxTable.TabIndex = 0;
            this.groupBoxTable.TabStop = false;
            this.groupBoxTable.Text = "在售列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 30);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 102;
            this.dataGridView1.Size = new System.Drawing.Size(833, 1030);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBoxUpload
            // 
            this.groupBoxUpload.Controls.Add(this.checkBoxUploadSnapshot);
            this.groupBoxUpload.Controls.Add(this.label3);
            this.groupBoxUpload.Controls.Add(this.textBoxUploadToken);
            this.groupBoxUpload.Controls.Add(this.label2);
            this.groupBoxUpload.Controls.Add(this.label1);
            this.groupBoxUpload.Controls.Add(this.textBoxUpload);
            this.groupBoxUpload.Controls.Add(this.checkBoxUpload);
            this.groupBoxUpload.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxUpload.Location = new System.Drawing.Point(10, 10);
            this.groupBoxUpload.Margin = new System.Windows.Forms.Padding(10, 10, 10, 20);
            this.groupBoxUpload.Name = "groupBoxUpload";
            this.groupBoxUpload.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxUpload.Size = new System.Drawing.Size(497, 181);
            this.groupBoxUpload.TabIndex = 1;
            this.groupBoxUpload.TabStop = false;
            this.groupBoxUpload.Text = "上报设置";
            // 
            // checkBoxUploadSnapshot
            // 
            this.checkBoxUploadSnapshot.AutoSize = true;
            this.checkBoxUploadSnapshot.Checked = true;
            this.checkBoxUploadSnapshot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUploadSnapshot.Enabled = false;
            this.checkBoxUploadSnapshot.Location = new System.Drawing.Point(154, 36);
            this.checkBoxUploadSnapshot.Name = "checkBoxUploadSnapshot";
            this.checkBoxUploadSnapshot.Size = new System.Drawing.Size(121, 24);
            this.checkBoxUploadSnapshot.TabIndex = 7;
            this.checkBoxUploadSnapshot.Text = "上报房区快照";
            this.checkBoxUploadSnapshot.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "如服务器未开启身份验证请留空";
            // 
            // textBoxUploadToken
            // 
            this.textBoxUploadToken.Enabled = false;
            this.textBoxUploadToken.Location = new System.Drawing.Point(112, 110);
            this.textBoxUploadToken.Name = "textBoxUploadToken";
            this.textBoxUploadToken.ReadOnly = true;
            this.textBoxUploadToken.Size = new System.Drawing.Size(370, 27);
            this.textBoxUploadToken.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Token：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "上报地址：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBoxUpload
            // 
            this.textBoxUpload.Enabled = false;
            this.textBoxUpload.Location = new System.Drawing.Point(112, 74);
            this.textBoxUpload.Margin = new System.Windows.Forms.Padding(5);
            this.textBoxUpload.Name = "textBoxUpload";
            this.textBoxUpload.ReadOnly = true;
            this.textBoxUpload.Size = new System.Drawing.Size(370, 27);
            this.textBoxUpload.TabIndex = 1;
            this.textBoxUpload.TextChanged += new System.EventHandler(this.textBoxUpload_TextChanged);
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.Location = new System.Drawing.Point(15, 36);
            this.checkBoxUpload.Margin = new System.Windows.Forms.Padding(5);
            this.checkBoxUpload.Name = "checkBoxUpload";
            this.checkBoxUpload.Size = new System.Drawing.Size(121, 24);
            this.checkBoxUpload.TabIndex = 0;
            this.checkBoxUpload.Text = "开启自动上报";
            this.checkBoxUpload.UseVisualStyleBackColor = true;
            this.checkBoxUpload.CheckedChanged += new System.EventHandler(this.checkBoxUpload_CheckedChanged);
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.textBoxLog);
            this.groupBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLog.Location = new System.Drawing.Point(10, 283);
            this.groupBoxLog.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxLog.Size = new System.Drawing.Size(497, 797);
            this.groupBoxLog.TabIndex = 2;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "日志";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(10, 30);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(5);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(477, 757);
            this.textBoxLog.TabIndex = 0;
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Controls.Add(this.buttonCopyToClipboard);
            this.groupBoxControl.Controls.Add(this.buttonSaveToFile);
            this.groupBoxControl.Controls.Add(this.buttonUploadOnce);
            this.groupBoxControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxControl.Location = new System.Drawing.Point(10, 191);
            this.groupBoxControl.Margin = new System.Windows.Forms.Padding(5, 20, 5, 5);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Padding = new System.Windows.Forms.Padding(5);
            this.groupBoxControl.Size = new System.Drawing.Size(497, 92);
            this.groupBoxControl.TabIndex = 3;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "操作";
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(330, 34);
            this.buttonCopyToClipboard.Margin = new System.Windows.Forms.Padding(5);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(147, 39);
            this.buttonCopyToClipboard.TabIndex = 0;
            this.buttonCopyToClipboard.Text = "复制到剪贴板";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // buttonSaveToFile
            // 
            this.buttonSaveToFile.Location = new System.Drawing.Point(173, 34);
            this.buttonSaveToFile.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.buttonSaveToFile.Size = new System.Drawing.Size(147, 39);
            this.buttonSaveToFile.TabIndex = 0;
            this.buttonSaveToFile.Text = "保存到文件";
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            // 
            // buttonUploadOnce
            // 
            this.buttonUploadOnce.Location = new System.Drawing.Point(10, 34);
            this.buttonUploadOnce.Margin = new System.Windows.Forms.Padding(5);
            this.buttonUploadOnce.Name = "buttonUploadOnce";
            this.buttonUploadOnce.Size = new System.Drawing.Size(147, 39);
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
            this.panel1.Location = new System.Drawing.Point(883, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 10, 20, 12);
            this.panel1.Size = new System.Drawing.Size(527, 1092);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBoxTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(20, 10, 10, 12);
            this.panel2.Size = new System.Drawing.Size(883, 1092);
            this.panel2.TabIndex = 5;
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(1410, 1092);
            this.groupBoxTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingCheckBindingSource)).EndInit();
            this.groupBoxUpload.ResumeLayout(false);
            this.groupBoxUpload.PerformLayout();
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
    }
}
