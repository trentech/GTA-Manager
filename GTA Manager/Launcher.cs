using System;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;

namespace GTA_Manager
{
    class Launcher
    {
        public static void launch(bool online, bool rage)
        {
            string path = Program.Config.Settings.Directory;

            if (rage && !online)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = path;
                processStartInfo.FileName = "RagePluginHook.exe";
                Process.Start(processStartInfo);
            }
            else if (new FileInfo(path + "steam_api64.dll").Exists || new FileInfo(path + "steam_api.dll").Exists)
            {
                Uri uri;
                if (Program.Config.Settings.Enhanced)
                {
                    uri = new Uri("steam://rungameid/3240220");
                } else
                {
                    uri = new Uri("steam://rungameid/271590");
                }
                    
                Process.Start(uri.AbsoluteUri);
            }
            else
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = path;

                if (Program.Config.Settings.Enhanced)
                {
                    processStartInfo.FileName = "GTAV_Enhanced.exe";
                }
                else
                {
                    processStartInfo.FileName = "GTAVLauncher.exe";
                }
                
                Process.Start(processStartInfo);
            }
        }

        public static void enableMods()
        {
            string path = Program.Config.Settings.Directory;

            string text = path + "dinput8.dll.DISABLE";

            if (File.Exists(text))
            {
                File.Move(text, text.Replace(".DISABLE", ""));
            }

            string text2 = path + "ScriptHookV.dll.DISABLE";

            if (File.Exists(text2))
            {
                File.Move(text2, text2.Replace(".DISABLE", ""));
            }

            if (Directory.Exists(path + @"asi\"))
            {
                enableMods(path + @"asi\", true, Type.ASI);
            }

            enableMods(path, true, Type.ASI);
            enableMods(path + @"scripts\", true, Type.DOTNET);
            enableMods(path + @"Plugins\", true, Type.RAGE);
            enableMods(path + @"scripts\addins\", true, Type.LUALEGACY);
            enableMods(path + @"scripts\ScriptsDir-Lua\Modules", true, Type.LUALEGACY);
            enableMods(path + @"scripts\ScriptsDir-Lua\", true, Type.LUA);         
            enableMods(path + @"Plugins\LSPDFR\", true, Type.LSPDFR);
        }

        public static void enableAllMods()
        {
            string path = Program.Config.Settings.Directory;

            string text = path + "dinput8.dll.DISABLE";

            if (File.Exists(text))
            {
                File.Move(text, text.Replace(".DISABLE", ""));
            }

            string text2 = path + "ScriptHookV.dll.DISABLE";

            if (File.Exists(text2))
            {
                File.Move(text2, text2.Replace(".DISABLE", ""));
            }

            if (Directory.Exists(path + @"asi\"))
            {
                enableMods(path + @"asi\", false, Type.ASI);
            }

            enableMods(path, false, Type.ASI);
            enableMods(path + @"scripts\", false, Type.DOTNET);
            enableMods(path + @"Plugins\", false, Type.RAGE);           
            enableMods(path + @"scripts\addins\", false, Type.LUALEGACY);
            enableMods(path + @"scripts\ScriptsDir-Lua\Modules\", false, Type.LUALEGACY);
            enableMods(path + @"scripts\ScriptsDir-Lua\", false, Type.LUA);
            enableMods(path + @"Plugins\LSPDFR\", false, Type.LSPDFR);
        }

        private static void enableMods(string directory, bool check, Type type)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }

            foreach (string file in Directory.GetFiles(directory))
            {
                if (!Directory.Exists(file) && file.Contains(".DISABLE"))
                {
                    string destFileName = file.Replace(".DISABLE", "");

                    if (check)
                    {
                        if (!Program.Config.DisabledItems.Contains(type, destFileName.Replace(directory, "")))
                        {
                            File.Move(file, destFileName);
                        }
                    }
                    else
                    {
                        File.Move(file, destFileName);
                    }
                }
            }
        }

        public static void enableMod(Type type, string fileName)
        {
            string path = Program.Config.Settings.Directory;

            if (type.Equals(Type.ASI))
            {
                fileName = ((!File.Exists(path + @"asi\" + fileName)) ? (path + fileName) : (path + @"asi\" + fileName));
            }
            else if (type.Equals(Type.DOTNET))
            {
                fileName = path + @"scripts\" + fileName;
            }
            else if (type.Equals(Type.RAGE))
            {
                fileName = path + @"Plugins\" + fileName;
            }
            else if (type.Equals(Type.LUA))
            {
                fileName = path + @"scripts\ScriptsDir-Lua\" + fileName;
            }
            else if (type.Equals(Type.LUALEGACY))
            {
                fileName = File.Exists(path + @"scripts\addins\" + fileName) ? path + @"scripts\addins\" + fileName : path + @"scripts\ScriptsDir-Lua\Modules\" + fileName;
            }
            else if (type.Equals(Type.LSPDFR))
            {
                fileName = path + @"Plugins\LSPDFR\" + fileName;
            }

            File.Move(fileName + ".DISABLE", fileName.Replace(".DISABLE", ""));
        }

        public static void disableMods()
        {
            string path = Program.Config.Settings.Directory;

            string[] args1 = new string[] { ".asi" };

            disableMods(path + @"asi\", true, Type.ASI, args1);
            disableMods(path, true, Type.ASI, args1);

            string[] args2 = new string[] { ".dll", ".cs", ".vb" };
            disableMods(path + @"scripts\", true, Type.DOTNET, args2);

            string[] args3 = new string[] { ".dll" };
            disableMods(path + @"Plugins\", true, Type.RAGE, args3);

            string[] args4 = new string[] { ".lua" };
            disableMods(path + @"scripts\addins\", true, Type.LUALEGACY, args4);
            disableMods(path + @"scripts\ScriptsDir-Lua\", true, Type.LUA, args4);
            disableMods(path + @"scripts\ScriptsDir-Lua\Modules", true, Type.LUALEGACY, args4);

            string[] args5 = new string[] { ".dll" };
            disableMods(path + @"Plugins\LSPDFR\", true, Type.LSPDFR, args5);
        }

        public static void disableAllMods()
        {
            string path = Program.Config.Settings.Directory;

            string text = path + "dinput8.dll";

            if (File.Exists(text))
            {
                File.Move(text, text + ".DISABLE");
            }

            string text2 = path + "ScriptHookV.dll";

            if (File.Exists(text2))
            {
                File.Move(text2, text2 + ".DISABLE");
            }

            string[] args = new string[1] { ".asi" };
            disableMods(path, false, Type.ASI, args);
        }

        public static void disableMod(Type type, string fileName)
        {
            string path = Program.Config.Settings.Directory;

            if (type.Equals(Type.ASI))
            {
                fileName = ((!File.Exists(path + @"asi\" + fileName)) ? (path + fileName) : (path + @"asi\" + fileName));
            }
            else if (type.Equals(Type.DOTNET))
            {
                fileName = path + @"scripts\" + fileName;
            }
            else if (type.Equals(Type.RAGE))
            {
                fileName = path + @"Plugins\" + fileName;
            }
            else if (type.Equals(Type.LUA))
            {
                fileName = path + @"scripts\ScriptsDir-Lua\" + fileName;
            }
            else if (type.Equals(Type.LUALEGACY))
            {
                fileName = File.Exists(path + @"scripts\addins\" + fileName) ? path + @"scripts\addins\" + fileName : path + @"scripts\ScriptsDir-Lua\Modules\" + fileName;
            }
            else if (type.Equals(Type.LSPDFR))
            {
                fileName = path + @"Plugins\LSPDFR\" + fileName;
            }

            if (File.Exists(fileName))
            {
                File.Move(fileName, fileName + ".DISABLE");
            }
        }

        private static void disableMods(string directory, bool check, Type type, string[] args)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }

            foreach (string file in Directory.GetFiles(directory))
            {
                if (!Directory.Exists(file))
                {
                    foreach (string str1 in args)
                    {
                        if (file.Contains(str1))
                        {
                            if (check)
                            {
                                if (Program.Config.DisabledItems.Contains(type, file.Replace(directory, "")))
                                {
                                    File.Move(file, file + ".DISABLE");
                                }
                            }
                            else if (!file.Contains(".DISABLE"))
                            {
                                File.Move(file, file + ".DISABLE");
                            }
                        }
                    }
                }
            }
        }
    }
}
