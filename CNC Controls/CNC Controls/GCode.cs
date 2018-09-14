/*
 * GCode.cs - part of CNC Controls library
 *
 * v0.01 / 2018-09-14 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2018, Io Engineering (Terje Io)
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
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace CNC_Controls
{

    public class GCode
    {

        private DataTable gcode = new DataTable("GCode");
        private BindingSource source = new BindingSource();

        public Queue<string> commands = new Queue<string>();

        public delegate bool ToolChangedHandler(int toolNumber);
        public event ToolChangedHandler ToolChanged = null;

        public delegate void FileChangedHandler (string filename);
        public event FileChangedHandler FileChanged = null;

        public GCode()
        {
            this.gcode.Columns.Add("LineNum", typeof(int));
            this.gcode.Columns.Add("Data", typeof(string));
            this.gcode.Columns.Add("Length", typeof(int));
            this.gcode.Columns.Add("File", typeof(bool));
            this.gcode.Columns.Add("Sent", typeof(string));
            this.gcode.Columns.Add("Ok", typeof(bool));
            this.gcode.PrimaryKey = new DataColumn[] { this.gcode.Columns["LineNum"] };

            filename = "";
            source.DataSource = gcode;

            Reset();
        }

        public DataTable Data { get { return gcode; } }
        public BindingSource Source { get { return source; } }
        public bool Open { get { return gcode.Rows.Count > 0; } }

        public double min_feed { get; private set; }
        public double max_feed { get; private set; }

        public double min_x { get; private set; }
        public double max_x { get; private set; }

        public double min_y { get; private set; }
        public double max_y { get; private set; }

        public double min_z { get; private set; }
        public double max_z { get; private set; }
        
        public string filename { get; private set; }

        public bool LoadFile (string filename) {

            int LineNumber = 0;

            if (this.Open)
                this.gcode.Rows.Clear();

            commands.Clear();

            Reset();

            this.filename = filename;

            FileInfo file = new FileInfo(filename);

            StreamReader sr = file.OpenText();

            string s = sr.ReadLine();

            while (s != null)
            {
                if(ParseBlock(s + "\r", false))
                    this.gcode.Rows.Add(new object[] { LineNumber++, s, s.Length + 1, true, "", false });
                while(commands.Count > 0)
                    this.gcode.Rows.Add(new object[] { LineNumber++, commands.Dequeue(), 20, true, "", false });
                s = sr.ReadLine();
            }

            sr.Close();

            if (max_feed == double.MinValue)
            {
                min_feed = 0.0;
                max_feed = 0.0;
            }

            if (max_x == double.MinValue)
            {
                min_x = 0.0;
                max_x = 0.0;
            }

            if (max_y == double.MinValue)
            {
                min_y = 0.0;
                max_y = 0.0;
            }

            if (max_z == double.MinValue)
            {
                min_z = 0.0;
                max_z = 0.0;
            }

            if (FileChanged != null)
                FileChanged(filename);

            return true;
        }

        public void CloseFile()
        {
            if (this.Open)
                this.gcode.Rows.Clear();

            commands.Clear();

            Reset();

            this.filename = "";

            if (FileChanged != null)
                FileChanged(filename);
        }

        private void Reset()
        {
            min_feed = double.MaxValue;
            max_feed = double.MinValue;
            min_x = double.MaxValue;
            max_x = double.MinValue;
            min_y = double.MaxValue;
            max_y = double.MinValue;
            min_z = double.MaxValue;
            max_z = double.MinValue;
        }

        // IMPORTANT: block must be in uppercase and terminated with \r
        public bool ParseBlock(string block, bool quiet)
        {

            const string ignore = "$!~?(";
            const string codes = "MTSGFXYZR";
            const string all = "MTFGPSXYZIJKR [](\r";
            const string special = "TSFXYZR";

            bool collect = false;
            string gcode = "";
            double value;
            List<string> gcodes = new List<string>();

            if (block.Length == 0 || ignore.Contains(block.Substring(0, 1)))
                return false;

            foreach (char c in block)
            {

                if (all.Contains(c))
                {

                    collect = false;

                    if (gcode != "")
                    {
                        gcodes.Add(gcode);
                        gcode = "";
                    }

                    if (c == '(')
                        break;

                    if (codes.Contains(c))
                    {
                        collect = true;
                        gcode += c;
                    }
                }
                else if (collect)
                    gcode += c;
            }

            foreach (string code in gcodes)
            {

                if (special.Contains(code.Substring(0, 1)))
                {

                    value = double.Parse(code.Remove(0, 1), CultureInfo.InvariantCulture);

                    switch (code.Substring(0, 1))
                    {

                        case "F":
                            min_feed = Math.Min(min_feed, value);
                            max_feed = Math.Max(max_feed, value);
                            break;

                        case "X":
                            min_x = Math.Min(min_x, value);
                            max_x = Math.Max(max_x, value);
                            break;

                        case "Y":
                            min_y = Math.Min(min_y, value);
                            max_y = Math.Max(max_y, value);
                            break;

                        case "Z":
                            min_z = Math.Min(min_z, value);
                            max_z = Math.Max(max_z, value);
                            break;

                        case "T":
                            if (!quiet && ToolChanged != null)
                            {
                                if(!ToolChanged((int)value))
                                    MessageBox.Show(string.Format("Tool {0} not associated with a profile!", value.ToString()), "GCode parser", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                    }

                }
                else switch (code)
                    {

                        case "G20":
                            break;


                    }
            }
            return true;
        }

    }
}
