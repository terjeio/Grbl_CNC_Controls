namespace CNC_Camera_Control
{
    partial class CNCCameraControl
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
            this.trbCircle = new System.Windows.Forms.TrackBar();
            this.btnMoveOffset = new System.Windows.Forms.Button();
            this.VideoPanel = new System.Windows.Forms.Panel();
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.lblCircle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trbCircle)).BeginInit();
            this.VideoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // trbCircle
            // 
            this.trbCircle.Location = new System.Drawing.Point(441, 16);
            this.trbCircle.Maximum = 100;
            this.trbCircle.Minimum = 5;
            this.trbCircle.Name = "trbCircle";
            this.trbCircle.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbCircle.Size = new System.Drawing.Size(45, 124);
            this.trbCircle.TabIndex = 6;
            this.trbCircle.Value = 5;
            // 
            // btnMoveOffset
            // 
            this.btnMoveOffset.Location = new System.Drawing.Point(13, 323);
            this.btnMoveOffset.Name = "btnMoveOffset";
            this.btnMoveOffset.Size = new System.Drawing.Size(75, 23);
            this.btnMoveOffset.TabIndex = 5;
            this.btnMoveOffset.Text = "Move offset";
            this.btnMoveOffset.UseVisualStyleBackColor = true;
            // 
            // VideoPanel
            // 
            this.VideoPanel.Controls.Add(this.videoSourcePlayer);
            this.VideoPanel.Location = new System.Drawing.Point(13, 16);
            this.VideoPanel.Name = "VideoPanel";
            this.VideoPanel.Size = new System.Drawing.Size(406, 301);
            this.VideoPanel.TabIndex = 7;
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.AutoSizeControl = true;
            this.videoSourcePlayer.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.videoSourcePlayer.ForeColor = System.Drawing.Color.White;
            this.videoSourcePlayer.Location = new System.Drawing.Point(42, 29);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(322, 242);
            this.videoSourcePlayer.TabIndex = 2;
            this.videoSourcePlayer.VideoSource = null;
            // 
            // lblCircle
            // 
            this.lblCircle.AutoSize = true;
            this.lblCircle.Location = new System.Drawing.Point(438, 143);
            this.lblCircle.Name = "lblCircle";
            this.lblCircle.Size = new System.Drawing.Size(33, 13);
            this.lblCircle.TabIndex = 8;
            this.lblCircle.Text = "Circle";
            // 
            // CNCCameraControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCircle);
            this.Controls.Add(this.VideoPanel);
            this.Controls.Add(this.trbCircle);
            this.Controls.Add(this.btnMoveOffset);
            this.Name = "CNCCameraControl";
            this.Size = new System.Drawing.Size(530, 363);
            ((System.ComponentModel.ISupportInitialize)(this.trbCircle)).EndInit();
            this.VideoPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trbCircle;
        private System.Windows.Forms.Button btnMoveOffset;
        private System.Windows.Forms.Panel VideoPanel;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.Label lblCircle;
    }
}
