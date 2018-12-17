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
            this.droZ = new CNC_Controls.DROBase();
            this.droY = new CNC_Controls.DROBase();
            this.droX = new CNC_Controls.DROBase();
            this.droA = new CNC_Controls.DROBase();
            this.droB = new CNC_Controls.DROBase();
            this.droC = new CNC_Controls.DROBase();
            this.grpDRO.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnZeroAll
            // 
            this.btnZeroAll.Location = new System.Drawing.Point(35, 124);
            this.btnZeroAll.Name = "btnZeroAll";
            this.btnZeroAll.Size = new System.Drawing.Size(153, 30);
            this.btnZeroAll.TabIndex = 9;
            this.btnZeroAll.Tag = "ALL";
            this.btnZeroAll.Text = "Zero all";
            this.btnZeroAll.UseVisualStyleBackColor = true;
            // 
            // grpDRO
            // 
            this.grpDRO.Controls.Add(this.droC);
            this.grpDRO.Controls.Add(this.droB);
            this.grpDRO.Controls.Add(this.droA);
            this.grpDRO.Controls.Add(this.droZ);
            this.grpDRO.Controls.Add(this.droY);
            this.grpDRO.Controls.Add(this.droX);
            this.grpDRO.Controls.Add(this.btnZeroAll);
            this.grpDRO.Location = new System.Drawing.Point(3, 3);
            this.grpDRO.Name = "grpDRO";
            this.grpDRO.Size = new System.Drawing.Size(199, 166);
            this.grpDRO.TabIndex = 10;
            this.grpDRO.TabStop = false;
            this.grpDRO.Text = "DRO";
            // 
            // droZ
            // 
            this.droZ.Location = new System.Drawing.Point(7, 85);
            this.droZ.Name = "droZ";
            this.droZ.Size = new System.Drawing.Size(186, 37);
            this.droZ.TabIndex = 12;
            // 
            // droY
            // 
            this.droY.Location = new System.Drawing.Point(7, 50);
            this.droY.Name = "droY";
            this.droY.Size = new System.Drawing.Size(186, 37);
            this.droY.TabIndex = 11;
            // 
            // droX
            // 
            this.droX.Location = new System.Drawing.Point(6, 15);
            this.droX.Name = "droX";
            this.droX.Size = new System.Drawing.Size(187, 37);
            this.droX.TabIndex = 10;
            // 
            // droA
            // 
            this.droA.Location = new System.Drawing.Point(7, 120);
            this.droA.Name = "droA";
            this.droA.Size = new System.Drawing.Size(186, 37);
            this.droA.TabIndex = 13;
            this.droA.Visible = false;
            // 
            // droB
            // 
            this.droB.Location = new System.Drawing.Point(7, 120);
            this.droB.Name = "droB";
            this.droB.Size = new System.Drawing.Size(186, 37);
            this.droB.TabIndex = 14;
            this.droB.Visible = false;
            // 
            // droC
            // 
            this.droC.Location = new System.Drawing.Point(7, 120);
            this.droC.Name = "droC";
            this.droC.Size = new System.Drawing.Size(186, 37);
            this.droC.TabIndex = 15;
            this.droC.Visible = false;
            // 
            // DROControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDRO);
            this.Name = "DROControl";
            this.Size = new System.Drawing.Size(202, 169);
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
    }
}
