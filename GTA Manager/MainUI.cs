using GTA_Manager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            Config config = new Config();

            labelVersion.Text += Assembly.GetCallingAssembly().GetName().Version.ToString().Substring(0, Assembly.GetCallingAssembly().GetName().Version.ToString().LastIndexOf("."));
            comboBoxLang.SelectedIndex = Int32.Parse(config.getSetting("Language"));

            buttonAdd.Text = Language.buttonAdd;
            buttonRemove.Text = Language.buttonRemove;
            buttonOpen.Text = Language.buttonOpen;
            buttonPlay.Text = Language.buttonPlay;
            buttonBrowse.Text = Language.buttonBrowse;
            comboBoxMode.Items.AddRange(new string[] { Language.comboBoxOnline, Language.comboBoxOffline });

            if (Boolean.Parse(config.getSetting("First")))
            {
                DialogResult dialogResult = MessageBox.Show(Language.MessageShortcut, Language.TitleShortcut, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    createShortcut();
                }

                config.setSetting("First", "False");

                config.save();
            }

            if (!Boolean.Parse(config.getSetting("Online")))
            {
                comboBoxMode.SelectedIndex = 1;
                checkBoxRage.Checked = Boolean.Parse(config.getSetting("Rage"));
            }
            else
            {
                Launcher.disableAllMods();

                comboBoxMode.SelectedIndex = 0;
                checkBoxRage.Checked = false;
                checkBoxRage.Enabled = false;            
            }

            if (!config.getSetting("Directory").Equals("") && File.Exists(config.getSetting("Directory") + "GTA5.exe"))
            {
                textBoxDirectory.Text = config.getSetting("Directory");
                textBoxDirectory.ForeColor = Color.Black;

                initTabs();
            }
            else
            {
                textBoxDirectory.Text = Language.textBoxDirectory;
            }
        }

        private void initTabs()
        {
            tabPageScriptHook = new TabPage();
            tabPageScriptHook.Location = new Point(0, 0);
            tabPageScriptHook.Name = "tabPageScriptHook";
            tabPageScriptHook.Text = "ScriptHookV";
            tabPageScriptHook.Size = new Size(200, 100);
            tabPageScriptHook.TabIndex = 0;
            tabPageScriptHook.UseVisualStyleBackColor = true;
            tabScriptHook = new TabPageControl();
            tabScriptHook.init(TabPageControl.Type.ASI);
            tabPageScriptHook.Controls.Add(tabScriptHook);
            tabControl.Controls.Add(tabPageScriptHook);

            Config config = new Config();

            if (Directory.Exists(config.getSetting("Directory") + @"scripts\"))
            {
                tabPageScriptHookDotNet = new TabPage();
                tabPageScriptHookDotNet.Location = new Point(0, 0);
                tabPageScriptHookDotNet.Name = "tabPageScriptHookDotNet";
                tabPageScriptHookDotNet.Text = "ScriptHookVDotNet";
                tabPageScriptHookDotNet.Size = new Size(200, 100);
                tabPageScriptHookDotNet.TabIndex = 1;
                tabPageScriptHookDotNet.UseVisualStyleBackColor = true;
                tabScriptHookDotNet = new TabPageControl();
                tabScriptHookDotNet.init(TabPageControl.Type.DOTNET);
                tabPageScriptHookDotNet.Controls.Add(tabScriptHookDotNet);
                tabControl.Controls.Add(tabPageScriptHookDotNet);
            }

            if (Directory.Exists(config.getSetting("Directory") + @"scripts\ScriptsDir-Lua\"))
            {
                tabPageLua = new TabPage();
                tabPageLua.Location = new Point(0, 0);
                tabPageLua.Name = "tabPageLua";
                tabPageLua.Text = "Lua";
                tabPageLua.Size = new Size(200, 100);
                tabPageLua.TabIndex = 2;
                tabPageLua.UseVisualStyleBackColor = true;
                tabLua = new TabPageControl();
                tabLua.init(TabPageControl.Type.LUA);
                tabPageLua.Controls.Add(tabLua);
                tabControl.Controls.Add(tabPageLua);

                tabPageLuaLegacy = new TabPage();
                tabPageLuaLegacy.Location = new Point(0, 0);
                tabPageLuaLegacy.Name = "tabPageLuaLegacy";
                tabPageLuaLegacy.Text = "LuaLegacy";
                tabPageLuaLegacy.Size = new Size(200, 100);
                tabPageLuaLegacy.TabIndex = 3;
                tabPageLuaLegacy.UseVisualStyleBackColor = true;
                tabLuaLegacy = new TabPageControl();
                tabLuaLegacy.init(TabPageControl.Type.LUALEGACY);
                tabPageLuaLegacy.Controls.Add(tabLuaLegacy);
                tabControl.Controls.Add(tabPageLuaLegacy);
            } else if (Directory.Exists(config.getSetting("Directory") + @"scripts\addins\"))
            {
                tabPageLuaLegacy = new TabPage();
                tabPageLuaLegacy.Location = new Point(0, 0);
                tabPageLuaLegacy.Name = "tabPageLuaLegacy";
                tabPageLuaLegacy.Text = "LuaLegacy";
                tabPageLuaLegacy.Size = new Size(200, 100);
                tabPageLuaLegacy.TabIndex = 3;
                tabPageLuaLegacy.UseVisualStyleBackColor = true;
                tabLuaLegacy = new TabPageControl();
                tabLuaLegacy.init(TabPageControl.Type.LUA);
                tabPageLuaLegacy.Controls.Add(tabLuaLegacy);
                tabControl.Controls.Add(tabPageLuaLegacy);
            }

            if (Directory.Exists(config.getSetting("Directory") + @"Plugins\"))
            {
                tabPageRage = new TabPage();
                tabPageRage.Location = new Point(0, 0);
                tabPageRage.Name = "tabPageRage";
                tabPageRage.Text = "Rage Plugin Hook";
                tabPageRage.Size = new Size(200, 100);
                tabPageRage.TabIndex = 4;
                tabPageRage.UseVisualStyleBackColor = true;
                tabRage = new TabPageControl();
                tabRage.init(TabPageControl.Type.RAGE);
                tabPageRage.Controls.Add(tabRage);
                tabControl.Controls.Add(tabPageRage);
            }

            if (Directory.Exists(config.getSetting("Directory") + @"Plugins\LSPDFR\"))
            {
                tabPageLSPDFR = new TabPage();
                tabPageLSPDFR.Location = new Point(0, 0);
                tabPageLSPDFR.Name = "tabPageLSPDFR";
                tabPageLSPDFR.Text = "LSPDFR";
                tabPageLSPDFR.Size = new Size(200, 100);
                tabPageLSPDFR.TabIndex = 5;
                tabPageLSPDFR.UseVisualStyleBackColor = true;
                tabLSPDFR = new TabPageControl();
                tabLSPDFR.init(TabPageControl.Type.LSPDFR);
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
            Config config = new Config();

            if (checkBoxRage.Checked)
            {
                if (!File.Exists(config.getSetting("Directory") + "RAGEPluginHook.exe"))
                {
                    MessageBox.Show(Language.MessageRage, Language.TitleRage, MessageBoxButtons.OK);
                    checkBoxRage.Checked = false;
                }
            }

            config.setSetting("Rage", checkBoxRage.Checked.ToString());
            config.save();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Process[] processesByName = Process.GetProcessesByName("GTA5");

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
            Config config = new Config();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = config.getSetting("Directory");
            openFileDialog.RestoreDirectory = false;

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string directory = getSelectedTab().getDirectory();
                string[] fileNames = openFileDialog.FileNames;
                string[] array = fileNames;

                foreach (string sourceFileName in array)
                {
                    string fullPath = Path.GetFullPath(sourceFileName);
                    string fileName = Path.GetFileName(sourceFileName);
                    File.Copy(sourceFileName, directory + fileName);

                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            Process[] processesByName = Process.GetProcessesByName("GTA5");

            if (processesByName.Length == 0)
            {
                TabPageControl selectedTab = getSelectedTab();
                string directory = getSelectedTab().getDirectory();

                if (MessageBox.Show(Language.MessageRemove, Language.TitleRemove, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Config config = new Config();

                    List<string> list = selectedTab.listBoxDisabled.SelectedItems.OfType<string>().ToList();

                    foreach (string item in list)
                    {
                        File.Delete(directory + item + ".DISABLE");
                        config.removeDisabled(selectedTab.getType().ToString(), item);
                        config.save();

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
            Process.Start("explorer.exe", getSelectedTab().getDirectory());
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            //string path = new Config().getSetting("Directory");

            //if (tabControl.SelectedTab.Name.Equals("tabPageScriptHook"))
            //{
            //    if (Directory.Exists(path + @"asi\"))
            //    {
            //        textBoxDirectory.Text = path + @"asi\";
            //    }
            //    else
            //    {
            //        textBoxDirectory.Text = path;
            //    }
            //}
            //else if (tabControl.SelectedTab.Name.Equals("tabPageScriptHookDotNet"))
            //{
            //    textBoxDirectory.Text = path + @"scripts\";
            //}
            //else if (tabControl.SelectedTab.Name.Equals("tabPageRage"))
            //{
            //    textBoxDirectory.Text = path + @"Plugins\";
            //}
            //else if (tabControl.SelectedTab.Name.Equals("tabPageLua"))
            //{
            //    textBoxDirectory.Text = path + @"scripts\addins\";
            //}
            //else if (tabControl.SelectedTab.Name.Equals("tabPageLSPDFR"))
            //{
            //    textBoxDirectory.Text = path + @"Plugins\LSPDFR\";
            //}
        }

        private void comboBoxLang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Config config = new Config();

            if (int.Parse(config.getSetting("Language")) != comboBoxLang.SelectedIndex)
            {
                config.setSetting("Language", comboBoxLang.SelectedIndex.ToString());
                config.save();

                MessageBox.Show(Language.MessageRestart, Language.TitleRestart, MessageBoxButtons.OK);
            }
        }

        private void comboBoxMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Config config = new Config();

            int num = 0;

            if (!Boolean.Parse(config.getSetting("Online")))
            {
                num = 1;
            }

            if (num == comboBoxMode.SelectedIndex)
            {
                return;
            }

            Process[] processesByName = Process.GetProcessesByName("GTA5");

            if (processesByName.Length == 0)
            {
                if (comboBoxMode.SelectedIndex == 0)
                {
                    Launcher.disableAllMods();
                    bool @checked = checkBoxRage.Checked;
                    checkBoxRage.Checked = false;
                    checkBoxRage.Enabled = false;
                    config.setSetting("Rage", @checked.ToString());
                    config.setSetting("Online", "true");
                }
                else
                {
                    Launcher.enableMods();
                    checkBoxRage.Enabled = true;
                    checkBoxRage.Checked = Boolean.Parse(config.getSetting("Rage"));
                    config.setSetting("Online", "false");
                }

                config.save();
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
            Process[] processesByName = Process.GetProcessesByName("GTA5");

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
            Config config = new Config();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = config.getSetting("Directory");
            openFileDialog.Filter = "GTA Executable |GTA5.exe";

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string directory = openFileDialog.FileName.Replace("GTA5.exe", "");

                config.setSetting("Directory", directory);
                config.save();

                textBoxDirectory.Text = directory;
                textBoxDirectory.ForeColor = Color.Black;

                tabControl.Controls.Clear();
                initTabs();
            }
        }
    }
}
