
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
            this.areaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slotDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existenceTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxUpload = new System.Windows.Forms.GroupBox();
            this.checkBoxML = new System.Windows.Forms.CheckBox();
            this.textBoxUpload = new System.Windows.Forms.TextBox();
            this.checkBoxUpload = new System.Windows.Forms.CheckBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.numericUpDownTimeout = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.buttonSaveToFile = new System.Windows.Forms.Button();
            this.buttonUploadOnce = new System.Windows.Forms.Button();
            this.housingCheckBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBoxTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBoxUpload.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.groupBoxControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingCheckBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTable
            // 
            this.groupBoxTable.Controls.Add(this.dataGridView1);
            this.groupBoxTable.Location = new System.Drawing.Point(18, 18);
            this.groupBoxTable.Name = "groupBoxTable";
            this.groupBoxTable.Size = new System.Drawing.Size(538, 602);
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
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.areaDataGridViewTextBoxColumn,
            this.slotDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn,
            this.sizeDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn,
            this.addTimeDataGridViewTextBoxColumn,
            this.existenceTimeDataGridViewTextBoxColumn});
            this.dataGridView1.Location = new System.Drawing.Point(7, 18);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(525, 578);
            this.dataGridView1.TabIndex = 0;
            // 
            // areaDataGridViewTextBoxColumn
            // 
            this.areaDataGridViewTextBoxColumn.DataPropertyName = "Area";
            this.areaDataGridViewTextBoxColumn.HeaderText = "住宅区";
            this.areaDataGridViewTextBoxColumn.Name = "areaDataGridViewTextBoxColumn";
            this.areaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // slotDataGridViewTextBoxColumn
            // 
            this.slotDataGridViewTextBoxColumn.DataPropertyName = "Slot";
            this.slotDataGridViewTextBoxColumn.FillWeight = 40F;
            this.slotDataGridViewTextBoxColumn.HeaderText = "区";
            this.slotDataGridViewTextBoxColumn.Name = "slotDataGridViewTextBoxColumn";
            this.slotDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.FillWeight = 40F;
            this.idDataGridViewTextBoxColumn.HeaderText = "号";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sizeDataGridViewTextBoxColumn
            // 
            this.sizeDataGridViewTextBoxColumn.DataPropertyName = "Size";
            this.sizeDataGridViewTextBoxColumn.FillWeight = 60F;
            this.sizeDataGridViewTextBoxColumn.HeaderText = "大小";
            this.sizeDataGridViewTextBoxColumn.Name = "sizeDataGridViewTextBoxColumn";
            this.sizeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.FillWeight = 80F;
            this.priceDataGridViewTextBoxColumn.HeaderText = "价格";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // addTimeDataGridViewTextBoxColumn
            // 
            this.addTimeDataGridViewTextBoxColumn.DataPropertyName = "AddTime";
            this.addTimeDataGridViewTextBoxColumn.FillWeight = 150F;
            this.addTimeDataGridViewTextBoxColumn.HeaderText = "首次记录时间";
            this.addTimeDataGridViewTextBoxColumn.Name = "addTimeDataGridViewTextBoxColumn";
            this.addTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // existenceTimeDataGridViewTextBoxColumn
            // 
            this.existenceTimeDataGridViewTextBoxColumn.DataPropertyName = "ExistenceTime";
            this.existenceTimeDataGridViewTextBoxColumn.FillWeight = 150F;
            this.existenceTimeDataGridViewTextBoxColumn.HeaderText = "最后记录时间";
            this.existenceTimeDataGridViewTextBoxColumn.Name = "existenceTimeDataGridViewTextBoxColumn";
            this.existenceTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // groupBoxUpload
            // 
            this.groupBoxUpload.Controls.Add(this.checkBoxML);
            this.groupBoxUpload.Controls.Add(this.textBoxUpload);
            this.groupBoxUpload.Controls.Add(this.checkBoxUpload);
            this.groupBoxUpload.Location = new System.Drawing.Point(562, 18);
            this.groupBoxUpload.Name = "groupBoxUpload";
            this.groupBoxUpload.Size = new System.Drawing.Size(321, 138);
            this.groupBoxUpload.TabIndex = 1;
            this.groupBoxUpload.TabStop = false;
            this.groupBoxUpload.Text = "上报设置";
            // 
            // checkBoxML
            // 
            this.checkBoxML.AutoSize = true;
            this.checkBoxML.Checked = true;
            this.checkBoxML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxML.Location = new System.Drawing.Point(114, 18);
            this.checkBoxML.Name = "checkBoxML";
            this.checkBoxML.Size = new System.Drawing.Size(78, 16);
            this.checkBoxML.TabIndex = 2;
            this.checkBoxML.Text = "只上报M/L";
            this.checkBoxML.UseVisualStyleBackColor = true;
            // 
            // textBoxUpload
            // 
            this.textBoxUpload.Enabled = false;
            this.textBoxUpload.Location = new System.Drawing.Point(7, 41);
            this.textBoxUpload.Multiline = true;
            this.textBoxUpload.Name = "textBoxUpload";
            this.textBoxUpload.ReadOnly = true;
            this.textBoxUpload.Size = new System.Drawing.Size(308, 91);
            this.textBoxUpload.TabIndex = 1;
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.Location = new System.Drawing.Point(7, 18);
            this.checkBoxUpload.Name = "checkBoxUpload";
            this.checkBoxUpload.Size = new System.Drawing.Size(96, 16);
            this.checkBoxUpload.TabIndex = 0;
            this.checkBoxUpload.Text = "开启自动上报";
            this.checkBoxUpload.UseVisualStyleBackColor = true;
            this.checkBoxUpload.CheckedChanged += new System.EventHandler(this.checkBoxUpload_CheckedChanged);
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.textBoxLog);
            this.groupBoxLog.Location = new System.Drawing.Point(563, 253);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(320, 367);
            this.groupBoxLog.TabIndex = 2;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "日志";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(7, 18);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(307, 343);
            this.textBoxLog.TabIndex = 0;
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Controls.Add(this.numericUpDownTimeout);
            this.groupBoxControl.Controls.Add(this.label1);
            this.groupBoxControl.Controls.Add(this.buttonCopyToClipboard);
            this.groupBoxControl.Controls.Add(this.buttonSaveToFile);
            this.groupBoxControl.Controls.Add(this.buttonUploadOnce);
            this.groupBoxControl.Location = new System.Drawing.Point(563, 162);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Size = new System.Drawing.Size(320, 85);
            this.groupBoxControl.TabIndex = 3;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "操作";
            // 
            // numericUpDownTimeout
            // 
            this.numericUpDownTimeout.Location = new System.Drawing.Point(205, 56);
            this.numericUpDownTimeout.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDownTimeout.Name = "numericUpDownTimeout";
            this.numericUpDownTimeout.Size = new System.Drawing.Size(48, 21);
            this.numericUpDownTimeout.TabIndex = 5;
            this.numericUpDownTimeout.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "最后记录早于此分钟数就不上报：";
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(216, 20);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(98, 23);
            this.buttonCopyToClipboard.TabIndex = 0;
            this.buttonCopyToClipboard.Text = "复制到剪贴板";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // buttonSaveToFile
            // 
            this.buttonSaveToFile.Location = new System.Drawing.Point(111, 20);
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.buttonSaveToFile.Size = new System.Drawing.Size(98, 23);
            this.buttonSaveToFile.TabIndex = 0;
            this.buttonSaveToFile.Text = "保存到文件";
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            // 
            // buttonUploadOnce
            // 
            this.buttonUploadOnce.Location = new System.Drawing.Point(6, 20);
            this.buttonUploadOnce.Name = "buttonUploadOnce";
            this.buttonUploadOnce.Size = new System.Drawing.Size(98, 23);
            this.buttonUploadOnce.TabIndex = 0;
            this.buttonUploadOnce.Text = "手动上报一次";
            this.buttonUploadOnce.UseVisualStyleBackColor = true;
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxControl);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.groupBoxUpload);
            this.Controls.Add(this.groupBoxTable);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(940, 655);
            this.groupBoxTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBoxUpload.ResumeLayout(false);
            this.groupBoxUpload.PerformLayout();
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
            this.groupBoxControl.ResumeLayout(false);
            this.groupBoxControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingCheckBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTable;
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
        public System.Windows.Forms.NumericUpDown numericUpDownTimeout;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.BindingSource housingCheckBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn slotDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn existenceTimeDataGridViewTextBoxColumn;
    }
}
