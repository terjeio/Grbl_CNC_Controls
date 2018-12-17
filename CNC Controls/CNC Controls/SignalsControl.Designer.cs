namespace CNC_Controls
{
    partial class SignalsControl
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btnLimitX = new System.Windows.Forms.Button();
            this.lblLimitX = new System.Windows.Forms.Label();
            this.lblLimitY = new System.Windows.Forms.Label();
            this.btnY = new System.Windows.Forms.Button();
            this.btnLimitZ = new System.Windows.Forms.Label();
            this.btnZ = new System.Windows.Forms.Button();
            this.lblLimitA = new System.Windows.Forms.Label();
            this.btnLimitA = new System.Windows.Forms.Button();
            this.lblLimitB = new System.Windows.Forms.Label();
            this.btnLimitB = new System.Windows.Forms.Button();
            this.lblLimitC = new System.Windows.Forms.Label();
            this.btnLimitC = new System.Windows.Forms.Button();
            this.lblProbe = new System.Windows.Forms.Label();
            this.btnProbe = new System.Windows.Forms.Button();
            this.lblSafetyDoor = new System.Windows.Forms.Label();
            this.btnSafetyDoor = new System.Windows.Forms.Button();
            this.lblReset = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblCycleStart = new System.Windows.Forms.Label();
            this.btnCycleStart = new System.Windows.Forms.Button();
            this.lblFeedHold = new System.Windows.Forms.Label();
            this.btnFeedHold = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.lblFeedHold);
            this.groupBox.Controls.Add(this.btnFeedHold);
            this.groupBox.Controls.Add(this.lblCycleStart);
            this.groupBox.Controls.Add(this.btnCycleStart);
            this.groupBox.Controls.Add(this.lblReset);
            this.groupBox.Controls.Add(this.btnReset);
            this.groupBox.Controls.Add(this.lblSafetyDoor);
            this.groupBox.Controls.Add(this.btnSafetyDoor);
            this.groupBox.Controls.Add(this.lblProbe);
            this.groupBox.Controls.Add(this.btnProbe);
            this.groupBox.Controls.Add(this.lblLimitC);
            this.groupBox.Controls.Add(this.btnLimitC);
            this.groupBox.Controls.Add(this.lblLimitB);
            this.groupBox.Controls.Add(this.btnLimitB);
            this.groupBox.Controls.Add(this.lblLimitA);
            this.groupBox.Controls.Add(this.btnLimitA);
            this.groupBox.Controls.Add(this.btnLimitZ);
            this.groupBox.Controls.Add(this.btnZ);
            this.groupBox.Controls.Add(this.lblLimitY);
            this.groupBox.Controls.Add(this.btnY);
            this.groupBox.Controls.Add(this.lblLimitX);
            this.groupBox.Controls.Add(this.btnLimitX);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(210, 58);
            this.groupBox.TabIndex = 26;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Signals";
            // 
            // btnLimitX
            // 
            this.btnLimitX.Enabled = false;
            this.btnLimitX.FlatAppearance.BorderSize = 0;
            this.btnLimitX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimitX.Location = new System.Drawing.Point(6, 19);
            this.btnLimitX.Name = "btnLimitX";
            this.btnLimitX.Size = new System.Drawing.Size(12, 12);
            this.btnLimitX.TabIndex = 0;
            this.btnLimitX.TabStop = false;
            this.btnLimitX.Tag = "X";
            this.btnLimitX.UseVisualStyleBackColor = true;
            // 
            // lblLimitX
            // 
            this.lblLimitX.AutoSize = true;
            this.lblLimitX.Location = new System.Drawing.Point(6, 37);
            this.lblLimitX.Name = "lblLimitX";
            this.lblLimitX.Size = new System.Drawing.Size(14, 13);
            this.lblLimitX.TabIndex = 1;
            this.lblLimitX.Tag = "X";
            this.lblLimitX.Text = "X";
            // 
            // lblLimitY
            // 
            this.lblLimitY.AutoSize = true;
            this.lblLimitY.Location = new System.Drawing.Point(24, 37);
            this.lblLimitY.Name = "lblLimitY";
            this.lblLimitY.Size = new System.Drawing.Size(14, 13);
            this.lblLimitY.TabIndex = 3;
            this.lblLimitY.Tag = "Y";
            this.lblLimitY.Text = "Y";
            // 
            // btnY
            // 
            this.btnY.Enabled = false;
            this.btnY.FlatAppearance.BorderSize = 0;
            this.btnY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnY.Location = new System.Drawing.Point(24, 19);
            this.btnY.Name = "btnY";
            this.btnY.Size = new System.Drawing.Size(12, 12);
            this.btnY.TabIndex = 2;
            this.btnY.Tag = "Y";
            this.btnY.UseVisualStyleBackColor = true;
            // 
            // btnLimitZ
            // 
            this.btnLimitZ.AutoSize = true;
            this.btnLimitZ.Location = new System.Drawing.Point(42, 37);
            this.btnLimitZ.Name = "btnLimitZ";
            this.btnLimitZ.Size = new System.Drawing.Size(14, 13);
            this.btnLimitZ.TabIndex = 5;
            this.btnLimitZ.Text = "Z";
            // 
            // btnZ
            // 
            this.btnZ.Enabled = false;
            this.btnZ.FlatAppearance.BorderSize = 0;
            this.btnZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZ.Location = new System.Drawing.Point(42, 19);
            this.btnZ.Name = "btnZ";
            this.btnZ.Size = new System.Drawing.Size(12, 12);
            this.btnZ.TabIndex = 4;
            this.btnZ.Tag = "Z";
            this.btnZ.UseVisualStyleBackColor = true;
            // 
            // lblLimitA
            // 
            this.lblLimitA.AutoSize = true;
            this.lblLimitA.Location = new System.Drawing.Point(62, 37);
            this.lblLimitA.Name = "lblLimitA";
            this.lblLimitA.Size = new System.Drawing.Size(14, 13);
            this.lblLimitA.TabIndex = 7;
            this.lblLimitA.Tag = "A";
            this.lblLimitA.Text = "A";
            // 
            // btnLimitA
            // 
            this.btnLimitA.Enabled = false;
            this.btnLimitA.FlatAppearance.BorderSize = 0;
            this.btnLimitA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimitA.Location = new System.Drawing.Point(62, 19);
            this.btnLimitA.Name = "btnLimitA";
            this.btnLimitA.Size = new System.Drawing.Size(12, 12);
            this.btnLimitA.TabIndex = 6;
            this.btnLimitA.Tag = "A";
            this.btnLimitA.UseVisualStyleBackColor = true;
            // 
            // lblLimitB
            // 
            this.lblLimitB.AutoSize = true;
            this.lblLimitB.Location = new System.Drawing.Point(80, 37);
            this.lblLimitB.Name = "lblLimitB";
            this.lblLimitB.Size = new System.Drawing.Size(14, 13);
            this.lblLimitB.TabIndex = 9;
            this.lblLimitB.Tag = "B";
            this.lblLimitB.Text = "B";
            // 
            // btnLimitB
            // 
            this.btnLimitB.Enabled = false;
            this.btnLimitB.FlatAppearance.BorderSize = 0;
            this.btnLimitB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimitB.Location = new System.Drawing.Point(80, 19);
            this.btnLimitB.Name = "btnLimitB";
            this.btnLimitB.Size = new System.Drawing.Size(12, 12);
            this.btnLimitB.TabIndex = 8;
            this.btnLimitB.Tag = "B";
            this.btnLimitB.UseVisualStyleBackColor = true;
            // 
            // lblLimitC
            // 
            this.lblLimitC.AutoSize = true;
            this.lblLimitC.Location = new System.Drawing.Point(98, 37);
            this.lblLimitC.Name = "lblLimitC";
            this.lblLimitC.Size = new System.Drawing.Size(14, 13);
            this.lblLimitC.TabIndex = 11;
            this.lblLimitC.Tag = "C";
            this.lblLimitC.Text = "C";
            // 
            // btnLimitC
            // 
            this.btnLimitC.Enabled = false;
            this.btnLimitC.FlatAppearance.BorderSize = 0;
            this.btnLimitC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimitC.Location = new System.Drawing.Point(98, 19);
            this.btnLimitC.Name = "btnLimitC";
            this.btnLimitC.Size = new System.Drawing.Size(12, 12);
            this.btnLimitC.TabIndex = 10;
            this.btnLimitC.Tag = "C";
            this.btnLimitC.UseVisualStyleBackColor = true;
            // 
            // lblProbe
            // 
            this.lblProbe.AutoSize = true;
            this.lblProbe.Location = new System.Drawing.Point(192, 37);
            this.lblProbe.Name = "lblProbe";
            this.lblProbe.Size = new System.Drawing.Size(14, 13);
            this.lblProbe.TabIndex = 13;
            this.lblProbe.Tag = "P";
            this.lblProbe.Text = "P";
            // 
            // btnProbe
            // 
            this.btnProbe.Enabled = false;
            this.btnProbe.FlatAppearance.BorderSize = 0;
            this.btnProbe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProbe.Location = new System.Drawing.Point(192, 19);
            this.btnProbe.Name = "btnProbe";
            this.btnProbe.Size = new System.Drawing.Size(12, 12);
            this.btnProbe.TabIndex = 12;
            this.btnProbe.Tag = "P";
            this.btnProbe.UseVisualStyleBackColor = true;
            // 
            // lblSafetyDoor
            // 
            this.lblSafetyDoor.AutoSize = true;
            this.lblSafetyDoor.Location = new System.Drawing.Point(174, 37);
            this.lblSafetyDoor.Name = "lblSafetyDoor";
            this.lblSafetyDoor.Size = new System.Drawing.Size(15, 13);
            this.lblSafetyDoor.TabIndex = 15;
            this.lblSafetyDoor.Tag = "D";
            this.lblSafetyDoor.Text = "D";
            // 
            // btnSafetyDoor
            // 
            this.btnSafetyDoor.Enabled = false;
            this.btnSafetyDoor.FlatAppearance.BorderSize = 0;
            this.btnSafetyDoor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSafetyDoor.Location = new System.Drawing.Point(174, 19);
            this.btnSafetyDoor.Name = "btnSafetyDoor";
            this.btnSafetyDoor.Size = new System.Drawing.Size(12, 12);
            this.btnSafetyDoor.TabIndex = 14;
            this.btnSafetyDoor.Tag = "D";
            this.btnSafetyDoor.UseVisualStyleBackColor = true;
            // 
            // lblReset
            // 
            this.lblReset.AutoSize = true;
            this.lblReset.Location = new System.Drawing.Point(156, 37);
            this.lblReset.Name = "lblReset";
            this.lblReset.Size = new System.Drawing.Size(15, 13);
            this.lblReset.TabIndex = 17;
            this.lblReset.Tag = "R";
            this.lblReset.Text = "R";
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(156, 19);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(12, 12);
            this.btnReset.TabIndex = 16;
            this.btnReset.Tag = "R";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // lblCycleStart
            // 
            this.lblCycleStart.AutoSize = true;
            this.lblCycleStart.Location = new System.Drawing.Point(138, 37);
            this.lblCycleStart.Name = "lblCycleStart";
            this.lblCycleStart.Size = new System.Drawing.Size(14, 13);
            this.lblCycleStart.TabIndex = 19;
            this.lblCycleStart.Tag = "S";
            this.lblCycleStart.Text = "S";
            // 
            // btnCycleStart
            // 
            this.btnCycleStart.Enabled = false;
            this.btnCycleStart.FlatAppearance.BorderSize = 0;
            this.btnCycleStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCycleStart.Location = new System.Drawing.Point(138, 19);
            this.btnCycleStart.Name = "btnCycleStart";
            this.btnCycleStart.Size = new System.Drawing.Size(12, 12);
            this.btnCycleStart.TabIndex = 18;
            this.btnCycleStart.Tag = "S";
            this.btnCycleStart.UseVisualStyleBackColor = true;
            // 
            // lblFeedHold
            // 
            this.lblFeedHold.AutoSize = true;
            this.lblFeedHold.Location = new System.Drawing.Point(120, 37);
            this.lblFeedHold.Name = "lblFeedHold";
            this.lblFeedHold.Size = new System.Drawing.Size(15, 13);
            this.lblFeedHold.TabIndex = 21;
            this.lblFeedHold.Tag = "H";
            this.lblFeedHold.Text = "H";
            // 
            // btnFeedHold
            // 
            this.btnFeedHold.Enabled = false;
            this.btnFeedHold.FlatAppearance.BorderSize = 0;
            this.btnFeedHold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFeedHold.Location = new System.Drawing.Point(120, 19);
            this.btnFeedHold.Name = "btnFeedHold";
            this.btnFeedHold.Size = new System.Drawing.Size(12, 12);
            this.btnFeedHold.TabIndex = 20;
            this.btnFeedHold.Tag = "H";
            this.btnFeedHold.UseVisualStyleBackColor = true;
            // 
            // SignalsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "SignalsControl";
            this.Size = new System.Drawing.Size(216, 65);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button btnLimitX;
        private System.Windows.Forms.Label lblLimitX;
        private System.Windows.Forms.Label lblFeedHold;
        private System.Windows.Forms.Button btnFeedHold;
        private System.Windows.Forms.Label lblCycleStart;
        private System.Windows.Forms.Button btnCycleStart;
        private System.Windows.Forms.Label lblReset;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblSafetyDoor;
        private System.Windows.Forms.Button btnSafetyDoor;
        private System.Windows.Forms.Label lblProbe;
        private System.Windows.Forms.Button btnProbe;
        private System.Windows.Forms.Label lblLimitC;
        private System.Windows.Forms.Button btnLimitC;
        private System.Windows.Forms.Label lblLimitB;
        private System.Windows.Forms.Button btnLimitB;
        private System.Windows.Forms.Label lblLimitA;
        private System.Windows.Forms.Button btnLimitA;
        private System.Windows.Forms.Label btnLimitZ;
        private System.Windows.Forms.Button btnZ;
        private System.Windows.Forms.Label lblLimitY;
        private System.Windows.Forms.Button btnY;
    }
}
