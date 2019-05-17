/*
 * WizardConfig.cs - part of CNC Controls library for Grbl
 *
 * v0.01 / 2019-05-14 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2019, Io Engineering (Terje Io)
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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace CNC_Controls
{
    public interface IWizard
    {
        void InitUI();
    }

    [Serializable]
    public class ProfileData
    {
        public ProfileData()
        {
            PassDepthFirst = 0.1d;
            PassDepthLast = PassDepthLast = 0.02d;
            XClearance = 0.5d;
            ZClearance = 0.0d;
            RPM = 100.0d;
            Feedrate = 200.0d;
            FeedrateLast = 100.0d;
            CSSMaxRPM = 0.0d;
            CSS = false;
        }

        [XmlIgnore]
        public int Id {get; set; }

        public string Name {get; set; }
        public double PassDepthFirst;
        public double PassDepthLast;
        public double PassDepthMin;
        public double XClearance;
        public double ZClearance;
        public double Feedrate;
        public double FeedrateLast;
        public double FeedrateMax;
        public double RPM;
        public double CSSMaxRPM;
        public bool CSS;
    }

    public class LatheProfile
    {
        public List<ProfileData> profiles = new List<ProfileData>();

        private int id = 0;
        private string filename;

        public LatheProfile(string filename)
        {
            this.filename = Application.StartupPath + "\\" + filename;
        }

        public ProfileData Add(string name)
        {
            ProfileData data = new ProfileData();

            data.Name = name;
            data.Id = id++;

            profiles.Add(data);

            return data;
        }

        public void Save()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<ProfileData>));

            FileStream fsout = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                using (fsout)
                {
                    xs.Serialize(fsout, profiles);
                }
            }
            catch
            {
            } 
        }

        public void Load()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<ProfileData>));

            try
            {
                StreamReader reader = new StreamReader(filename);
                profiles = (List<ProfileData>)xs.Deserialize(reader);
                reader.Close();

                foreach (ProfileData profile in profiles)
                    profile.Id = id++;
            }
            catch
            {
            }
        }
    }

    public class WizardConfig
    {
        private IWizard dialog = null;
        public LatheProfile profile;

        public WizardConfig(IWizard dialog, string name)
        {
            this.dialog = dialog;
            zdir = -1.0d;
            xmode = LatheMode.Radius;
            xmodelock = false;

            this.Name = name;

            profile = new LatheProfile(name + "Profiles.xml");

            profile.Load();

            if (profile.profiles.Count() == 0)
            {
                Current = profile.Add("Default");

                profile.Save();
            }
            else
                Current = profile.profiles[0];
        }

        public string Name { get; private set; }

        public ProfileData Current1 { get; private set; }
        public ProfileData Current;

        public List<ProfileData> Profiles { get { return profile.profiles; } }

        public string ProfileName { get { return Current.Name; } }
        public double passdepth_first { get { return Current.PassDepthFirst; } }
        public double passdepth_last { get { return Current.PassDepthLast; } }
        public double passdepth_min { get { return Current.PassDepthMin; } }
        public double xclear { get { return Current.XClearance; } }
        public double zclear { get { return Current.ZClearance; } }
        public double feedrate { get { return Current.Feedrate; } }
        public double feedrate_last { get { return Current.FeedrateLast; } }
        public double feedrate_max { get { return Current.FeedrateMax; } }
        public double rpm { get { return Current.RPM; } }
        public bool css { get { return Current.CSS; } }
        public double css_max_rpm { get { return Current.CSSMaxRPM; } }

        public double rpm_min { get; private set; }
        public double rpm_max { get; private set; }
        public bool metric { get; private set; }
        public double xspeed { get; private set; }
        public double xaccel { get; private set; }
        public double zspeed { get; private set; }
        public double zaccel { get; private set; }
        public double zdir { get; private set; }
        public LatheMode xmode { get; private set; }
        public bool xmodelock { get; private set; }

        public ProfileData Add(string name)
        {
            return profile.Add(name);
        }

        public void Load()
        {
            XmlDocument config = new XmlDocument();

            config.Load(GrblInfo.IniName);

            foreach (XmlNode N in config.SelectNodes("Config/Lathe/*"))
            {
                switch (N.Name)
                {
                    case "ZDirection":
                        zdir = N.InnerText.Trim().ToUpper() == "POSITIVE" ? 1.0 : -1.0;
                        break;

                    case "XMode":
                        switch (N.InnerText.ToUpper())
                        {
                            case "DIAMETER":
                                xmodelock = true;
                                xmode = LatheMode.Diameter;
                                break;

                            case "RADIUS":
                                xmodelock = true;
                                xmode = LatheMode.Radius;
                                break;

                            default:
                                xmodelock = false;
                                break;
                        }
                        break;
                }
            }
        }

        public bool Update()
        {
            if (GrblSettings.Loaded)
            {
                xspeed = GrblSettings.GetDouble(GrblSetting.AxisSetting_XMaxRate);
                xaccel = GrblSettings.GetDouble(GrblSetting.AxisSetting_XAcceleration);
                zspeed = GrblSettings.GetDouble(GrblSetting.AxisSetting_ZMaxRate);
                zaccel = GrblSettings.GetDouble(GrblSetting.AxisSetting_ZAcceleration);
                rpm_min = GrblSettings.GetDouble(GrblSetting.RpmMin);
                rpm_max = GrblSettings.GetDouble(GrblSetting.RpmMax);

                GrblParserState.Get();

                metric = GrblParserState.IsActive("G21") != null;
                if (!xmodelock)
                    xmode = GrblInfo.LatheXMode;

                if (dialog != null)
                    dialog.InitUI(); // use event instead?
            }

            return GrblSettings.Loaded;
        }

        public void SelectProfile(ProfileData profile)
        {
            Current = profile;
            Current.XClearance = xclear;

            if (dialog != null)
                dialog.InitUI(); // use event instead?
        }

        public bool Update(ProfileData profile, double xclear, LatheMode xmode)
        {
            this.xmode = xmode;

            if (profile != null)
            {
                Current = profile;
                Current.XClearance = xclear;
            }

            this.profile.Save();

            if (dialog != null)
                dialog.InitUI(); // use event instead?

            return true;
        }
    }
}
