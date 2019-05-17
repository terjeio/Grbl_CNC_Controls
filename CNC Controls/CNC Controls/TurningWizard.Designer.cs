namespace CNC_Controls
{
    partial class TurningWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TurningWizard));
            this.btnCalculate = new System.Windows.Forms.Button();
            this.txtGCode = new System.Windows.Forms.TextBox();
            this.chkCSS = new System.Windows.Forms.CheckBox();
            this.chkSpringPasses = new System.Windows.Forms.CheckBox();
            this.txtSpringPasses = new System.Windows.Forms.NumericUpDown();
            this.lblSpindleRPM = new System.Windows.Forms.Label();
            this.unitSpindleRPM = new System.Windows.Forms.Label();
            this.txtSpindleRPM = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkTaper = new System.Windows.Forms.CheckBox();
            this.lblTaperUnit = new System.Windows.Forms.Label();
            this.txtTaper = new System.Windows.Forms.TextBox();
            this.cvDiameter = new CNC_Controls.CoordValueControl();
            this.cvTargetX = new CNC_Controls.CoordValueControl();
            this.cvClearanceX = new CNC_Controls.CoordValueControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cvPassDepth = new CNC_Controls.CoordValueControl();
            this.cvFeedRateLast = new CNC_Controls.CoordValueControl();
            this.cvPassDepthLast = new CNC_Controls.CoordValueControl();
            this.cvFeedRate = new CNC_Controls.CoordValueControl();
            this.btnOptions = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxProfile = new System.Windows.Forms.ComboBox();
            this.cvLength = new CNC_Controls.CoordValueControl();
            this.cvStart = new CNC_Controls.CoordValueControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpringPasses)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(23, 480);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            // 
            // txtGCode
            // 
            this.txtGCode.Location = new System.Drawing.Point(23, 338);
            this.txtGCode.Multiline = true;
            this.txtGCode.Name = "txtGCode";
            this.txtGCode.ReadOnly = true;
            this.txtGCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGCode.Size = new System.Drawing.Size(468, 136);
            this.txtGCode.TabIndex = 14;
            // 
            // chkCSS
            // 
            this.chkCSS.AutoSize = true;
            this.chkCSS.Location = new System.Drawing.Point(23, 307);
            this.chkCSS.Name = "chkCSS";
            this.chkCSS.Size = new System.Drawing.Size(175, 17);
            this.chkCSS.TabIndex = 16;
            this.chkCSS.Text = "CSS (Constand Surface Speed)";
            this.chkCSS.UseVisualStyleBackColor = true;
            // 
            // chkSpringPasses
            // 
            this.chkSpringPasses.AutoSize = true;
            this.chkSpringPasses.Location = new System.Drawing.Point(364, 31);
            this.chkSpringPasses.Name = "chkSpringPasses";
            this.chkSpringPasses.Size = new System.Drawing.Size(95, 17);
            this.chkSpringPasses.TabIndex = 68;
            this.chkSpringPasses.Text = "Spring passes:";
            this.chkSpringPasses.UseVisualStyleBackColor = true;
            // 
            // txtSpringPasses
            // 
            this.txtSpringPasses.Enabled = false;
            this.txtSpringPasses.Location = new System.Drawing.Point(465, 30);
            this.txtSpringPasses.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtSpringPasses.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtSpringPasses.Name = "txtSpringPasses";
            this.txtSpringPasses.ReadOnly = true;
            this.txtSpringPasses.Size = new System.Drawing.Size(50, 20);
            this.txtSpringPasses.TabIndex = 66;
            this.txtSpringPasses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSpringPasses.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSpindleRPM
            // 
            this.lblSpindleRPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpindleRPM.Location = new System.Drawing.Point(212, 308);
            this.lblSpindleRPM.Name = "lblSpindleRPM";
            this.lblSpindleRPM.Size = new System.Drawing.Size(45, 13);
            this.lblSpindleRPM.TabIndex = 71;
            this.lblSpindleRPM.Text = "Spindle:";
            this.lblSpindleRPM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // unitSpindleRPM
            // 
            this.unitSpindleRPM.AutoSize = true;
            this.unitSpindleRPM.Location = new System.Drawing.Point(319, 308);
            this.unitSpindleRPM.Name = "unitSpindleRPM";
            this.unitSpindleRPM.Size = new System.Drawing.Size(31, 13);
            this.unitSpindleRPM.TabIndex = 70;
            this.unitSpindleRPM.Text = "RPM";
            // 
            // txtSpindleRPM
            // 
            this.txtSpindleRPM.Location = new System.Drawing.Point(263, 305);
            this.txtSpindleRPM.Name = "txtSpindleRPM";
            this.txtSpindleRPM.Size = new System.Drawing.Size(50, 20);
            this.txtSpindleRPM.TabIndex = 69;
            this.txtSpindleRPM.Text = "300";
            this.txtSpindleRPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkTaper);
            this.groupBox1.Controls.Add(this.lblTaperUnit);
            this.groupBox1.Controls.Add(this.txtTaper);
            this.groupBox1.Controls.Add(this.cvDiameter);
            this.groupBox1.Controls.Add(this.cvTargetX);
            this.groupBox1.Controls.Add(this.cvClearanceX);
            this.groupBox1.Location = new System.Drawing.Point(23, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 85);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Diameter";
            // 
            // chkTaper
            // 
            this.chkTaper.AutoSize = true;
            this.chkTaper.Location = new System.Drawing.Point(34, 59);
            this.chkTaper.Name = "chkTaper";
            this.chkTaper.Size = new System.Drawing.Size(57, 17);
            this.chkTaper.TabIndex = 74;
            this.chkTaper.Text = "Taper:";
            this.chkTaper.UseVisualStyleBackColor = true;
            // 
            // lblTaperUnit
            // 
            this.lblTaperUnit.AutoSize = true;
            this.lblTaperUnit.Location = new System.Drawing.Point(153, 59);
            this.lblTaperUnit.Name = "lblTaperUnit";
            this.lblTaperUnit.Size = new System.Drawing.Size(25, 13);
            this.lblTaperUnit.TabIndex = 57;
            this.lblTaperUnit.Text = "deg";
            // 
            // txtTaper
            // 
            this.txtTaper.Location = new System.Drawing.Point(97, 56);
            this.txtTaper.Name = "txtTaper";
            this.txtTaper.ReadOnly = true;
            this.txtTaper.Size = new System.Drawing.Size(50, 20);
            this.txtTaper.TabIndex = 56;
            this.txtTaper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cvDiameter
            // 
            this.cvDiameter.Label = "Current:";
            this.cvDiameter.Location = new System.Drawing.Point(6, 30);
            this.cvDiameter.Metric = true;
            this.cvDiameter.Name = "cvDiameter";
            this.cvDiameter.Size = new System.Drawing.Size(175, 20);
            this.cvDiameter.TabIndex = 17;
            this.cvDiameter.Unit = "mm";
            this.cvDiameter.Value = 12D;
            // 
            // cvTargetX
            // 
            this.cvTargetX.Label = "Target:";
            this.cvTargetX.Location = new System.Drawing.Point(171, 30);
            this.cvTargetX.Metric = true;
            this.cvTargetX.Name = "cvTargetX";
            this.cvTargetX.Size = new System.Drawing.Size(175, 20);
            this.cvTargetX.TabIndex = 2;
            this.cvTargetX.Unit = "mm";
            this.cvTargetX.Value = 10D;
            // 
            // cvClearanceX
            // 
            this.cvClearanceX.Label = "Clearance:";
            this.cvClearanceX.Location = new System.Drawing.Point(340, 30);
            this.cvClearanceX.Metric = true;
            this.cvClearanceX.Name = "cvClearanceX";
            this.cvClearanceX.Size = new System.Drawing.Size(175, 20);
            this.cvClearanceX.TabIndex = 15;
            this.cvClearanceX.Unit = "mm";
            this.cvClearanceX.Value = 1D;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cvPassDepth);
            this.groupBox2.Controls.Add(this.cvFeedRateLast);
            this.groupBox2.Controls.Add(this.txtSpringPasses);
            this.groupBox2.Controls.Add(this.cvPassDepthLast);
            this.groupBox2.Controls.Add(this.chkSpringPasses);
            this.groupBox2.Controls.Add(this.cvFeedRate);
            this.groupBox2.Location = new System.Drawing.Point(23, 204);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(525, 85);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cut depths and feed rates";
            // 
            // cvPassDepth
            // 
            this.cvPassDepth.Label = "Pass depth:";
            this.cvPassDepth.Location = new System.Drawing.Point(6, 30);
            this.cvPassDepth.Metric = true;
            this.cvPassDepth.Name = "cvPassDepth";
            this.cvPassDepth.Size = new System.Drawing.Size(175, 20);
            this.cvPassDepth.TabIndex = 3;
            this.cvPassDepth.Unit = "mm";
            this.cvPassDepth.Value = 0.2D;
            // 
            // cvFeedRateLast
            // 
            this.cvFeedRateLast.Label = "Feed rate:";
            this.cvFeedRateLast.Location = new System.Drawing.Point(171, 56);
            this.cvFeedRateLast.Metric = true;
            this.cvFeedRateLast.Name = "cvFeedRateLast";
            this.cvFeedRateLast.Size = new System.Drawing.Size(175, 20);
            this.cvFeedRateLast.TabIndex = 77;
            this.cvFeedRateLast.Unit = "mm/min";
            this.cvFeedRateLast.Value = 100D;
            // 
            // cvPassDepthLast
            // 
            this.cvPassDepthLast.Label = "Last pass:";
            this.cvPassDepthLast.Location = new System.Drawing.Point(6, 56);
            this.cvPassDepthLast.Metric = true;
            this.cvPassDepthLast.Name = "cvPassDepthLast";
            this.cvPassDepthLast.Size = new System.Drawing.Size(175, 20);
            this.cvPassDepthLast.TabIndex = 76;
            this.cvPassDepthLast.Unit = "mm";
            this.cvPassDepthLast.Value = 0.05D;
            // 
            // cvFeedRate
            // 
            this.cvFeedRate.Label = "Feed rate:";
            this.cvFeedRate.Location = new System.Drawing.Point(171, 31);
            this.cvFeedRate.Metric = true;
            this.cvFeedRate.Name = "cvFeedRate";
            this.cvFeedRate.Size = new System.Drawing.Size(175, 20);
            this.cvFeedRate.TabIndex = 72;
            this.cvFeedRate.Unit = "mm/min";
            this.cvFeedRate.Value = 300D;
            // 
            // btnOptions
            // 
            this.btnOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOptions.Image")));
            this.btnOptions.Location = new System.Drawing.Point(365, 14);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(40, 23);
            this.btnOptions.TabIndex = 79;
            this.btnOptions.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 80;
            this.label1.Text = "Profile:";
            // 
            // cbxProfile
            // 
            this.cbxProfile.FormattingEnabled = true;
            this.cbxProfile.Location = new System.Drawing.Point(59, 15);
            this.cbxProfile.Name = "cbxProfile";
            this.cbxProfile.Size = new System.Drawing.Size(300, 21);
            this.cbxProfile.TabIndex = 81;
            // 
            // cvLength
            // 
            this.cvLength.Label = "Length:";
            this.cvLength.Location = new System.Drawing.Point(160, 69);
            this.cvLength.Metric = true;
            this.cvLength.Name = "cvLength";
            this.cvLength.Size = new System.Drawing.Size(175, 20);
            this.cvLength.TabIndex = 1;
            this.cvLength.Unit = "mm";
            this.cvLength.Value = 10D;
            // 
            // cvStart
            // 
            this.cvStart.Label = "StartZ:";
            this.cvStart.Location = new System.Drawing.Point(3, 69);
            this.cvStart.Metric = true;
            this.cvStart.Name = "cvStart";
            this.cvStart.Size = new System.Drawing.Size(175, 20);
            this.cvStart.TabIndex = 0;
            this.cvStart.Unit = "mm";
            this.cvStart.Value = 0D;
            // 
            // TurningWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.cbxProfile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblSpindleRPM);
            this.Controls.Add(this.unitSpindleRPM);
            this.Controls.Add(this.txtSpindleRPM);
            this.Controls.Add(this.chkCSS);
            this.Controls.Add(this.txtGCode);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.cvLength);
            this.Controls.Add(this.cvStart);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TurningWizard";
            this.Size = new System.Drawing.Size(876, 511);
            ((System.ComponentModel.ISupportInitialize)(this.txtSpringPasses)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.TextBox txtGCode;
        private CoordValueControl cvStart;
        private CoordValueControl cvLength;
        private CoordValueControl cvTargetX;
        private CoordValueControl cvPassDepth;
        private CoordValueControl cvClearanceX;
        private System.Windows.Forms.CheckBox chkCSS;
        private CoordValueControl cvDiameter;
        private System.Windows.Forms.CheckBox chkSpringPasses;
        private System.Windows.Forms.NumericUpDown txtSpringPasses;
        private System.Windows.Forms.Label lblSpindleRPM;
        private System.Windows.Forms.Label unitSpindleRPM;
        private System.Windows.Forms.TextBox txtSpindleRPM;
        private CoordValueControl cvFeedRate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkTaper;
        private System.Windows.Forms.Label lblTaperUnit;
        private System.Windows.Forms.TextBox txtTaper;
        private System.Windows.Forms.ToolTip toolTip;
        private CoordValueControl cvPassDepthLast;
        private CoordValueControl cvFeedRateLast;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxProfile;
    }
}
