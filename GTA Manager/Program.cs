using GTA_Manager.Properties;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace GTA_Manager
{
    static class Program
    {
        public static Config Config;

        [STAThread]
        private static void Main()
        {
            Config = Config.Get();

            Thread.CurrentThread.CurrentCulture = new CultureInfo(getRegionCode());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(getRegionCode());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Config.Settings.First)
            {
                StartUI startUI = new StartUI();
                CenterToScreen(startUI);
                Application.Run(startUI);
            }
            else
            {
                MainUI mainUI = new MainUI();
                CenterToScreen(mainUI);
                Application.Run(mainUI);
            }
        }

        public static void open()
        {
            new StartUI().Show();
        }

        public static string getRegionCode()
        {
            switch (Config.Settings.Language)
            {
                case 0:
                    return "en-US";
                case 1:
                    return "de-DE";
                case 2:
                    return "es-ES";
                case 3:
                    return "fr-FR";
                case 4:
                    return "el-GR";
                case 5:
                    return "pl-PL";
                case 6:
                    return "pt-BR";
                case 7:
                    return "it-IT";
                case 8:
                    return "ru-RU";
                case 9:
                    return "he-IL";
                case 10:
                    return "da-DK";
                case 11:
                    return "tr-TR";
                default:
                    return null;
            }
        }

        public static void CenterToScreen(Form form)
        {
            Screen screen = Screen.FromControl(form);
            Rectangle workingArea = screen.WorkingArea;

            form.Location = new Point
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - form.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - form.Height) / 2)
            };
        }
    }
}
