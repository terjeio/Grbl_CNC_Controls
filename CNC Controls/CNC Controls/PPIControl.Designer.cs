namespace CNC_Controls
{
    partial class PPIControl
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
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.lblmms = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.txtPulseWidths = new System.Windows.Forms.Label();
            this.tbrPPI = new System.Windows.Forms.TrackBar();
            this.lblTbr3 = new System.Windows.Forms.Label();
            this.txtPPI = new System.Windows.Forms.TextBox();
            this.txtPulseWidth = new System.Windows.Forms.TextBox();
            this.tbrPulseWidth = new System.Windows.Forms.TrackBar();
            this.lblTbr2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbrMaxPower = new System.Windows.Forms.TrackBar();
            this.txtPower = new System.Windows.Forms.TextBox();
            this.grpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrPPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrPulseWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMaxPower)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.txtSpeed);
            this.grpBox.Controls.Add(this.lblmms);
            this.grpBox.Controls.Add(this.lblSpeed);
            this.grpBox.Controls.Add(this.txtPulseWidths);
            this.grpBox.Controls.Add(this.tbrPPI);
            this.grpBox.Controls.Add(this.lblTbr3);
            this.grpBox.Controls.Add(this.txtPPI);
            this.grpBox.Controls.Add(this.txtPulseWidth);
            this.grpBox.Controls.Add(this.tbrPulseWidth);
            this.grpBox.Controls.Add(this.lblTbr2);
            this.grpBox.Controls.Add(this.groupBox2);
            this.grpBox.Location = new System.Drawing.Point(0, 0);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(213, 220);
            this.grpBox.TabIndex = 12;
            this.grpBox.TabStop = false;
            this.grpBox.Text = "Parameters";
            // 
            // txtSpeed
            // 
            this.txtSpeed.Enabled = false;
            this.txtSpeed.Location = new System.Drawing.Point(53, 23);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(32, 20);
            this.txtSpeed.TabIndex = 12;
            this.txtSpeed.Text = "---";
            // 
            // lblmms
            // 
            this.lblmms.AutoSize = true;
            this.lblmms.Location = new System.Drawing.Point(91, 26);
            this.lblmms.Name = "lblmms";
            this.lblmms.Size = new System.Drawing.Size(44, 13);
            this.lblmms.TabIndex = 30;
            this.lblmms.Text = "mm/min";
            this.lblmms.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(6, 26);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(41, 13);
            this.lblSpeed.TabIndex = 29;
            this.lblSpeed.Text = "Speed:";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPulseWidths
            // 
            this.txtPulseWidths.AutoSize = true;
            this.txtPulseWidths.Location = new System.Drawing.Point(70, 200);
            this.txtPulseWidths.Name = "txtPulseWidths";
            this.txtPulseWidths.Size = new System.Drawing.Size(67, 13);
            this.txtPulseWidths.TabIndex = 28;
            this.txtPulseWidths.Text = "x.xx ms/pixel";
            // 
            // tbrPPI
            // 
            this.tbrPPI.LargeChange = 100;
            this.tbrPPI.Location = new System.Drawing.Point(91, 68);
            this.tbrPPI.Maximum = 1200;
            this.tbrPPI.Minimum = 25;
            this.tbrPPI.Name = "tbrPPI";
            this.tbrPPI.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbrPPI.Size = new System.Drawing.Size(45, 104);
            this.tbrPPI.SmallChange = 50;
            this.tbrPPI.TabIndex = 21;
            this.tbrPPI.TabStop = false;
            this.tbrPPI.Value = 600;
            // 
            // lblTbr3
            // 
            this.lblTbr3.AutoSize = true;
            this.lblTbr3.Location = new System.Drawing.Point(133, 52);
            this.lblTbr3.Name = "lblTbr3";
            this.lblTbr3.Size = new System.Drawing.Size(67, 13);
            this.lblTbr3.TabIndex = 20;
            this.lblTbr3.Text = "Pulse Width:";
            // 
            // txtPPI
            // 
            this.txtPPI.Location = new System.Drawing.Point(91, 174);
            this.txtPPI.Name = "txtPPI";
            this.txtPPI.Size = new System.Drawing.Size(32, 20);
            this.txtPPI.TabIndex = 19;
            this.txtPPI.Text = "100%";
            // 
            // txtPulseWidth
            // 
            this.txtPulseWidth.Location = new System.Drawing.Point(142, 174);
            this.txtPulseWidth.Name = "txtPulseWidth";
            this.txtPulseWidth.Size = new System.Drawing.Size(32, 20);
            this.txtPulseWidth.TabIndex = 18;
            this.txtPulseWidth.Text = "100%";
            // 
            // tbrPulseWidth
            // 
            this.tbrPulseWidth.LargeChange = 50;
            this.tbrPulseWidth.Location = new System.Drawing.Point(142, 68);
            this.tbrPulseWidth.Maximum = 7000;
            this.tbrPulseWidth.Minimum = 700;
            this.tbrPulseWidth.Name = "tbrPulseWidth";
            this.tbrPulseWidth.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbrPulseWidth.Size = new System.Drawing.Size(45, 104);
            this.tbrPulseWidth.SmallChange = 10;
            this.tbrPulseWidth.TabIndex = 6;
            this.tbrPulseWidth.TabStop = false;
            this.tbrPulseWidth.TickFrequency = 10;
            this.tbrPulseWidth.Value = 2500;
            // 
            // lblTbr2
            // 
            this.lblTbr2.AutoSize = true;
            this.lblTbr2.Location = new System.Drawing.Point(88, 52);
            this.lblTbr2.Name = "lblTbr2";
            this.lblTbr2.Size = new System.Drawing.Size(27, 13);
            this.lblTbr2.TabIndex = 7;
            this.lblTbr2.Text = "PPI:";
            this.lblTbr2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbrMaxPower);
            this.groupBox2.Controls.Add(this.txtPower);
            this.groupBox2.Location = new System.Drawing.Point(4, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(60, 158);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Power:";
            // 
            // tbrMaxPower
            // 
            this.tbrMaxPower.Location = new System.Drawing.Point(6, 16);
            this.tbrMaxPower.Maximum = 100;
            this.tbrMaxPower.Minimum = 1;
            this.tbrMaxPower.Name = "tbrMaxPower";
            this.tbrMaxPower.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbrMaxPower.Size = new System.Drawing.Size(45, 104);
            this.tbrMaxPower.TabIndex = 8;
            this.tbrMaxPower.TabStop = false;
            this.tbrMaxPower.Value = 15;
            // 
            // txtPower
            // 
            this.txtPower.Location = new System.Drawing.Point(6, 122);
            this.txtPower.Name = "txtPower";
            this.txtPower.Size = new System.Drawing.Size(32, 20);
            this.txtPower.TabIndex = 11;
            this.txtPower.Text = "100%";
            // 
            // PPIControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBox);
            this.Name = "PPIControl";
            this.Size = new System.Drawing.Size(213, 220);
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrPPI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrPulseWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMaxPower)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBox;
        private System.Windows.Forms.Label txtPulseWidths;
        private System.Windows.Forms.TrackBar tbrMaxPower;
        private System.Windows.Forms.TrackBar tbrPPI;
        private System.Windows.Forms.Label lblTbr3;
        private System.Windows.Forms.TextBox txtPPI;
        private System.Windows.Forms.TextBox txtPulseWidth;
        private System.Windows.Forms.TextBox txtPower;
        private System.Windows.Forms.TrackBar tbrPulseWidth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTbr2;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.Label lblmms;
        private System.Windows.Forms.Label lblSpeed;
    }
}
