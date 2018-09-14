namespace CNC_Controls
{
    partial class FlowControl
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
            this.chkCoolant = new System.Windows.Forms.CheckBox();
            this.chkExhaust = new System.Windows.Forms.CheckBox();
            this.chkAir = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkCoolant);
            this.groupBox1.Controls.Add(this.chkExhaust);
            this.groupBox1.Controls.Add(this.chkAir);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 77);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main";
            // 
            // chkCoolant
            // 
            this.chkCoolant.AutoSize = true;
            this.chkCoolant.Location = new System.Drawing.Point(38, 52);
            this.chkCoolant.Name = "chkCoolant";
            this.chkCoolant.Size = new System.Drawing.Size(62, 17);
            this.chkCoolant.TabIndex = 20;
            this.chkCoolant.Text = "Coolant";
            this.chkCoolant.UseVisualStyleBackColor = true;
            // 
            // chkExhaust
            // 
            this.chkExhaust.AutoSize = true;
            this.chkExhaust.Location = new System.Drawing.Point(38, 35);
            this.chkExhaust.Name = "chkExhaust";
            this.chkExhaust.Size = new System.Drawing.Size(82, 17);
            this.chkExhaust.TabIndex = 19;
            this.chkExhaust.Text = "Exhaust fan";
            this.chkExhaust.UseVisualStyleBackColor = true;
            // 
            // chkAir
            // 
            this.chkAir.AutoSize = true;
            this.chkAir.Location = new System.Drawing.Point(38, 17);
            this.chkAir.Name = "chkAir";
            this.chkAir.Size = new System.Drawing.Size(67, 17);
            this.chkAir.TabIndex = 18;
            this.chkAir.Text = "Air assist";
            this.chkAir.UseVisualStyleBackColor = true;
            // 
            // FlowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "FlowControl";
            this.Size = new System.Drawing.Size(210, 77);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkCoolant;
        private System.Windows.Forms.CheckBox chkExhaust;
        private System.Windows.Forms.CheckBox chkAir;
    }
}
