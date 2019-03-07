using System;
using System.Diagnostics;
using System.IO;

namespace GTA_Manager
{
    class Launcher
    {
        public static void launch(bool online, bool rage)
        {
            Config config = new Config();

            string path = config.getSetting("Directory");

            if (rage && !online)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = path;
                processStartInfo.FileName = "RagePluginHook.exe";
                Process.Start(processStartInfo);
            }
            else if (new FileInfo(path + "steam_api64.dll").Exists || new FileInfo(path + "steam_api.dll").Exists)
            {
                Uri uri = new Uri("steam://rungameid/271590");
                Process.Start(uri.AbsoluteUri);
            }
            else
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = path;
                processStartInfo.FileName = "GTAVLauncher.exe";
                Process.Start(processStartInfo);
            }
        }

        public static void enableMods()
        {
            string path = new Config().getSetting("Directory");

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
                enableMods(path + @"asi\", true, TabPageControl.Type.ASI);
            }

            enableMods(path, true, TabPageControl.Type.ASI);
            enableMods(path + @"scripts\", true, TabPageControl.Type.DOTNET);
            enableMods(path + @"Plugins\", true, TabPageControl.Type.RAGE);
            enableMods(path + @"scripts\addins\", true, TabPageControl.Type.LUA);
            enableMods(path + @"Plugins\LSPDFR\", true, TabPageControl.Type.LSPDFR);
        }

        public static void enableAllMods()
        {
            string path = new Config().getSetting("Directory");

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
                enableMods(path + @"asi\", false, TabPageControl.Type.ASI);
            }

            enableMods(path, false, TabPageControl.Type.ASI);
            enableMods(path + @"scripts\", false, TabPageControl.Type.DOTNET);
            enableMods(path + @"Plugins\", false, TabPageControl.Type.RAGE);
            enableMods(path + @"Plugins\LSPDFR\", false, TabPageControl.Type.LUA);
            enableMods(path + @"scripts\addins\", false, TabPageControl.Type.LSPDFR);
        }

        private static void enableMods(string directory, bool check, TabPageControl.Type type)
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
                        if (!new Config().getDisabled(type.ToString()).Contains(destFileName.Replace(directory, "")))
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

        public static void enableMod(TabPageControl.Type type, string fileName)
        {
            string path = new Config().getSetting("Directory");

            if (type.Equals(TabPageControl.Type.ASI))
            {
                fileName = ((!File.Exists(path + @"asi\" + fileName)) ? (path + fileName) : (path + @"asi\" + fileName));
            }
            else if (type.Equals(TabPageControl.Type.DOTNET))
            {
                fileName = path + @"scripts\" + fileName;
            }
            else if (type.Equals(TabPageControl.Type.RAGE))
            {
                fileName = path + @"Plugins\" + fileName;
            }
            else if (type.Equals(TabPageControl.Type.LUA))
            {
                fileName = path + @"scripts\addins\" + fileName;
            }
            else if (type.Equals(TabPageControl.Type.LSPDFR))
            {
                fileName = path + @"Plugins\LSPDFR\" + fileName;
            }

            File.Move(fileName + ".DISABLE", fileName.Replace(".DISABLE", ""));
        }

        public static void disableMods()
        {
            string path = new Config().getSetting("Directory");

            string[] args1 = new string[] { ".asi" };

            if (Directory.Exists(path + @"asi\"))
            {
                disableMods(path + @"asi\", true, TabPageControl.Type.ASI, args1);
            }

            disableMods(path, true, TabPageControl.Type.ASI, args1);

            string[] args2 = new string[] { ".dll", ".cs", ".vb" };
            disableMods(path + @"scripts\", true, TabPageControl.Type.DOTNET, args2);

            string[] args3 = new string[] { ".dll" };
            disableMods(path + @"Plugins\", true, TabPageControl.Type.RAGE, args3);

            string[] args4 = new string[] { ".lua" };
            disableMods(path + @"scripts\addins\", true, TabPageControl.Type.LUA, args4);

            string[] args5 = new string[] { ".dll" };
            disableMods(path + @"Plugins\LSPDFR\", true, TabPageControl.Type.LSPDFR, args5);
        }

        public static void disableAllMods()
        {
            string path = new Config().getSetting("Directory");

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
            disableMods(path, false, TabPageControl.Type.ASI, args);
        }

        public static void disableMod(TabPageControl.Type type, string fileName)
        {
            string path = new Config().getSetting("Directory");

            if (type.Equals(TabPageControl.Type.ASI))
            {
                fileName = ((!File.Exists(path + @"asi\" + fileName)) ? (path + fileName) : (path + @"asi\" + fileName));
            }
            else if (type.Equals(TabPageControl.Type.DOTNET))
            {
                fileName = path + @"scripts\" + fileName;
            }
            else if (type.Equals(TabPageControl.Type.RAGE))
            {
                fileName = path + @"Plugins\" + fileName;
            }
            else if (type.Equals(TabPageControl.Type.LUA))
            {
                fileName = path + @"scripts\\addins\" + fileName;
            }
            else if (type.Equals(TabPageControl.Type.LSPDFR))
            {
                fileName = path + @"Plugins\\LSPDFR\" + fileName;
            }

            if (File.Exists(fileName))
            {
                File.Move(fileName, fileName + ".DISABLE");
            }
        }

        private static void disableMods(string directory, bool check, TabPageControl.Type type, string[] args)
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
                                if (new Config().getDisabled(type.ToString()).Contains(file.Replace(directory, "")))
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
