namespace CNC_Controls
{
    partial class DPIControl
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
            this.txtPulseWidth = new System.Windows.Forms.Label();
            this.lblPwrMax = new System.Windows.Forms.Label();
            this.lblPwrMin = new System.Windows.Forms.Label();
            this.tbrMaxPower = new System.Windows.Forms.TrackBar();
            this.txtMinPower = new System.Windows.Forms.TextBox();
            this.tbrMinPower = new System.Windows.Forms.TrackBar();
            this.tbrDutyCycle = new System.Windows.Forms.TrackBar();
            this.lblTbr3 = new System.Windows.Forms.Label();
            this.txtDutyCycle = new System.Windows.Forms.TextBox();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.tbrSpeed = new System.Windows.Forms.TrackBar();
            this.txtMaxPower = new System.Windows.Forms.TextBox();
            this.lblTbr2 = new System.Windows.Forms.Label();
            this.lblDPI = new System.Windows.Forms.Label();
            this.cmbDPI = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMaxPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMinPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrDutyCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSpeed)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.txtPulseWidth);
            this.grpBox.Controls.Add(this.lblPwrMax);
            this.grpBox.Controls.Add(this.lblPwrMin);
            this.grpBox.Controls.Add(this.tbrDutyCycle);
            this.grpBox.Controls.Add(this.lblTbr3);
            this.grpBox.Controls.Add(this.txtDutyCycle);
            this.grpBox.Controls.Add(this.txtSpeed);
            this.grpBox.Controls.Add(this.tbrSpeed);
            this.grpBox.Controls.Add(this.txtMaxPower);
            this.grpBox.Controls.Add(this.lblTbr2);
            this.grpBox.Controls.Add(this.lblDPI);
            this.grpBox.Controls.Add(this.cmbDPI);
            this.grpBox.Controls.Add(this.groupBox2);
            this.grpBox.Location = new System.Drawing.Point(0, 0);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(213, 213);
            this.grpBox.TabIndex = 12;
            this.grpBox.TabStop = false;
            this.grpBox.Text = "Parameters";
            // 
            // txtPulseWidth
            // 
            this.txtPulseWidth.AutoSize = true;
            this.txtPulseWidth.Location = new System.Drawing.Point(96, 192);
            this.txtPulseWidth.Name = "txtPulseWidth";
            this.txtPulseWidth.Size = new System.Drawing.Size(67, 13);
            this.txtPulseWidth.TabIndex = 28;
            this.txtPulseWidth.Text = "x.xx ms/pixel";
            // 
            // lblPwrMax
            // 
            this.lblPwrMax.AutoSize = true;
            this.lblPwrMax.Location = new System.Drawing.Point(42, 192);
            this.lblPwrMax.Name = "lblPwrMax";
            this.lblPwrMax.Size = new System.Drawing.Size(27, 13);
            this.lblPwrMax.TabIndex = 26;
            this.lblPwrMax.Text = "Max";
            // 
            // lblPwrMin
            // 
            this.lblPwrMin.AutoSize = true;
            this.lblPwrMin.Location = new System.Drawing.Point(5, 192);
            this.lblPwrMin.Name = "lblPwrMin";
            this.lblPwrMin.Size = new System.Drawing.Size(24, 13);
            this.lblPwrMin.TabIndex = 25;
            this.lblPwrMin.Text = "Min";
            // 
            // tbrMaxPower
            // 
            this.tbrMaxPower.Location = new System.Drawing.Point(39, 16);
            this.tbrMaxPower.Maximum = 100;
            this.tbrMaxPower.Minimum = 1;
            this.tbrMaxPower.Name = "tbrMaxPower";
            this.tbrMaxPower.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbrMaxPower.Size = new System.Drawing.Size(45, 104);
            this.tbrMaxPower.TabIndex = 8;
            this.tbrMaxPower.TabStop = false;
            this.tbrMaxPower.Value = 15;
            // 
            // txtMinPower
            // 
            this.txtMinPower.Location = new System.Drawing.Point(1, 117);
            this.txtMinPower.Name = "txtMinPower";
            this.txtMinPower.Size = new System.Drawing.Size(32, 20);
            this.txtMinPower.TabIndex = 24;
            this.txtMinPower.Text = "100%";
            // 
            // tbrMinPower
            // 
            this.tbrMinPower.Location = new System.Drawing.Point(2, 16);
            this.tbrMinPower.Maximum = 99;
            this.tbrMinPower.Name = "tbrMinPower";
            this.tbrMinPower.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbrMinPower.Size = new System.Drawing.Size(45, 104);
            this.tbrMinPower.TabIndex = 23;
            this.tbrMinPower.TabStop = false;
            this.tbrMinPower.Value = 15;
            // 
            // tbrDutyCycle
            // 
            this.tbrDutyCycle.LargeChange = 10;
            this.tbrDutyCycle.Location = new System.Drawing.Point(98, 68);
            this.tbrDutyCycle.Maximum = 100;
            this.tbrDutyCycle.Minimum = 25;
            this.tbrDutyCycle.Name = "tbrDutyCycle";
            this.tbrDutyCycle.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbrDutyCycle.Size = new System.Drawing.Size(45, 104);
            this.tbrDutyCycle.SmallChange = 50;
            this.tbrDutyCycle.TabIndex = 22;
            this.tbrDutyCycle.TabStop = false;
            this.tbrDutyCycle.Value = 30;
            // 
            // lblTbr3
            // 
            this.lblTbr3.AutoSize = true;
            this.lblTbr3.Location = new System.Drawing.Point(159, 52);
            this.lblTbr3.Name = "lblTbr3";
            this.lblTbr3.Size = new System.Drawing.Size(41, 13);
            this.lblTbr3.TabIndex = 20;
            this.lblTbr3.Text = "Speed:";
            // 
            // txtDutyCycle
            // 
            this.txtDutyCycle.Location = new System.Drawing.Point(99, 169);
            this.txtDutyCycle.Name = "txtDutyCycle";
            this.txtDutyCycle.Size = new System.Drawing.Size(32, 20);
            this.txtDutyCycle.TabIndex = 19;
            this.txtDutyCycle.Text = "100%";
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(162, 169);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(32, 20);
            this.txtSpeed.TabIndex = 18;
            this.txtSpeed.Text = "100%";
            // 
            // tbrSpeed
            // 
            this.tbrSpeed.LargeChange = 10;
            this.tbrSpeed.Location = new System.Drawing.Point(162, 68);
            this.tbrSpeed.Maximum = 100;
            this.tbrSpeed.Minimum = 10;
            this.tbrSpeed.Name = "tbrSpeed";
            this.tbrSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbrSpeed.Size = new System.Drawing.Size(45, 104);
            this.tbrSpeed.SmallChange = 10;
            this.tbrSpeed.TabIndex = 12;
            this.tbrSpeed.Value = 10;
            // 
            // txtMaxPower
            // 
            this.txtMaxPower.Location = new System.Drawing.Point(43, 169);
            this.txtMaxPower.Name = "txtMaxPower";
            this.txtMaxPower.Size = new System.Drawing.Size(32, 20);
            this.txtMaxPower.TabIndex = 11;
            this.txtMaxPower.Text = "100%";
            // 
            // lblTbr2
            // 
            this.lblTbr2.Location = new System.Drawing.Point(95, 52);
            this.lblTbr2.Name = "lblTbr2";
            this.lblTbr2.Size = new System.Drawing.Size(58, 13);
            this.lblTbr2.TabIndex = 7;
            this.lblTbr2.Text = "DutyCycle:";
            this.lblTbr2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDPI
            // 
            this.lblDPI.AutoSize = true;
            this.lblDPI.Location = new System.Drawing.Point(12, 22);
            this.lblDPI.Name = "lblDPI";
            this.lblDPI.Size = new System.Drawing.Size(28, 13);
            this.lblDPI.TabIndex = 17;
            this.lblDPI.Text = "DPI:";
            // 
            // cmbDPI
            // 
            this.cmbDPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDPI.Enabled = false;
            this.cmbDPI.FormattingEnabled = true;
            this.cmbDPI.Items.AddRange(new object[] {
            "75",
            "100",
            "150",
            "200",
            "300",
            "400",
            "600",
            "1200"});
            this.cmbDPI.Location = new System.Drawing.Point(41, 19);
            this.cmbDPI.Name = "cmbDPI";
            this.cmbDPI.Size = new System.Drawing.Size(72, 21);
            this.cmbDPI.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMinPower);
            this.groupBox2.Controls.Add(this.tbrMaxPower);
            this.groupBox2.Controls.Add(this.tbrMinPower);
            this.groupBox2.Location = new System.Drawing.Point(4, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(86, 158);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Power:";
            // 
            // DPIControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBox);
            this.Name = "DPIControl";
            this.Size = new System.Drawing.Size(213, 213);
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMaxPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMinPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrDutyCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSpeed)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBox;
        private System.Windows.Forms.Label txtPulseWidth;
        private System.Windows.Forms.TrackBar tbrMaxPower;
        private System.Windows.Forms.TrackBar tbrDutyCycle;
        private System.Windows.Forms.Label lblTbr3;
        private System.Windows.Forms.TextBox txtDutyCycle;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.TrackBar tbrSpeed;
        private System.Windows.Forms.TextBox txtMaxPower;
        private System.Windows.Forms.Label lblTbr2;
        private System.Windows.Forms.Label lblDPI;
        private System.Windows.Forms.ComboBox cmbDPI;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblPwrMax;
        private System.Windows.Forms.Label lblPwrMin;
        private System.Windows.Forms.TextBox txtMinPower;
        private System.Windows.Forms.TrackBar tbrMinPower;
    }
}
