namespace CNC_Controls
{
    partial class FeedControl
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
            this.txtFeedRate = new System.Windows.Forms.TextBox();
            this.lblFeedRrate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRapids = new System.Windows.Forms.Label();
            this.feedOverrideControl = new CNC_Controls.OverrideControl();
            this.rapidsOverrideControl = new CNC_Controls.OverrideControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFeedRate
            // 
            this.txtFeedRate.Location = new System.Drawing.Point(6, 19);
            this.txtFeedRate.Name = "txtFeedRate";
            this.txtFeedRate.ReadOnly = true;
            this.txtFeedRate.Size = new System.Drawing.Size(67, 20);
            this.txtFeedRate.TabIndex = 4;
            // 
            // lblFeedRrate
            // 
            this.lblFeedRrate.AutoSize = true;
            this.lblFeedRrate.Location = new System.Drawing.Point(79, 22);
            this.lblFeedRrate.Name = "lblFeedRrate";
            this.lblFeedRrate.Size = new System.Drawing.Size(44, 13);
            this.lblFeedRrate.TabIndex = 3;
            this.lblFeedRrate.Text = "mm/min";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFeedRrate);
            this.groupBox1.Controls.Add(this.txtFeedRate);
            this.groupBox1.Controls.Add(this.feedOverrideControl);
            this.groupBox1.Controls.Add(this.lblRapids);
            this.groupBox1.Controls.Add(this.rapidsOverrideControl);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 152);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Feed rate";
            // 
            // lblRapids
            // 
            this.lblRapids.AutoSize = true;
            this.lblRapids.Location = new System.Drawing.Point(6, 92);
            this.lblRapids.Name = "lblRapids";
            this.lblRapids.Size = new System.Drawing.Size(43, 13);
            this.lblRapids.TabIndex = 8;
            this.lblRapids.Text = "Rapids:";
            // 
            // feedOverrideControl
            // 
            this.feedOverrideControl.Location = new System.Drawing.Point(30, 29);
            this.feedOverrideControl.Name = "feedOverrideControl";
            this.feedOverrideControl.Size = new System.Drawing.Size(174, 61);
            this.feedOverrideControl.TabIndex = 5;
            // 
            // rapidsOverrideControl
            // 
            this.rapidsOverrideControl.Location = new System.Drawing.Point(30, 92);
            this.rapidsOverrideControl.Name = "rapidsOverrideControl";
            this.rapidsOverrideControl.Size = new System.Drawing.Size(174, 54);
            this.rapidsOverrideControl.TabIndex = 6;
            // 
            // FeedControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "FeedControl";
            this.Size = new System.Drawing.Size(216, 159);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OverrideControl feedOverrideControl;
        private System.Windows.Forms.TextBox txtFeedRate;
        private System.Windows.Forms.Label lblFeedRrate;
        private System.Windows.Forms.GroupBox groupBox1;
        private OverrideControl rapidsOverrideControl;
        private System.Windows.Forms.Label lblRapids;
    }
}
