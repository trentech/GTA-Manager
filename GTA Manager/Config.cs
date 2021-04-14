using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GTA_Manager
{
    public class Config
    {
        private string CONFIG_PATH = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "gta_manager.ini";
        private FileIniDataParser parser = new FileIniDataParser();
        private IniData data;
        private KeyDataCollection settings;
        private KeyDataCollection disabled;

        public Config()
        {
            FileIniDataParser parser = new FileIniDataParser();

            if (!File.Exists(CONFIG_PATH))
            {
                IniData data = new IniData();

                data.Sections.AddSection("Settings");

                data["Settings"].AddKey("Directory", "");
                data["Settings"].AddKey("First", "True");
                data["Settings"].AddKey("Language", "0");
                data["Settings"].AddKey("Online", "False");
                data["Settings"].AddKey("Rage", "False");

                data.Sections.AddSection("Disabled");

                parser.WriteFile(CONFIG_PATH, data);
            }

            parser = new FileIniDataParser();
            data = parser.ReadFile(CONFIG_PATH);

            settings = data["Settings"];
            disabled = data["Disabled"];
        }

        public string getSetting(string key)
        {
            return settings[key];
        }

        public void setSetting(string key, string value)
        {
            settings[key] = value;
        }

        public List<string> getDisabled(string key)
        {
            if (disabled[key] == null)
            {
                return new List<string>();
            }

            return disabled[key].Split(',').ToList();
        }

        public void setDisabled(string key, string value)
        {
            disabled[key] = value;
        }

        public void addDisabled(string key, string value)
        {
            List<string> list;
            if (disabled[key] == null)
            {
                list = new List<string>();
            } else
            {
                list = disabled[key].Split(',').ToList();
            }

            list.Add(value);

            disabled[key] = string.Join(",", list);
        }

        public void removeDisabled(string key, string value)
        {
            List<string> list;
            if (disabled[key] == null)
            {
                list = new List<string>();
            }
            else
            {
                list = disabled[key].Split(',').ToList();
            }

            list.Remove(value);

            disabled[key] = string.Join(",", list);
        }

        public void save()
        {
            parser.WriteFile(CONFIG_PATH, data);
        }
    }
}
