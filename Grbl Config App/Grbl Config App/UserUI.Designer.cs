namespace Grbl_Config_App
{
    partial class UserUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grblConfig = new CNC_Controls.GrblConfig();
            this.SuspendLayout();
            // 
            // grblConfig
            // 
            this.grblConfig.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grblConfig.Location = new System.Drawing.Point(9, 9);
            this.grblConfig.Margin = new System.Windows.Forms.Padding(0);
            this.grblConfig.Name = "grblConfig";
            this.grblConfig.Size = new System.Drawing.Size(875, 515);
            this.grblConfig.TabIndex = 0;
            // 
            // UserUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 529);
            this.Controls.Add(this.grblConfig);
            this.Name = "UserUI";
            this.Text = "Grbl Configuration App";
            this.ResumeLayout(false);

        }

        #endregion

        private CNC_Controls.GrblConfig grblConfig;
    }
}

