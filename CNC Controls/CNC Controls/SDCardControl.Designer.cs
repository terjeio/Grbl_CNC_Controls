namespace CNC_Controls
{
    partial class SDCardControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GrblFileView = new System.Windows.Forms.DataGridView();
            this.SettingName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GrblFileView)).BeginInit();
            this.SuspendLayout();
            // 
            // GrblFileView
            // 
            this.GrblFileView.AllowUserToAddRows = false;
            this.GrblFileView.AllowUserToDeleteRows = false;
            this.GrblFileView.AllowUserToResizeRows = false;
            this.GrblFileView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrblFileView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SettingName,
            this.Value,
            this.Unit});
            this.GrblFileView.Location = new System.Drawing.Point(14, 14);
            this.GrblFileView.MultiSelect = false;
            this.GrblFileView.Name = "GrblFileView";
            this.GrblFileView.ReadOnly = true;
            this.GrblFileView.RowHeadersVisible = false;
            this.GrblFileView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GrblFileView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GrblFileView.Size = new System.Drawing.Size(392, 485);
            this.GrblFileView.TabIndex = 2;
            // 
            // SettingName
            // 
            this.SettingName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SettingName.DataPropertyName = "Name";
            this.SettingName.HeaderText = "Name";
            this.SettingName.Name = "SettingName";
            this.SettingName.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Size";
            dataGridViewCellStyle1.NullValue = null;
            this.Value.DefaultCellStyle = dataGridViewCellStyle1;
            this.Value.HeaderText = "Size";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Value.Width = 90;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Invalid";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Unit.Width = 75;
            // 
            // SDCardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.GrblFileView);
            this.Name = "SDCardControl";
            this.Size = new System.Drawing.Size(871, 511);
            ((System.ComponentModel.ISupportInitialize)(this.GrblFileView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GrblFileView;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettingName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
    }
}
