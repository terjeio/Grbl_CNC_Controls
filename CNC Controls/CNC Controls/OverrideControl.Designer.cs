namespace CNC_Controls
{
    partial class OverrideControl
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
            this.btnOvCoarseMinus = new System.Windows.Forms.Button();
            this.btnOvCoarsePlus = new System.Windows.Forms.Button();
            this.btnOvReset = new System.Windows.Forms.Button();
            this.btnOvFineMinus = new System.Windows.Forms.Button();
            this.btnOvFinePlus = new System.Windows.Forms.Button();
            this.txtOverride = new System.Windows.Forms.TextBox();
            this.lblOverride = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOvCoarseMinus
            // 
            this.btnOvCoarseMinus.Location = new System.Drawing.Point(95, 31);
            this.btnOvCoarseMinus.Name = "btnOvCoarseMinus";
            this.btnOvCoarseMinus.Size = new System.Drawing.Size(27, 26);
            this.btnOvCoarseMinus.TabIndex = 0;
            this.btnOvCoarseMinus.TabStop = false;
            this.btnOvCoarseMinus.Text = "-";
            this.btnOvCoarseMinus.UseVisualStyleBackColor = true;
            // 
            // btnOvCoarsePlus
            // 
            this.btnOvCoarsePlus.Location = new System.Drawing.Point(95, 2);
            this.btnOvCoarsePlus.Name = "btnOvCoarsePlus";
            this.btnOvCoarsePlus.Size = new System.Drawing.Size(27, 26);
            this.btnOvCoarsePlus.TabIndex = 0;
            this.btnOvCoarsePlus.TabStop = false;
            this.btnOvCoarsePlus.Text = "+";
            this.btnOvCoarsePlus.UseVisualStyleBackColor = true;
            // 
            // btnOvReset
            // 
            this.btnOvReset.Location = new System.Drawing.Point(150, 20);
            this.btnOvReset.Name = "btnOvReset";
            this.btnOvReset.Size = new System.Drawing.Size(21, 23);
            this.btnOvReset.TabIndex = 0;
            this.btnOvReset.TabStop = false;
            this.btnOvReset.Text = "0";
            this.btnOvReset.UseVisualStyleBackColor = true;
            // 
            // btnOvFineMinus
            // 
            this.btnOvFineMinus.Location = new System.Drawing.Point(125, 31);
            this.btnOvFineMinus.Name = "btnOvFineMinus";
            this.btnOvFineMinus.Size = new System.Drawing.Size(21, 23);
            this.btnOvFineMinus.TabIndex = 0;
            this.btnOvFineMinus.TabStop = false;
            this.btnOvFineMinus.Text = "-";
            this.btnOvFineMinus.UseVisualStyleBackColor = true;
            // 
            // btnOvFinePlus
            // 
            this.btnOvFinePlus.Location = new System.Drawing.Point(125, 5);
            this.btnOvFinePlus.Name = "btnOvFinePlus";
            this.btnOvFinePlus.Size = new System.Drawing.Size(21, 23);
            this.btnOvFinePlus.TabIndex = 0;
            this.btnOvFinePlus.TabStop = false;
            this.btnOvFinePlus.Text = "+";
            this.btnOvFinePlus.UseVisualStyleBackColor = true;
            // 
            // txtOverride
            // 
            this.txtOverride.Location = new System.Drawing.Point(56, 21);
            this.txtOverride.Name = "txtOverride";
            this.txtOverride.ReadOnly = true;
            this.txtOverride.Size = new System.Drawing.Size(33, 20);
            this.txtOverride.TabIndex = 0;
            this.txtOverride.TabStop = false;
            // 
            // lblOverride
            // 
            this.lblOverride.AutoSize = true;
            this.lblOverride.Location = new System.Drawing.Point(3, 24);
            this.lblOverride.Name = "lblOverride";
            this.lblOverride.Size = new System.Drawing.Size(47, 13);
            this.lblOverride.TabIndex = 12;
            this.lblOverride.Text = "Override";
            // 
            // OverrideControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOvCoarseMinus);
            this.Controls.Add(this.btnOvCoarsePlus);
            this.Controls.Add(this.btnOvReset);
            this.Controls.Add(this.btnOvFineMinus);
            this.Controls.Add(this.btnOvFinePlus);
            this.Controls.Add(this.txtOverride);
            this.Controls.Add(this.lblOverride);
            this.Name = "OverrideControl";
            this.Size = new System.Drawing.Size(174, 61);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOvCoarseMinus;
        private System.Windows.Forms.Button btnOvCoarsePlus;
        private System.Windows.Forms.Button btnOvReset;
        private System.Windows.Forms.Button btnOvFineMinus;
        private System.Windows.Forms.Button btnOvFinePlus;
        private System.Windows.Forms.TextBox txtOverride;
        private System.Windows.Forms.Label lblOverride;
    }
}
