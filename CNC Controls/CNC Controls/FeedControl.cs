using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CNC_Controls
{
    public partial class FeedControl : UserControl
    {
        public delegate void CommandGeneratedHandler(string command);
        public event CommandGeneratedHandler CommandGenerated;

        public FeedControl()
        {
            InitializeComponent();
            this.feedOverrideControl.ResetCommand = GrblConstants.CMD_FEED_OVR_RESET;
            this.feedOverrideControl.FineMinusCommand = GrblConstants.CMD_FEED_OVR_FINE_MINUS;
            this.feedOverrideControl.FinePlusCommand = GrblConstants.CMD_FEED_OVR_FINE_PLUS;
            this.feedOverrideControl.CoarseMinusCommand = GrblConstants.CMD_FEED_OVR_COARSE_MINUS;
            this.feedOverrideControl.CoarsePlusCommand = GrblConstants.CMD_FEED_OVR_COARSE_PLUS;
            this.feedOverrideControl.CommandGenerated += new OverrideControl.CommandGeneratedHandler(overrideControl_CommandGenerated);

            this.rapidsOverrideControl.MinusOnly = true;
            this.rapidsOverrideControl.ResetCommand = GrblConstants.CMD_RAPID_OVR_RESET;
            this.rapidsOverrideControl.FineMinusCommand = GrblConstants.CMD_RAPID_OVR_MEDIUM;
            this.rapidsOverrideControl.CoarseMinusCommand = GrblConstants.CMD_RAPID_OVR_LOW;
            this.rapidsOverrideControl.CommandGenerated += new OverrideControl.CommandGeneratedHandler(overrideControl_CommandGenerated);
        }

        public bool EnableControl
        {
            get { return this.txtFeedRate.Enabled; }
            set
            {
                this.txtFeedRate.Enabled = value;
            }
        }

        public double FeedRate { get { return 0; } set { this.txtFeedRate.Text = value.ToString(); } }
        public int FeedOverride { set { this.feedOverrideControl.Value = value; } }
        public int RapidsOverride { set { this.rapidsOverrideControl.Value = value; } }

        void overrideControl_CommandGenerated(string command)
        {
            if (CommandGenerated != null)
                CommandGenerated(command);
        }
    }
}
