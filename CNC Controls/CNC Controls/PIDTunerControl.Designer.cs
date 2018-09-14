namespace CNC_Controls
{
    partial class PIDTunerControl
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
            this.PIDPlot = new System.Windows.Forms.Panel();
            this.btnGetPIDData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PIDPlot
            // 
            this.PIDPlot.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PIDPlot.Location = new System.Drawing.Point(19, 20);
            this.PIDPlot.Name = "PIDPlot";
            this.PIDPlot.Size = new System.Drawing.Size(637, 405);
            this.PIDPlot.TabIndex = 0;
            // 
            // btnGetPIDData
            // 
            this.btnGetPIDData.Location = new System.Drawing.Point(19, 445);
            this.btnGetPIDData.Name = "btnGetPIDData";
            this.btnGetPIDData.Size = new System.Drawing.Size(75, 23);
            this.btnGetPIDData.TabIndex = 1;
            this.btnGetPIDData.Text = "Get data";
            this.btnGetPIDData.UseVisualStyleBackColor = true;
            // 
            // PIDTunerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnGetPIDData);
            this.Controls.Add(this.PIDPlot);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PIDTunerControl";
            this.Size = new System.Drawing.Size(871, 511);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PIDPlot;
        private System.Windows.Forms.Button btnGetPIDData;
    }
}
