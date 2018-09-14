namespace CNC_Controls
{
    partial class GrblConfig
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
            this.GrblSettingsView = new System.Windows.Forms.DataGridView();
            this.SettingId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettingName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GrblSettingsView)).BeginInit();
            this.SuspendLayout();
            // 
            // GrblSettingsView
            // 
            this.GrblSettingsView.AllowDrop = true;
            this.GrblSettingsView.AllowUserToAddRows = false;
            this.GrblSettingsView.AllowUserToDeleteRows = false;
            this.GrblSettingsView.AllowUserToResizeRows = false;
            this.GrblSettingsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrblSettingsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SettingId,
            this.Value,
            this.Unit,
            this.SettingName});
            this.GrblSettingsView.Location = new System.Drawing.Point(13, 17);
            this.GrblSettingsView.MultiSelect = false;
            this.GrblSettingsView.Name = "GrblSettingsView";
            this.GrblSettingsView.ReadOnly = true;
            this.GrblSettingsView.RowHeadersVisible = false;
            this.GrblSettingsView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GrblSettingsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GrblSettingsView.Size = new System.Drawing.Size(392, 485);
            this.GrblSettingsView.TabIndex = 1;
            // 
            // SettingId
            // 
            this.SettingId.DataPropertyName = "Id";
            this.SettingId.HeaderText = "ID";
            this.SettingId.Name = "SettingId";
            this.SettingId.ReadOnly = true;
            this.SettingId.Width = 35;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            dataGridViewCellStyle1.NullValue = null;
            this.Value.DefaultCellStyle = dataGridViewCellStyle1;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Value.Width = 90;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Unit.Width = 75;
            // 
            // SettingName
            // 
            this.SettingName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SettingName.DataPropertyName = "Name";
            this.SettingName.HeaderText = "Name";
            this.SettingName.Name = "SettingName";
            this.SettingName.ReadOnly = true;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Location = new System.Drawing.Point(446, 124);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(402, 60);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.TabStop = false;
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(411, 479);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(492, 479);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(680, 479);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 23);
            this.btnBackup.TabIndex = 5;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            // 
            // GrblConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.GrblSettingsView);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "GrblConfig";
            this.Size = new System.Drawing.Size(875, 515);
            ((System.ComponentModel.ISupportInitialize)(this.GrblSettingsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GrblSettingsView;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettingId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettingName;
        private System.Windows.Forms.Button btnBackup;
    }
}
