
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
            this.groupBoxTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingCheckBindingSource)).BeginInit();
            this.groupBoxUpload.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTable
            // 
            this.groupBoxTable.Controls.Add(this.dataGridView1);
            this.groupBoxTable.Location = new System.Drawing.Point(18, 20);
            this.groupBoxTable.Name = "groupBoxTable";
            this.groupBoxTable.Size = new System.Drawing.Size(661, 600);
            this.groupBoxTable.TabIndex = 0;
            this.groupBoxTable.TabStop = false;
            this.groupBoxTable.Text = "在售列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(648, 574);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBoxUpload
            // 
            this.groupBoxUpload.Controls.Add(this.checkBoxML);
            this.groupBoxUpload.Controls.Add(this.textBoxUpload);
            this.groupBoxUpload.Controls.Add(this.checkBoxUpload);
            this.groupBoxUpload.Location = new System.Drawing.Point(685, 20);
            this.groupBoxUpload.Name = "groupBoxUpload";
            this.groupBoxUpload.Size = new System.Drawing.Size(321, 164);
            this.groupBoxUpload.TabIndex = 1;
            this.groupBoxUpload.TabStop = false;
            this.groupBoxUpload.Text = "上报设置";
            // 
            // checkBoxML
            // 
            this.checkBoxML.AutoSize = true;
            this.checkBoxML.Checked = true;
            this.checkBoxML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxML.Location = new System.Drawing.Point(104, 20);
            this.checkBoxML.Name = "checkBoxML";
            this.checkBoxML.Size = new System.Drawing.Size(82, 17);
            this.checkBoxML.TabIndex = 2;
            this.checkBoxML.Text = "只上报M/L";
            this.checkBoxML.UseVisualStyleBackColor = true;
            // 
            // textBoxUpload
            // 
            this.textBoxUpload.Enabled = false;
            this.textBoxUpload.Location = new System.Drawing.Point(7, 44);
            this.textBoxUpload.Multiline = true;
            this.textBoxUpload.Name = "textBoxUpload";
            this.textBoxUpload.ReadOnly = true;
            this.textBoxUpload.Size = new System.Drawing.Size(308, 114);
            this.textBoxUpload.TabIndex = 1;
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.Location = new System.Drawing.Point(7, 20);
            this.checkBoxUpload.Name = "checkBoxUpload";
            this.checkBoxUpload.Size = new System.Drawing.Size(74, 17);
            this.checkBoxUpload.TabIndex = 0;
            this.checkBoxUpload.Text = "开启上报";
            this.checkBoxUpload.UseVisualStyleBackColor = true;
            this.checkBoxUpload.CheckedChanged += new System.EventHandler(this.checkBoxUpload_CheckedChanged);
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.textBoxLog);
            this.groupBoxLog.Location = new System.Drawing.Point(686, 191);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(320, 429);
            this.groupBoxLog.TabIndex = 2;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "日志";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(7, 20);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(307, 403);
            this.textBoxLog.TabIndex = 0;
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.groupBoxUpload);
            this.Controls.Add(this.groupBoxTable);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(1075, 651);
            this.groupBoxTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingCheckBindingSource)).EndInit();
            this.groupBoxUpload.ResumeLayout(false);
            this.groupBoxUpload.PerformLayout();
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
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
    }
}
