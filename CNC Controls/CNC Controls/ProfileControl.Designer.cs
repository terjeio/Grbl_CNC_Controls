namespace CNC_Controls
{
    partial class ProfileControl
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
            this.btnProfile = new System.Windows.Forms.Button();
            this.cmbProfile = new System.Windows.Forms.ComboBox();
            this.lblProfile = new System.Windows.Forms.Label();
            this.mnuProfileContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mitAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mitUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mitDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProfileContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProfile
            // 
            this.btnProfile.Location = new System.Drawing.Point(196, 3);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(24, 23);
            this.btnProfile.TabIndex = 29;
            this.btnProfile.Text = "...";
            this.btnProfile.UseVisualStyleBackColor = true;
            // 
            // cmbProfile
            // 
            this.cmbProfile.FormattingEnabled = true;
            this.cmbProfile.Items.AddRange(new object[] {
            "<default>"});
            this.cmbProfile.Location = new System.Drawing.Point(42, 3);
            this.cmbProfile.Name = "cmbProfile";
            this.cmbProfile.Size = new System.Drawing.Size(148, 21);
            this.cmbProfile.TabIndex = 28;
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Location = new System.Drawing.Point(4, 6);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(39, 13);
            this.lblProfile.TabIndex = 27;
            this.lblProfile.Text = "Profile:";
            // 
            // mnuProfileContext
            // 
            this.mnuProfileContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitAdd,
            this.mitUpdate,
            this.mitDelete});
            this.mnuProfileContext.Name = "mnuProfileContext";
            this.mnuProfileContext.Size = new System.Drawing.Size(113, 70);
            // 
            // mitAdd
            // 
            this.mitAdd.Name = "mitAdd";
            this.mitAdd.Size = new System.Drawing.Size(112, 22);
            this.mitAdd.Text = "Add";
            // 
            // mitUpdate
            // 
            this.mitUpdate.Name = "mitUpdate";
            this.mitUpdate.Size = new System.Drawing.Size(112, 22);
            this.mitUpdate.Text = "Update";
            // 
            // mitDelete
            // 
            this.mitDelete.Name = "mitDelete";
            this.mitDelete.Size = new System.Drawing.Size(112, 22);
            this.mitDelete.Text = "Delete";
            // 
            // ProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.cmbProfile);
            this.Controls.Add(this.lblProfile);
            this.Name = "ProfileControl";
            this.Size = new System.Drawing.Size(225, 36);
            this.mnuProfileContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.ComboBox cmbProfile;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.ContextMenuStrip mnuProfileContext;
        private System.Windows.Forms.ToolStripMenuItem mitAdd;
        private System.Windows.Forms.ToolStripMenuItem mitUpdate;
        private System.Windows.Forms.ToolStripMenuItem mitDelete;
    }
}
