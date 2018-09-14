namespace CNC_Controls
{
    partial class CoolantControl
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
            this.chkFlood = new System.Windows.Forms.CheckBox();
            this.chkMist = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkFlood);
            this.groupBox1.Controls.Add(this.chkMist);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 65);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coolant";
            // 
            // chkFlood
            // 
            this.chkFlood.AutoSize = true;
            this.chkFlood.Location = new System.Drawing.Point(21, 19);
            this.chkFlood.Name = "chkFlood";
            this.chkFlood.Size = new System.Drawing.Size(52, 17);
            this.chkFlood.TabIndex = 20;
            this.chkFlood.Text = "Flood";
            this.chkFlood.UseVisualStyleBackColor = true;
            // 
            // chkMist
            // 
            this.chkMist.AutoSize = true;
            this.chkMist.Location = new System.Drawing.Point(21, 42);
            this.chkMist.Name = "chkMist";
            this.chkMist.Size = new System.Drawing.Size(45, 17);
            this.chkMist.TabIndex = 18;
            this.chkMist.Text = "Mist";
            this.chkMist.UseVisualStyleBackColor = true;
            // 
            // CoolantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CoolantControl";
            this.Size = new System.Drawing.Size(219, 73);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkFlood;
        private System.Windows.Forms.CheckBox chkMist;
    }
}
