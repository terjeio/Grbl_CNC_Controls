/*
 * CNCCameraControl.cs - part of CNC Controls library
 *
 * v0.01 / 2018-09-14 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2018, Io Engineering (Terje Io) - parts derived from AForge example code
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

· Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

· Redistributions in binary form must reproduce the above copyright notice, this
list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

· Neither the name of the copyright holder nor the names of its contributors may
be used to endorse or promote products derived from this software without
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;

namespace CNC_Camera_Control
{
    public partial class CNCCameraControl : UserControl
    {
        private int cpct = 20;

        public enum MoveMode
        {
            XAxisFirst = 1,
            YAxisFirst = 2,
            BothAxes   = 3
        }

        public delegate void MoveOffsetHandler (MoveMode Mode, double XOffset, double YOffset);
        public event MoveOffsetHandler MoveOffset;

        private SolidBrush brush = null;
        private Pen pen = null;

        public CNCCameraControl()
        {
            InitializeComponent();

            this.brush = new SolidBrush(Color.Red);
            this.pen = new Pen(brush);

            trbCircle.ValueChanged += new EventHandler(trbCircle_ValueChanged);
            btnMoveOffset.Click += new EventHandler(btnMoveOffset_Click);
            videoSourcePlayer.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(videoSourcePlayer_NewFrame);

            XOffset = YOffset = 0.0;
            Mode = MoveMode.BothAxes;
        }

        public double XOffset { get; set; }
        public double YOffset { get; set; }
        public MoveMode Mode { get; set; }
    
        void trbCircle_ValueChanged(object sender, EventArgs e)
        {
            cpct = trbCircle.Value;
        }

        void btnMoveOffset_Click(object sender, EventArgs e)
        {
            if (MoveOffset != null)
                MoveOffset(Mode, XOffset, YOffset);
        }

        public void SelectVideoSource()
        {
            VideoCaptureDeviceForm form = null;

            if (videoSourcePlayer.VideoSource == null)
            {
                form = new VideoCaptureDeviceForm();

                if (form.ShowDialog(this) == DialogResult.OK)
                    OpenVideoSource(form.VideoDevice);
            }
        }

        private void OpenVideoSource(IVideoSource source)
        {
            this.Cursor = Cursors.WaitCursor;

            CloseCurrentVideoSource();

            videoSourcePlayer.VideoSource = source;
            videoSourcePlayer.Start();

            this.Cursor = Cursors.Default;
        }

        public void CloseCurrentVideoSource()
        {
            if (videoSourcePlayer.VideoSource != null)
            {
                videoSourcePlayer.SignalToStop();

                // wait ~ 3 seconds
                for (int i = 0; i < 30; i++)
                {
                    if (!videoSourcePlayer.IsRunning)
                        break;
                    System.Threading.Thread.Sleep(100);
                }

                if (videoSourcePlayer.IsRunning)
                    videoSourcePlayer.Stop();

                videoSourcePlayer.VideoSource = null;
            }
        }

        private void videoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);

            Point center = new Point((int)(g.VisibleClipBounds.Width / 2.0f), (int)(g.VisibleClipBounds.Height / 2.0f));

            g.DrawLine(this.pen, g.VisibleClipBounds.X, center.Y, g.VisibleClipBounds.Width, center.Y);
            g.DrawLine(this.pen, center.X, g.VisibleClipBounds.Y, center.X, g.VisibleClipBounds.Height);
            g.DrawEllipse(this.pen, center.X - cpct, center.Y - cpct, cpct * 2, cpct * 2);
            g.Dispose();
        }

    }
}
