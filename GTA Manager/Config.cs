using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GTA_Manager
{
    public class Config
    {
        [JsonProperty]
        public Settings Settings { get; set; }
        [JsonProperty]
        public DisabledItems DisabledItems { get; set; }
        [JsonIgnore]
        public static string PATH = AppDomain.CurrentDomain.BaseDirectory;
        [JsonIgnore]
        private static string CONFIG = Path.Combine(PATH, "config.json");

        public Config()
        {
            Settings = new Settings();
            DisabledItems = new DisabledItems();
        }

        public static Config Get()
        {
            Config config;

            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
            }

            if (!File.Exists(CONFIG))
            {
                config = new Config();

                File.WriteAllText(CONFIG, JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            else
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(CONFIG));
            }

            return config;
        }

        public void Save()
        {
            File.WriteAllText(CONFIG, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
    public enum Type
    {
        ASI,
        DOTNET,
        RAGE,
        LUA,
        LUALEGACY,
        LSPDFR
    }

    public class Settings
    {
        public string Directory { get; set; }
        public bool First { get; set; } = true;
        public int Language { get; set; } = 0;
        public bool Online { get; set; } = false;
        public bool Rage { get; set; } = false;
    }

    public class DisabledItems
    {
        [JsonProperty]
        private List<string> ASI = new List<string>();
        [JsonProperty]
        private List<string> DOTNET = new List<string>();
        [JsonProperty]
        private List<string> LUA = new List<string>();
        [JsonProperty]
        private List<string> LUALEGACY = new List<string>();
        [JsonProperty]
        private List<string> LSPDFR = new List<string>();

        public bool Contains(Type type, string name)
        {
            switch (type)
            {
                case Type.ASI:
                    return ASI.Contains(name);
                case Type.DOTNET:
                    return DOTNET.Contains(name);
                case Type.LUA:
                    return LUA.Contains(name);
                case Type.LUALEGACY:
                    return LUALEGACY.Contains(name);
                case Type.LSPDFR:
                    return LSPDFR.Contains(name);
                default: return false;
            }
        }

        public void Add(Type type, string name)
        {
            switch(type)
            {
                case Type.ASI:
                    if(!ASI.Contains(name)) { ASI.Add(name); }
                    break;
                case Type.DOTNET:
                    if (!DOTNET.Contains(name)) { DOTNET.Add(name); }
                    break;
                case Type.LUA:
                    if (!LUA.Contains(name)) { LUA.Add(name); }
                    break;
                case Type.LUALEGACY:
                    if (!LUALEGACY.Contains(name)) { LUALEGACY.Add(name); }
                    break;
                case Type.LSPDFR:
                    if (!LSPDFR.Contains(name)) { LSPDFR.Add(name); }
                    break;
            }

        }

        public void Remove(Type type, string name) 
        {
            switch (type)
            {
                case Type.ASI:
                    if (!ASI.Contains(name)) { ASI.Remove(name); }
                    break;
                case Type.DOTNET:
                    if (!DOTNET.Contains(name)) { DOTNET.Remove(name); }
                    break;
                case Type.LUA:
                    if (!LUA.Contains(name)) { LUA.Remove(name); }
                    break;
                case Type.LUALEGACY:
                    if (!LUALEGACY.Contains(name)) { LUALEGACY.Remove(name); }
                    break;
                case Type.LSPDFR:
                    if (!LSPDFR.Contains(name)) { LSPDFR.Remove(name); }
                    break;
            }
        }
    }
}
