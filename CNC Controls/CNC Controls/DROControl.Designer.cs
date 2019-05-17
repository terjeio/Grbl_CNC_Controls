namespace CNC_Controls
{
    partial class DROControl
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
            this.btnZeroAll = new System.Windows.Forms.Button();
            this.grpDRO = new System.Windows.Forms.GroupBox();
            this.lblXMode = new System.Windows.Forms.Label();
            this.droC = new CNC_Controls.DROBase();
            this.droB = new CNC_Controls.DROBase();
            this.droA = new CNC_Controls.DROBase();
            this.droZ = new CNC_Controls.DROBase();
            this.droY = new CNC_Controls.DROBase();
            this.droX = new CNC_Controls.DROBase();
            this.grpDRO.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnZeroAll
            // 
            this.btnZeroAll.Location = new System.Drawing.Point(35, 141);
            this.btnZeroAll.Name = "btnZeroAll";
            this.btnZeroAll.Size = new System.Drawing.Size(153, 30);
            this.btnZeroAll.TabIndex = 9;
            this.btnZeroAll.Tag = "ALL";
            this.btnZeroAll.Text = "Zero all";
            this.btnZeroAll.UseVisualStyleBackColor = true;
            // 
            // grpDRO
            // 
            this.grpDRO.Controls.Add(this.lblXMode);
            this.grpDRO.Controls.Add(this.droC);
            this.grpDRO.Controls.Add(this.droB);
            this.grpDRO.Controls.Add(this.droA);
            this.grpDRO.Controls.Add(this.droZ);
            this.grpDRO.Controls.Add(this.droY);
            this.grpDRO.Controls.Add(this.droX);
            this.grpDRO.Controls.Add(this.btnZeroAll);
            this.grpDRO.Location = new System.Drawing.Point(3, 3);
            this.grpDRO.Name = "grpDRO";
            this.grpDRO.Size = new System.Drawing.Size(199, 135);
            this.grpDRO.TabIndex = 10;
            this.grpDRO.TabStop = false;
            this.grpDRO.Text = "DRO";
            // 
            // lblXMode
            // 
            this.lblXMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblXMode.Location = new System.Drawing.Point(88, 14);
            this.lblXMode.Name = "lblXMode";
            this.lblXMode.Size = new System.Drawing.Size(49, 13);
            this.lblXMode.TabIndex = 16;
            this.lblXMode.Text = "Diameter";
            this.lblXMode.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblXMode.Visible = false;
            // 
            // droC
            // 
            this.droC.Location = new System.Drawing.Point(7, 133);
            this.droC.Name = "droC";
            this.droC.Size = new System.Drawing.Size(186, 33);
            this.droC.TabIndex = 15;
            this.droC.Visible = false;
            // 
            // droB
            // 
            this.droB.Location = new System.Drawing.Point(7, 137);
            this.droB.Name = "droB";
            this.droB.Size = new System.Drawing.Size(186, 33);
            this.droB.TabIndex = 14;
            this.droB.Visible = false;
            // 
            // droA
            // 
            this.droA.Location = new System.Drawing.Point(7, 137);
            this.droA.Name = "droA";
            this.droA.Size = new System.Drawing.Size(186, 33);
            this.droA.TabIndex = 13;
            this.droA.Visible = false;
            // 
            // droZ
            // 
            this.droZ.Location = new System.Drawing.Point(7, 98);
            this.droZ.Name = "droZ";
            this.droZ.Size = new System.Drawing.Size(186, 33);
            this.droZ.TabIndex = 12;
            // 
            // droY
            // 
            this.droY.Location = new System.Drawing.Point(7, 63);
            this.droY.Name = "droY";
            this.droY.Size = new System.Drawing.Size(186, 33);
            this.droY.TabIndex = 11;
            // 
            // droX
            // 
            this.droX.Location = new System.Drawing.Point(6, 28);
            this.droX.Name = "droX";
            this.droX.Size = new System.Drawing.Size(187, 33);
            this.droX.TabIndex = 10;
            // 
            // DROControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDRO);
            this.Name = "DROControl";
            this.Size = new System.Drawing.Size(202, 140);
            this.grpDRO.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnZeroAll;
        private System.Windows.Forms.GroupBox grpDRO;
        private DROBase droZ;
        private DROBase droY;
        private DROBase droX;
        private DROBase droA;
        private DROBase droC;
        private DROBase droB;
        private System.Windows.Forms.Label lblXMode;
    }
}
