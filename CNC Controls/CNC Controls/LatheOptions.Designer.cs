namespace CNC_Controls
{
    partial class LatheOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LatheOptions));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDiameter = new System.Windows.Forms.RadioButton();
            this.btnRadius = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxProfile = new System.Windows.Forms.ComboBox();
            this.btnAddProfile = new System.Windows.Forms.Button();
            this.lblSpindleRPM = new System.Windows.Forms.Label();
            this.unitSpindleRPM = new System.Windows.Forms.Label();
            this.txtSpindleRPM = new System.Windows.Forms.TextBox();
            this.chkCSS = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCSSMaxRPM = new System.Windows.Forms.Label();
            this.unitCSSMaxRPM = new System.Windows.Forms.Label();
            this.txtCSSMaxRPM = new System.Windows.Forms.TextBox();
            this.cvXClearance = new CNC_Controls.CoordValueControl();
            this.cvZClearance = new CNC_Controls.CoordValueControl();
            this.cvFeedRateLast = new CNC_Controls.CoordValueControl();
            this.cvFeedRate = new CNC_Controls.CoordValueControl();
            this.cvFirstCut = new CNC_Controls.CoordValueControl();
            this.cvMinCut = new CNC_Controls.CoordValueControl();
            this.cvLastCut = new CNC_Controls.CoordValueControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cvFeedRateLast);
            this.groupBox1.Controls.Add(this.cvFeedRate);
            this.groupBox1.Controls.Add(this.cvFirstCut);
            this.groupBox1.Controls.Add(this.cvMinCut);
            this.groupBox1.Controls.Add(this.cvLastCut);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cut depths";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDiameter);
            this.groupBox2.Controls.Add(this.btnRadius);
            this.groupBox2.Location = new System.Drawing.Point(12, 265);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 51);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "X-axis";
            // 
            // btnDiameter
            // 
            this.btnDiameter.AutoSize = true;
            this.btnDiameter.Location = new System.Drawing.Point(112, 20);
            this.btnDiameter.Name = "btnDiameter";
            this.btnDiameter.Size = new System.Drawing.Size(96, 17);
            this.btnDiameter.TabIndex = 1;
            this.btnDiameter.TabStop = true;
            this.btnDiameter.Text = "Diameter mode";
            this.btnDiameter.UseVisualStyleBackColor = true;
            // 
            // btnRadius
            // 
            this.btnRadius.AutoSize = true;
            this.btnRadius.Location = new System.Drawing.Point(19, 20);
            this.btnRadius.Name = "btnRadius";
            this.btnRadius.Size = new System.Drawing.Size(87, 17);
            this.btnRadius.TabIndex = 0;
            this.btnRadius.TabStop = true;
            this.btnRadius.Text = "Radius mode";
            this.btnRadius.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(124, 322);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(215, 322);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbxProfile
            // 
            this.cbxProfile.FormattingEnabled = true;
            this.cbxProfile.Location = new System.Drawing.Point(54, 9);
            this.cbxProfile.Name = "cbxProfile";
            this.cbxProfile.Size = new System.Drawing.Size(304, 21);
            this.cbxProfile.TabIndex = 7;
            // 
            // btnAddProfile
            // 
            this.btnAddProfile.Enabled = false;
            this.btnAddProfile.Location = new System.Drawing.Point(364, 7);
            this.btnAddProfile.Name = "btnAddProfile";
            this.btnAddProfile.Size = new System.Drawing.Size(36, 23);
            this.btnAddProfile.TabIndex = 8;
            this.btnAddProfile.Text = "Add";
            this.btnAddProfile.UseVisualStyleBackColor = true;
            // 
            // lblSpindleRPM
            // 
            this.lblSpindleRPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpindleRPM.Location = new System.Drawing.Point(189, 206);
            this.lblSpindleRPM.Name = "lblSpindleRPM";
            this.lblSpindleRPM.Size = new System.Drawing.Size(45, 13);
            this.lblSpindleRPM.TabIndex = 75;
            this.lblSpindleRPM.Text = "Spindle:";
            this.lblSpindleRPM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // unitSpindleRPM
            // 
            this.unitSpindleRPM.AutoSize = true;
            this.unitSpindleRPM.Location = new System.Drawing.Point(296, 206);
            this.unitSpindleRPM.Name = "unitSpindleRPM";
            this.unitSpindleRPM.Size = new System.Drawing.Size(31, 13);
            this.unitSpindleRPM.TabIndex = 74;
            this.unitSpindleRPM.Text = "RPM";
            // 
            // txtSpindleRPM
            // 
            this.txtSpindleRPM.Location = new System.Drawing.Point(240, 203);
            this.txtSpindleRPM.Name = "txtSpindleRPM";
            this.txtSpindleRPM.Size = new System.Drawing.Size(50, 20);
            this.txtSpindleRPM.TabIndex = 73;
            this.txtSpindleRPM.Text = "300";
            this.txtSpindleRPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkCSS
            // 
            this.chkCSS.AutoSize = true;
            this.chkCSS.Location = new System.Drawing.Point(12, 205);
            this.chkCSS.Name = "chkCSS";
            this.chkCSS.Size = new System.Drawing.Size(175, 17);
            this.chkCSS.TabIndex = 72;
            this.chkCSS.Text = "CSS (Constand Surface Speed)";
            this.chkCSS.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cvXClearance);
            this.groupBox3.Controls.Add(this.cvZClearance);
            this.groupBox3.Location = new System.Drawing.Point(13, 145);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(387, 51);
            this.groupBox3.TabIndex = 77;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Clearance";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 81;
            this.label1.Text = "Profile:";
            // 
            // lblCSSMaxRPM
            // 
            this.lblCSSMaxRPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCSSMaxRPM.Location = new System.Drawing.Point(189, 232);
            this.lblCSSMaxRPM.Name = "lblCSSMaxRPM";
            this.lblCSSMaxRPM.Size = new System.Drawing.Size(45, 13);
            this.lblCSSMaxRPM.TabIndex = 84;
            this.lblCSSMaxRPM.Text = "Max:";
            this.lblCSSMaxRPM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // unitCSSMaxRPM
            // 
            this.unitCSSMaxRPM.AutoSize = true;
            this.unitCSSMaxRPM.Location = new System.Drawing.Point(296, 232);
            this.unitCSSMaxRPM.Name = "unitCSSMaxRPM";
            this.unitCSSMaxRPM.Size = new System.Drawing.Size(31, 13);
            this.unitCSSMaxRPM.TabIndex = 83;
            this.unitCSSMaxRPM.Text = "RPM";
            // 
            // txtCSSMaxRPM
            // 
            this.txtCSSMaxRPM.Location = new System.Drawing.Point(240, 229);
            this.txtCSSMaxRPM.Name = "txtCSSMaxRPM";
            this.txtCSSMaxRPM.Size = new System.Drawing.Size(50, 20);
            this.txtCSSMaxRPM.TabIndex = 82;
            this.txtCSSMaxRPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cvXClearance
            // 
            this.cvXClearance.Label = "X-axis:";
            this.cvXClearance.Location = new System.Drawing.Point(6, 19);
            this.cvXClearance.Metric = true;
            this.cvXClearance.Name = "cvXClearance";
            this.cvXClearance.Size = new System.Drawing.Size(175, 20);
            this.cvXClearance.TabIndex = 2;
            this.cvXClearance.Unit = "mm";
            this.cvXClearance.Value = 0D;
            // 
            // cvZClearance
            // 
            this.cvZClearance.Label = "Z-axis:";
            this.cvZClearance.Location = new System.Drawing.Point(158, 19);
            this.cvZClearance.Metric = true;
            this.cvZClearance.Name = "cvZClearance";
            this.cvZClearance.Size = new System.Drawing.Size(175, 20);
            this.cvZClearance.TabIndex = 76;
            this.cvZClearance.Unit = "mm";
            this.cvZClearance.Value = 0D;
            // 
            // cvFeedRateLast
            // 
            this.cvFeedRateLast.Label = "Feed rate:";
            this.cvFeedRateLast.Location = new System.Drawing.Point(158, 45);
            this.cvFeedRateLast.Metric = true;
            this.cvFeedRateLast.Name = "cvFeedRateLast";
            this.cvFeedRateLast.Size = new System.Drawing.Size(175, 20);
            this.cvFeedRateLast.TabIndex = 74;
            this.cvFeedRateLast.Unit = "mm/min";
            this.cvFeedRateLast.Value = 300D;
            // 
            // cvFeedRate
            // 
            this.cvFeedRate.Label = "Feed rate:";
            this.cvFeedRate.Location = new System.Drawing.Point(158, 19);
            this.cvFeedRate.Metric = true;
            this.cvFeedRate.Name = "cvFeedRate";
            this.cvFeedRate.Size = new System.Drawing.Size(175, 20);
            this.cvFeedRate.TabIndex = 73;
            this.cvFeedRate.Unit = "mm/min";
            this.cvFeedRate.Value = 300D;
            // 
            // cvFirstCut
            // 
            this.cvFirstCut.Label = "(First) pass:";
            this.cvFirstCut.Location = new System.Drawing.Point(6, 19);
            this.cvFirstCut.Metric = true;
            this.cvFirstCut.Name = "cvFirstCut";
            this.cvFirstCut.Size = new System.Drawing.Size(175, 20);
            this.cvFirstCut.TabIndex = 0;
            this.cvFirstCut.Unit = "mm";
            this.cvFirstCut.Value = 0D;
            // 
            // cvMinCut
            // 
            this.cvMinCut.Enabled = false;
            this.cvMinCut.Label = "Min:";
            this.cvMinCut.Location = new System.Drawing.Point(6, 71);
            this.cvMinCut.Metric = true;
            this.cvMinCut.Name = "cvMinCut";
            this.cvMinCut.Size = new System.Drawing.Size(175, 20);
            this.cvMinCut.TabIndex = 2;
            this.cvMinCut.Unit = "mm";
            this.cvMinCut.Value = 0D;
            // 
            // cvLastCut
            // 
            this.cvLastCut.Label = "Last pass:";
            this.cvLastCut.Location = new System.Drawing.Point(6, 45);
            this.cvLastCut.Metric = true;
            this.cvLastCut.Name = "cvLastCut";
            this.cvLastCut.Size = new System.Drawing.Size(175, 20);
            this.cvLastCut.TabIndex = 1;
            this.cvLastCut.Unit = "mm";
            this.cvLastCut.Value = 0D;
            // 
            // LatheOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 352);
            this.Controls.Add(this.lblCSSMaxRPM);
            this.Controls.Add(this.unitCSSMaxRPM);
            this.Controls.Add(this.txtCSSMaxRPM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblSpindleRPM);
            this.Controls.Add(this.unitSpindleRPM);
            this.Controls.Add(this.txtSpindleRPM);
            this.Controls.Add(this.chkCSS);
            this.Controls.Add(this.btnAddProfile);
            this.Controls.Add(this.cbxProfile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LatheOptions";
            this.Text = "Lathe Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CoordValueControl cvFirstCut;
        private CoordValueControl cvLastCut;
        private CoordValueControl cvMinCut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton btnDiameter;
        private System.Windows.Forms.RadioButton btnRadius;
        private System.Windows.Forms.Button btnOk;
        private CoordValueControl cvXClearance;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbxProfile;
        private System.Windows.Forms.Button btnAddProfile;
        private CoordValueControl cvFeedRateLast;
        private CoordValueControl cvFeedRate;
        private System.Windows.Forms.Label lblSpindleRPM;
        private System.Windows.Forms.Label unitSpindleRPM;
        private System.Windows.Forms.TextBox txtSpindleRPM;
        private System.Windows.Forms.CheckBox chkCSS;
        private CoordValueControl cvZClearance;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCSSMaxRPM;
        private System.Windows.Forms.Label unitCSSMaxRPM;
        private System.Windows.Forms.TextBox txtCSSMaxRPM;
    }
}