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
            this.lblTarget = new System.Windows.Forms.Label();
            this.lblActual = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.tbError = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbError)).BeginInit();
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
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.ForeColor = System.Drawing.Color.Green;
            this.lblTarget.Location = new System.Drawing.Point(660, 20);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(38, 13);
            this.lblTarget.TabIndex = 2;
            this.lblTarget.Text = "Target";
            // 
            // lblActual
            // 
            this.lblActual.AutoSize = true;
            this.lblActual.ForeColor = System.Drawing.Color.Blue;
            this.lblActual.Location = new System.Drawing.Point(660, 60);
            this.lblActual.Name = "lblActual";
            this.lblActual.Size = new System.Drawing.Size(37, 13);
            this.lblActual.TabIndex = 3;
            this.lblActual.Text = "Actual";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(660, 40);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(29, 13);
            this.lblError.TabIndex = 4;
            this.lblError.Text = "Error";
            // 
            // tbError
            // 
            this.tbError.LargeChange = 50;
            this.tbError.Location = new System.Drawing.Point(662, 380);
            this.tbError.Maximum = 3000;
            this.tbError.Minimum = 1;
            this.tbError.Name = "tbError";
            this.tbError.Size = new System.Drawing.Size(200, 45);
            this.tbError.SmallChange = 10;
            this.tbError.TabIndex = 5;
            this.tbError.Value = 2500;
            // 
            // PIDTunerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tbError);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblActual);
            this.Controls.Add(this.lblTarget);
            this.Controls.Add(this.btnGetPIDData);
            this.Controls.Add(this.PIDPlot);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PIDTunerControl";
            this.Size = new System.Drawing.Size(871, 511);
            ((System.ComponentModel.ISupportInitialize)(this.tbError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PIDPlot;
        private System.Windows.Forms.Button btnGetPIDData;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label lblActual;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TrackBar tbError;
    }
}
