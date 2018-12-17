namespace CNC_Controls
{
    partial class SpindleControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.overrideControl = new CNC_Controls.OverrideControl();
            this.txtRPM = new System.Windows.Forms.TextBox();
            this.lblRPM = new System.Windows.Forms.Label();
            this.rbSpindleCCW = new System.Windows.Forms.RadioButton();
            this.rbSpindleCW = new System.Windows.Forms.RadioButton();
            this.rbSpindleOff = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.overrideControl);
            this.groupBox1.Controls.Add(this.txtRPM);
            this.groupBox1.Controls.Add(this.lblRPM);
            this.groupBox1.Controls.Add(this.rbSpindleCCW);
            this.groupBox1.Controls.Add(this.rbSpindleCW);
            this.groupBox1.Controls.Add(this.rbSpindleOff);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 130);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spindle";
            // 
            // overrideControl
            // 
            this.overrideControl.Location = new System.Drawing.Point(30, 61);
            this.overrideControl.Name = "overrideControl";
            this.overrideControl.Size = new System.Drawing.Size(174, 61);
            this.overrideControl.TabIndex = 5;
            // 
            // txtRPM
            // 
            this.txtRPM.Location = new System.Drawing.Point(48, 42);
            this.txtRPM.Name = "txtRPM";
            this.txtRPM.Size = new System.Drawing.Size(67, 20);
            this.txtRPM.TabIndex = 0;
            // 
            // lblRPM
            // 
            this.lblRPM.AutoSize = true;
            this.lblRPM.Location = new System.Drawing.Point(11, 45);
            this.lblRPM.Name = "lblRPM";
            this.lblRPM.Size = new System.Drawing.Size(31, 13);
            this.lblRPM.TabIndex = 3;
            this.lblRPM.Text = "RPM";
            // 
            // rbSpindleCCW
            // 
            this.rbSpindleCCW.AutoSize = true;
            this.rbSpindleCCW.Location = new System.Drawing.Point(112, 19);
            this.rbSpindleCCW.Name = "rbSpindleCCW";
            this.rbSpindleCCW.Size = new System.Drawing.Size(50, 17);
            this.rbSpindleCCW.TabIndex = 0;
            this.rbSpindleCCW.Text = "CCW";
            this.rbSpindleCCW.UseVisualStyleBackColor = true;
            // 
            // rbSpindleCW
            // 
            this.rbSpindleCW.AutoSize = true;
            this.rbSpindleCW.Location = new System.Drawing.Point(62, 19);
            this.rbSpindleCW.Name = "rbSpindleCW";
            this.rbSpindleCW.Size = new System.Drawing.Size(43, 17);
            this.rbSpindleCW.TabIndex = 0;
            this.rbSpindleCW.Text = "CW";
            this.rbSpindleCW.UseVisualStyleBackColor = true;
            // 
            // rbSpindleOff
            // 
            this.rbSpindleOff.AutoSize = true;
            this.rbSpindleOff.Location = new System.Drawing.Point(16, 19);
            this.rbSpindleOff.Name = "rbSpindleOff";
            this.rbSpindleOff.Size = new System.Drawing.Size(39, 17);
            this.rbSpindleOff.TabIndex = 0;
            this.rbSpindleOff.Text = "Off";
            this.rbSpindleOff.UseVisualStyleBackColor = true;
            // 
            // SpindleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SpindleControl";
            this.Size = new System.Drawing.Size(216, 136);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSpindleOff;
        private System.Windows.Forms.Label lblRPM;
        private System.Windows.Forms.RadioButton rbSpindleCCW;
        private System.Windows.Forms.RadioButton rbSpindleCW;
        private System.Windows.Forms.TextBox txtRPM;
        private OverrideControl overrideControl;
    }
}
