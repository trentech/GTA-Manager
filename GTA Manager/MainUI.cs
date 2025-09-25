using GTA_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GTA_Manager
{
    public partial class MainUI : Form
    {
        private TabPageControl tabScriptHook;
        private TabPageControl tabScriptHookDotNet;
        private TabPageControl tabRage;
        private TabPageControl tabLua;
        private TabPageControl tabLuaLegacy;
        private TabPageControl tabLSPDFR;

        public MainUI()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            Config config = Config.Get();

            labelVersion.Text += Assembly.GetCallingAssembly().GetName().Version.ToString().Substring(0, Assembly.GetCallingAssembly().GetName().Version.ToString().LastIndexOf("."));
            comboBoxLang.SelectedIndex = config.Settings.Language;

            buttonAdd.Text = Language.buttonAdd;
            buttonRemove.Text = Language.buttonRemove;
            buttonOpen.Text = Language.buttonOpen;
            buttonPlay.Text = Language.buttonPlay;
            buttonBrowse.Text = Language.buttonBrowse;
            comboBoxMode.Items.AddRange(new string[] { Language.comboBoxOnline, Language.comboBoxOffline });

            if (config.Settings.First)
            {
                DialogResult dialogResult = MessageBox.Show(Language.MessageShortcut, Language.TitleShortcut, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    createShortcut();
                }

                config.Settings.First = false;
                config.Save();
            }

            if (!config.Settings.Online)
            {
                comboBoxMode.SelectedIndex = 1;
                checkBoxRage.Checked = config.Settings.Rage;
            }
            else
            {
                Launcher.disableAllMods();

                comboBoxMode.SelectedIndex = 0;
                checkBoxRage.Checked = false;
                checkBoxRage.Enabled = false;            
            }

            if (File.Exists(config.Settings.Directory + "GTA5.exe") || File.Exists(config.Settings.Directory + "GTA5_Enhanced.exe"))
            {
                textBoxDirectory.Text = config.Settings.Directory;
                textBoxDirectory.ForeColor = Color.Black;

                initTabs();
            }
            else
            {
                textBoxDirectory.Text = Language.textBoxDirectory;
            }
        }

        private TabPage InitTabPage(string name, string text, int index)
        {
            TabPage tabPage = new TabPage();

            tabPage.Location = new Point(0, 0);
            tabPage.Name = name;
            tabPage.Text = text;
            tabPage.Size = new Size(200, 100);
            tabPage.TabIndex = index;
            tabPage.UseVisualStyleBackColor = true;

            return tabPage;
        }
        private void initTabs()
        {
            tabPageScriptHook = InitTabPage("tabPageScriptHook", "ScriptHookV", 0);

            tabScriptHook = new TabPageControl(Type.ASI);

            tabPageScriptHook.Controls.Add(tabScriptHook);
            tabControl.Controls.Add(tabPageScriptHook);

            Config config = Config.Get();

            if (Directory.Exists(config.Settings.Directory + @"scripts\"))
            {
                tabPageScriptHookDotNet = InitTabPage("tabPageScriptHookDotNet", "ScriptHookVDotNet", 1);

                tabScriptHookDotNet = new TabPageControl(Type.DOTNET);

                tabPageScriptHookDotNet.Controls.Add(tabScriptHookDotNet);
                tabControl.Controls.Add(tabPageScriptHookDotNet);
            }

            if (Directory.Exists(config.Settings.Directory + @"scripts\ScriptsDir-Lua\"))
            {
                tabPageLua = InitTabPage("tabPageLua", "Lua", 2);

                tabLua = new TabPageControl(Type.LUA);

                tabPageLua.Controls.Add(tabLua);
                tabControl.Controls.Add(tabPageLua);

                tabPageLuaLegacy = InitTabPage("tabPageLuaLegacy", "Lua Legacy", 3);

                tabLuaLegacy = new TabPageControl(Type.LUALEGACY);

                tabPageLuaLegacy.Controls.Add(tabLuaLegacy);
                tabControl.Controls.Add(tabPageLuaLegacy);
            } else if (Directory.Exists(config.Settings.Directory + @"scripts\addins\"))
            {
                tabPageLuaLegacy = InitTabPage("tabPageLuaLegacy", "Lua Legacy", 3);

                tabLuaLegacy = new TabPageControl(Type.LUALEGACY);

                tabPageLuaLegacy.Controls.Add(tabLuaLegacy);
                tabControl.Controls.Add(tabPageLuaLegacy);
            }

            if (Directory.Exists(config.Settings.Directory + @"Plugins\"))
            {
                tabPageRage = InitTabPage("tabPageRage", "Rage Plugin Hook", 4);

                tabRage = new TabPageControl(Type.RAGE);

                tabPageRage.Controls.Add(tabRage);
                tabControl.Controls.Add(tabPageRage);
            }

            if (Directory.Exists(config.Settings.Directory + @"Plugins\LSPDFR\"))
            {
                tabPageLSPDFR = InitTabPage("tabPageLSPDFR", "LSPDFR", 5);

                tabLSPDFR = new TabPageControl(Type.LSPDFR);

                tabPageLSPDFR.Controls.Add(tabLSPDFR);
                tabControl.Controls.Add(tabPageLSPDFR);
            }

            Launcher.disableMods();
        }

        private TabPageControl getSelectedTab()
        {
            if (tabControl.SelectedTab.Name.Equals("tabPageScriptHook"))
            {
                return tabScriptHook;
            }
            if (tabControl.SelectedTab.Name.Equals("tabPageScriptHookDotNet"))
            {
                return tabScriptHookDotNet;
            }
            if (tabControl.SelectedTab.Name.Equals("tabPageRage"))
            {
                return tabRage;
            }
            if (tabControl.SelectedTab.Name.Equals("tabPageLua"))
            {
                return tabLua;
            }
            if (tabControl.SelectedTab.Name.Equals("tabPageLuaLegacy"))
            {
                return tabLuaLegacy;
            }
            if (tabControl.SelectedTab.Name.Equals("tabPageLSPDFR"))
            {
                return tabLSPDFR;
            }
            return null;
        }

        private void createShortcut()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (StreamWriter streamWriter = new StreamWriter(folderPath + "\\GTA Manager.url"))
            {
                string location = Assembly.GetExecutingAssembly().Location;
                streamWriter.WriteLine("[InternetShortcut]");
                streamWriter.WriteLine("URL=file:///" + Path.GetDirectoryName(Application.ExecutablePath) + "\\GTA Manager.exe");
                streamWriter.WriteLine("IconIndex=0");
                string str = location.Replace('\\', '/');
                streamWriter.WriteLine("IconFile=" + str);
                streamWriter.Flush();
            }
        }

        private void checkBoxRage_CheckedChanged(object sender, EventArgs e)
        {
            Config config = Config.Get();

            if (checkBoxRage.Checked)
            {
                if (!File.Exists(config.Settings.Directory + "RAGEPluginHook.exe"))
                {
                    MessageBox.Show(Language.MessageRage, Language.TitleRage, MessageBoxButtons.OK);
                    checkBoxRage.Checked = false;
                }
            }

            config.Settings.Rage = checkBoxRage.Checked;
            config.Save();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Process[] processesByName;

            if (Config.Get().Settings.Enhanced)
            {
                processesByName = Process.GetProcessesByName("GTA5_Enhanced");
            }
            else
            {
                processesByName = Process.GetProcessesByName("GTA5");
            }

            if (processesByName.Length == 0)
            {
                if (comboBoxMode.SelectedText.Equals("Online Mode"))
                {
                    Launcher.launch(true, checkBoxRage.Checked);
                }
                else
                {
                    Launcher.launch(false, checkBoxRage.Checked);
                }
            }
            else
            {
                MessageBox.Show(Language.MessageRunning, Language.TitleRunning, MessageBoxButtons.OK);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Config.Get().Settings.Directory;
            openFileDialog.RestoreDirectory = false;

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string directory = getSelectedTab().Path;
                string[] fileNames = openFileDialog.FileNames;
                string[] array = fileNames;

                foreach (string sourceFileName in array)
                {
                    string fileName = Path.GetFileName(sourceFileName);
                    File.Copy(sourceFileName, directory + fileName);

                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            Config config = Config.Get();
            Process[] processesByName;

            if (config.Settings.Enhanced)
            {
                processesByName = Process.GetProcessesByName("GTA5_Enhanced");
            }
            else
            {
                processesByName = Process.GetProcessesByName("GTA5");
            }

            if (processesByName.Length == 0)
            {
                TabPageControl selectedTab = getSelectedTab();
                string directory = getSelectedTab().Path;

                if (MessageBox.Show(Language.MessageRemove, Language.TitleRemove, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    List<string> list = selectedTab.listBoxDisabled.SelectedItems.OfType<string>().ToList();

                    foreach (string item in list)
                    {
                        File.Delete(directory + item + ".DISABLE");
                        config.DisabledItems.Remove(selectedTab.Type, item);
                        config.Save();

                        if (Directory.GetFiles(directory) != null)
                        {
                            string[] files = Directory.GetFiles(directory);

                            foreach (string text in files)
                            {
                                if (text.Replace(directory, "").ToLower().Contains(item.ToLower().Substring(0, item.LastIndexOf("."))))
                                {
                                    File.Delete(text);
                                }
                            }
                        }
                    }

                    List<string> list2 = selectedTab.listBoxEnabled.SelectedItems.OfType<string>().ToList();

                    foreach (string item2 in list2)
                    {
                        File.Delete(directory + item2);

                        if (Directory.GetFiles(directory) != null)
                        {
                            string[] files = Directory.GetFiles(directory);

                            foreach (string text in files)
                            {
                                if (text.Replace(directory, "").ToLower().Contains(item2.ToLower().Substring(0, item2.LastIndexOf("."))))
                                {
                                    File.Delete(text);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(Language.MessageChanges, Language.TitleRunning, MessageBoxButtons.OK);
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", getSelectedTab().Path);
        }

        private void comboBoxLang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Config config = Config.Get();

            if (config.Settings.Language != comboBoxLang.SelectedIndex)
            {
                config.Settings.Language = comboBoxLang.SelectedIndex;
                config.Save();

                MessageBox.Show(Language.MessageRestart, Language.TitleRestart, MessageBoxButtons.OK);
            }
        }

        private void comboBoxMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = 0;

            Config config = Config.Get();

            if (!config.Settings.Online)
            {
                num = 1;
            }

            if (num == comboBoxMode.SelectedIndex)
            {
                return;
            }

            Process[] processesByName;

            if (config.Settings.Enhanced)
            {
                processesByName = Process.GetProcessesByName("GTA5_Enhanced");
            }
            else
            {
                processesByName = Process.GetProcessesByName("GTA5");
            }

            if (processesByName.Length == 0)
            {
                if (comboBoxMode.SelectedIndex == 0)
                {
                    Launcher.disableAllMods();
                    bool @checked = checkBoxRage.Checked;
                    checkBoxRage.Checked = false;
                    checkBoxRage.Enabled = false;
                    config.Settings.Rage = @checked;
                    config.Settings.Online = true;
                }
                else
                {
                    Launcher.enableMods();
                    checkBoxRage.Enabled = true;
                    checkBoxRage.Checked = config.Settings.Rage;
                    config.Settings.Online = false;
                }

                config.Save();
            }
            else
            {
                if (comboBoxMode.SelectedIndex == 0)
                {
                    comboBoxMode.SelectedIndex = 1;
                }
                else
                {
                    comboBoxMode.SelectedIndex = 0;
                }

                MessageBox.Show(Language.MessageChanges, Language.TitleRunning, MessageBoxButtons.OK);
            }
        }

        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] processesByName;

            if (Config.Get().Settings.Enhanced)
            {
                processesByName = Process.GetProcessesByName("GTA5_Enhanced");
            } else
            {
                processesByName = Process.GetProcessesByName("GTA5");
            }

            if (processesByName.Length == 0)
            {
                Launcher.enableAllMods();
                return;
            }

            e.Cancel = true;
            MessageBox.Show(Language.MessageClose, Language.TitleRunning, MessageBoxButtons.OK);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            Config config = Config.Get();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = config.Settings.Directory;
            openFileDialog.Filter = "GTA Executable |GTA5.exe;GTA5_Enhanced.exe";

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string directory;

                if (openFileDialog.FileName.EndsWith("GTA5_Enhanced.exe"))
                {
                    directory = openFileDialog.FileName.Replace("GTA5_Enhanced.exe", "");
                    config.Settings.Enhanced = true;
                } else
                {
                    directory = openFileDialog.FileName.Replace("GTA5.exe", "");
                }

                config.Settings.Directory = directory;
                config.Save();

                textBoxDirectory.Text = directory;
                textBoxDirectory.ForeColor = Color.Black;

                tabControl.Controls.Clear();
                initTabs();
            }
        }
    }
}
