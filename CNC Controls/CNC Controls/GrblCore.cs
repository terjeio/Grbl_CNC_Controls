/*
 * GrblCore.cs - part of CNC Controls library
 *
 * 2018-09-23 / Io Engineering (Terje Io)
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
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Data;

namespace CNC_Controls
{
    public class GrblConstants
    {
        public const byte
            CMD_EXIT = 0x03, // ctrl-C
            CMD_RESET = 0x18, // ctrl-X
            CMD_STOP = 0x19, // ctrl-Y
            CMD_JOG_CANCEL = 0x85,
            CMD_COOLANT_FLOOD_OVR_TOGGLE = 0xA0,
            CMD_COOLANT_MIST_OVR_TOGGLE = 0xA1;

        public const string
            CMD_STATUS_REPORT = "?",
            CMD_CYCLE_START = "~",
            CMD_FEED_HOLD = "!",
            CMD_UNLOCK = "$X",
            CMD_HOMING = "$H",
            CMD_CHECK = "$C",
            CMD_GETSETTINGS = "$$",
            CMD_GETPARSERSTATE = "$G",
            CMD_GETINFO = "$I",
            CMD_GETNGCPARAMETERS = "$#",
            CMD_GETPIDDATA = "#";
    }

    public enum GrblStates
    {
        Unknown = 0,
        Idle,
        Run,
        Tool,
        Hold,
        Check,
        Jog,
        Alarm,
        Door
    }

    public enum GrblSetting
    {
        PulseMicroseconds = 0,
        StepperIdleLockTime = 1,
        StepInvertMask = 2,
        DirInvertMask = 3,
        InvertStepperEnable = 4,
        LimitPinsInvertMask = 5,
        InvertProbePin = 6,
        StatusReportMask = 10,
        JunctionDeviation = 11,
        ArcTolerance = 12,
        ReportInches = 13,
        ControlInvertMask = 14,
        CoolantInvertMask = 15,
        SpindleInvertMask = 16,
        ControlPullUpDisableMask = 17,
        LimitPullUpDisableMask = 18,
        ProbePullUpDisable = 19,
        SoftLimitsEnable = 20,
        HardLimitsEnable = 21,
        HomingEnable = 22,
        HomingDirMask = 23,
        HomingFeedRate = 24,
        HomingSeekRate = 25,
        HomingDebounceDelay = 26,
        HomingPulloff = 27,
        G73Retract = 28,
        PulseDelayMicroseconds = 29,
        RpmMax = 30,
        RpmMin = 31,
        LaserMode = 32,
        PWMFreq = 33,
        PWMOffValue = 34,
        PWMMinValue = 35,
        PWMMaxValue = 36,
        StepperDeenergizeMask = 37,
        SpindlePPR  = 38,
        SpindlePGain  = 39,
        SpindleIGain  = 40,
        SpindleDGain  = 41,
        HomingLocateCycles = 43,
        HomingCycle_1  = 44,
        HomingCycle_2  = 45,
        HomingCycle_3  = 46,
        HomingCycle_4  = 47,
        HomingCycle_5  = 48,
        HomingCycle_6  = 49
    }

    public enum StreamingState
    {
        NoFile = 0,
        Idle,
        Send,
        SendMDI,
        Home,
        Halted,
        FeedHold,
        Stop,
        Reset,
        AwaitResetAck,
        Jogging,
        Disabled,
        Error
    }

    public struct GrblState
    {
        public GrblStates State;
        public int Substate;
        public System.Drawing.Color Color;
    }

    public class GrblLanguage
    {
        public static string language;

        static GrblLanguage()
        {
            GrblLanguage.language = "en_US";
        }
    }

    public class GrblStatusParameters
    {
        public string MPos {get; private set; }
        public string WPos { get; private set; }
        public string WCO { get; private set; }
        public string A { get; private set; }
        public string FS { get; private set; }
        public string MPG { get; private set; }

        public GrblStatusParameters()
        {
            MPos = WPos = WCO = A = FS = MPG = "";
        }

        public bool Set(string parameter, string value)
        {
            bool changed = false;

            switch (parameter)
            {
                case "MPos":
                    if ((changed = this.MPos != value))
                        this.MPos = value;
                    break;

                case "WPos":
                    if ((changed = this.WPos != value))
                        this.WPos = value;
                    break;

                case "A":
                    if ((changed = this.A != value))
                        this.A = value;
                    break;

                case "WCO":
                    if ((changed = this.WCO != value))
                        this.WCO = value;
                    break;

                case "FS":
                    if ((changed = this.FS != value))
                        this.FS = value;
                    break;

                case "MPG":
                    if ((changed = this.MPG != value))
                        this.MPG = value;
                    break;
            }

            return changed;
        }
    }

    public class CoordinateSystem
    {
        public string code;
        public double[] pos = new double[6];

        public CoordinateSystem()
        {
            for (int i = 0; i < pos.Length; i++)
                pos[i] = 0.0;
        }

        #region Attributes
        public double x { get { return pos[0]; } set { pos[0] = value; } }
        public double y { get { return pos[1]; } set { pos[1] = value; } }
        public double z { get { return pos[2]; } set { pos[2] = value; } }
        public double a { get { return pos[3]; } set { pos[3] = value; } }
        public double b { get { return pos[4]; } set { pos[4] = value; } }
        public double c { get { return pos[5]; } set { pos[5] = value; } }
        #endregion
    }

    public struct Tool
    {
        public string code;
        public double r;
        public double x;
        public double y;
        public double z;
    }

    public static class GrblInfo
    {
        public static string Version { get; private set; }
        public static string Options { get; private set; }
        public static int SerialBufferSize { get; private set; }
        public static int NumAxes { get; private set; }
        public static bool HasATC { get; private set; }

        public static void Get()
        {
            GrblInfo.NumAxes = 3;
            GrblInfo.SerialBufferSize = 128;
            GrblInfo.HasATC = false;

            Comms.com.DataReceived += new DataReceivedHandler(GrblInfo.Process);

            Comms.com.PurgeQueue();
            Comms.com.WriteCommand(GrblConstants.CMD_GETINFO);

            while (Comms.com.CommandState != Comms.State.ACK && Options == null)
                Application.DoEvents();

            Comms.com.DataReceived -= GrblInfo.Process;
        }

        private static void Process(string data)
        {
            if (data.StartsWith("[VER:"))
                Version = data.Substring(5, data.Length - 6);
            else if (data.StartsWith("[OPT:"))
            {
                Options = data.Substring(5, data.Length - 6);
                string[] s = Options.Split(',');
                GrblInfo.HasATC = s[0].Contains("T");
                if (s.Length > 2)
                    GrblInfo.SerialBufferSize = int.Parse(s[2], CultureInfo.InvariantCulture);
                if (s.Length > 3)
                    GrblInfo.NumAxes = int.Parse(s[3], CultureInfo.InvariantCulture);
            }
        }
    }

    public class GrblWorkParameters
    {
        public static double toolLengtOffset = 0.0f;
        public static CoordinateSystem probePosition;
        public static List<CoordinateSystem> offset = new List<CoordinateSystem>();
        public static List<Tool> tool = new List<Tool>();

        public static void Load()
        {
            offset.Clear();
            tool.Clear();

            Comms.com.DataReceived += new DataReceivedHandler(process);
            Comms.com.PurgeQueue();
            Comms.com.WriteCommand(GrblConstants.CMD_GETNGCPARAMETERS);

            while (Comms.com.CommandState != Comms.State.ACK)
                Application.DoEvents();

            Comms.com.DataReceived -= process;
        }

        private static string extractValues(string data, out string parameters)
        {
            int sep = data.IndexOf(":");
            parameters = data.Substring(sep + 1, data.IndexOf("]") - sep - 1);
            return data.Substring(1, sep - 1);
        }

        private static CoordinateSystem parseCoord(string gCode, string data)
        {
            CoordinateSystem workoffset = new CoordinateSystem();
            string[] s = data.Split(',');
            workoffset.code = gCode;

            for (int i = 0; i < s.Length; i++)
                workoffset.pos[i] = double.Parse(s[i], CultureInfo.InvariantCulture);

            return workoffset;
        }

        private static void AddTool(string gCode, string data)
        {
            Tool tool = new Tool();
            string[] s = data.Split(',');
            tool.code = gCode;
            tool.x = double.Parse(s[0], CultureInfo.InvariantCulture);
            tool.y = double.Parse(s[1], CultureInfo.InvariantCulture);
            tool.z = double.Parse(s[2], CultureInfo.InvariantCulture);
            GrblWorkParameters.tool.Add(tool);
        }

        private static void process(string data)
        {
            if (data != "ok")
            {
                string parameters, gCode = extractValues(data, out parameters);
                switch (gCode)
                {
                    case "G28":
                    case "G30":
                    case "G54":
                    case "G55":
                    case "G56":
                    case "G57":
                    case "G58":
                    case "G59":
                    case "G59.1":
                    case "G59.2":
                    case "G59.3":
                    case "G92":
                        offset.Add(parseCoord(gCode, parameters));
                        break;

                    case "T1":
                        AddTool(gCode, parameters);
                        break;

                    case "TLO":
                        string[] s = parameters.Split(',');
                        toolLengtOffset = double.Parse(s[0], CultureInfo.InvariantCulture);
                        break;

                    case "PRB":
                        probePosition = parseCoord(gCode, parameters.Substring(0, parameters.IndexOf(":") - 1));
                        break;
                }
            }
        }
    }

    public class GrblErrors
    {
        private static Dictionary<string, string> messages = null;

        static GrblErrors()
        {
            try
            {
                StreamReader file = new StreamReader(string.Format("{0}\\error_codes_{1}.csv", Application.StartupPath, GrblLanguage.language));

                if (file != null)
                {
                    messages = new Dictionary<string, string>();

                    string line = file.ReadLine();

                    line = file.ReadLine(); // Skip header  

                    while (line != null)
                    {
                        string[] columns = line.Split(',');

                        if (columns.Length == 3)
                            messages.Add(columns[0], columns[1] + ": " + columns[2]);

                        line = file.ReadLine();
                    }
                }

                file.Close();
            }
            catch
            {
            }
        }

        public static string GetMessage(string key)
        {
            string message = "";

            if (messages != null)
                messages.TryGetValue(key, out message);

            return message == "" ? string.Format("Error {0}", key) : message;
        }
    }

    public class GrblAlarms
    {
        private static Dictionary<string, string> messages = null;

        static GrblAlarms()
        {
            try
            {
                StreamReader file = new StreamReader(string.Format("{0}\\alarm_codes_{1}.csv", Application.StartupPath, GrblLanguage.language));

                if (file != null)
                {
                    messages = new Dictionary<string, string>();

                    string line = file.ReadLine();

                    line = file.ReadLine(); // Skip header  

                    while (line != null)
                    {
                        string[] columns = line.Split(',');

                        if (columns.Length == 3)
                            messages.Add(columns[0], columns[1] + ": " + columns[2]);

                        line = file.ReadLine();
                    }
                }

                file.Close();
            }
            catch
            {
            }
        }

        public static string GetMessage(string key)
        {
            string message = "";

            if (messages != null)
                messages.TryGetValue(key, out message);

            return message == "" ? string.Format("Alarm {0}", key) : message;
        }
    }

    public static class GrblSettings
    {
        public static DataTable data;

        static GrblSettings()
        {
            GrblSettings.data = new DataTable("Setting");

            GrblSettings.data.Columns.Add("Id", typeof(int));
            GrblSettings.data.Columns.Add("Name", typeof(string));
            GrblSettings.data.Columns.Add("Value", typeof(string));
            GrblSettings.data.Columns.Add("Unit", typeof(string));
            GrblSettings.data.Columns.Add("Description", typeof(string));
            GrblSettings.data.Columns.Add("DataType", typeof(string));
            GrblSettings.data.Columns.Add("DataFormat", typeof(string));
            GrblSettings.data.Columns.Add("Min", typeof(double));
            GrblSettings.data.Columns.Add("Max", typeof(double));
            GrblSettings.data.PrimaryKey = new DataColumn[] { GrblSettings.data.Columns["Id"] };
        }

        public static bool Loaded { get { return GrblSettings.data.Rows.Count > 0; } }

        public static double parseDouble(string value)
        {
            double result;

            value = value.Trim();

            if (value.Length == 0 || !double.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out result))
                result = double.NaN;

            return result;
        }

        public static string GetValue(GrblSetting key)
        {
            DataRow[] rows = GrblSettings.data.Select("Id = " + ((int)key).ToString());

            return rows.Count() == 1 ? (string)rows[0]["Value"] : null;
        }

        public static void Load()
        {
            GrblSettings.data.Clear();

            Comms.com.DataReceived += new DataReceivedHandler(GrblSettings.Process);

            Comms.com.PurgeQueue();
            Comms.com.WriteCommand(GrblConstants.CMD_GETSETTINGS);

            while (Comms.com.CommandState != Comms.State.ACK)
                Application.DoEvents();

            Comms.com.DataReceived -= GrblSettings.Process;

            try
            {
                StreamReader file = new StreamReader(string.Format("{0}\\setting_codes_{1}.txt", Application.StartupPath, GrblLanguage.language));

                if (file != null)
                {
                    string line = file.ReadLine();

                    line = file.ReadLine(); // Skip header  

                    while (line != null)
                    {
                        string[] columns = line.Split('\t');

                        if (columns.Length >= 6)
                        {
                            DataRow[] rows = GrblSettings.data.Select("Id=" + columns[0]);
                            if (rows.Count() == 1)
                            {
                                rows[0]["Name"] = columns[1];
                                rows[0]["Unit"] = columns[2];
                                rows[0]["DataType"] = columns[3];
                                rows[0]["DataFormat"] = columns[4];
                                rows[0]["Description"] = columns[5];
                                if (columns.Length >= 7)
                                    rows[0]["Min"] = parseDouble(columns[6]);
                                if (columns.Length >= 8)
                                    rows[0]["Max"] = parseDouble(columns[7]);
                                if ((string)rows[0]["DataType"] == "float")
                                    rows[0]["Value"] = GrblSettings.FormatFloat((string)rows[0]["Value"], (string)rows[0]["DataFormat"]);
                            }
                        }
                        line = file.ReadLine();
                    }
                }
                file.Close();
            }
            catch
            {
            }
            GrblSettings.data.AcceptChanges();
        }

        public static void Save()
        {
            DataTable Settings = GrblSettings.data.GetChanges();
            if (Settings != null)
            {
                foreach (DataRow Setting in Settings.Rows)
                {
                    Comms.com.WriteCommand(string.Format("${0}={1}", (int)Setting["Id"], (string)Setting["Value"]));
                    while (Comms.com.CommandState != Comms.State.ACK)
                        Application.DoEvents();
                }
                GrblSettings.data.AcceptChanges();
            }
        }

        public static void Backup(string filename)
        {
            if (GrblSettings.data != null) try
                {
                    StreamWriter file = new StreamWriter(filename);
                    if (file != null)
                    {
                        file.WriteLine('%');
                        foreach (DataRow Setting in GrblSettings.data.Rows)
                        {
                            file.WriteLine(string.Format("${0}={1}", (int)Setting["Id"], (string)Setting["Value"]));
                        }
                        file.WriteLine('%');
                        file.Close();
                    }
                }
                catch
                {
                }
        }

        public static string FormatFloat(string value, string format)
        {
            float fval;
            if (float.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out fval))
                value = fval.ToString(format, CultureInfo.InvariantCulture);
            return value;
        }

        private static void Process(string data)
        {
            if (data != "ok")
                GrblSettings.data.Rows.Add(new object[] { data.Substring(1, data.IndexOf("=") - 1), "", data.Substring(data.IndexOf("=") + 1), "", "", "", "", double.NaN, double.NaN });
        }

    }

    public class PollGrbl
    {
        System.Timers.Timer pollTimer = null;

        public void run()
        {
            this.pollTimer = new System.Timers.Timer();
            this.pollTimer.Elapsed += new System.Timers.ElapsedEventHandler(pollTimer_Elapsed);
            //  this.pollTimer.SynchronizingObject = this;
        }

        public void SetState(int PollInterval)
        {
            if (PollInterval != 0)
            {
                this.pollTimer.Interval = PollInterval;
                this.pollTimer.Start();
            }
            else
                this.pollTimer.Stop();
        }

        void pollTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Comms.com.WriteByte(Encoding.Default.GetBytes(GrblConstants.CMD_STATUS_REPORT)[0]);
        }
    }
}
