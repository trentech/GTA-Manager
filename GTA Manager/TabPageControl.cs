using GTA_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GTA_Manager
{
    public partial class TabPageControl : UserControl
    {

        public Type Type { get; }
        public string Path { get; set; }
        private List<string> Extensions { get; } = new List<string>();

        public TabPageControl(Type Type)
        {
            this.Type = Type;
            InitializeComponent();
            Dock = DockStyle.Fill;
            Init();
        }

        public void Init()
        {
            labelDisabled.Text = Language.labelDisabled;
            labelEnabled.Text = Language.labelEnabled;

            Text = Type.ToString();

            string directoryName = Config.Get().Settings.Directory;

            switch (Type)
            {
                case Type.ASI:
                    if (Directory.Exists(directoryName + @"asi\"))
                    {
                        Path = directoryName + @"asi\";
                    }
                    else
                    {
                        Path = directoryName;
                    }
                    Extensions.Add(".asi");
                    break;
                case Type.DOTNET:
                    Path = directoryName + @"scripts\";
                    Extensions.Add(".dll");
                    Extensions.Add(".cs");
                    Extensions.Add(".vb");
                    break;
                case Type.RAGE:
                    Path = directoryName + @"Plugins\";
                    Extensions.Add(".dll");
                    break;
                case Type.LUA:
                    Path = directoryName + @"scripts\ScriptsDir-Lua\";
                    Extensions.Add(".lua");
                    break;
                case Type.LUALEGACY:
                    if (Directory.Exists(directoryName + @"scripts\ScriptsDir-Lua\"))
                    {
                        Path = directoryName + @"scripts\ScriptsDir-Lua\Modules\";
                    }
                    else
                    {
                        Path = directoryName + @"scripts\addins\";
                    }
                    Extensions.Add(".lua");
                    break;
                case Type.LSPDFR:
                    Path = directoryName + @"Plugins\LSPDFR\";
                    Extensions.Add(".dll");
                    break;
            }

            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = Path;
            fileSystemWatcher.Filter = "*.*";
            fileSystemWatcher.Created += onDirectoryChange;
            fileSystemWatcher.Deleted += onDirectoryChange;
            fileSystemWatcher.EnableRaisingEvents = true;

            updateList();
        }

        private void updateList()
        {
            try
            {
                listBoxEnabled.Items.Clear();
                listBoxDisabled.Items.Clear();
            }
            catch
            {
                Invoke((MethodInvoker)delegate { listBoxEnabled.Items.Clear(); });
                Invoke((MethodInvoker)delegate { listBoxDisabled.Items.Clear(); });
            }

            foreach (string file in Directory.GetFiles(Path))
            {
                string fileName = file.Replace(".DISABLE", "");

                foreach (string extension in Extensions)
                {
                    if (fileName.Contains(extension))
                    {
                        if (Config.Get().DisabledItems.Contains(Type, file.Replace(Path, "")))
                        {
                            try
                            {
                                listBoxDisabled.Items.Add(fileName.Replace(Path, ""));
                            }
                            catch
                            {
                                Invoke((MethodInvoker)delegate { listBoxDisabled.Items.Add(fileName.Replace(Path, "")); });
                            }
                        }
                        else
                        {
                            try
                            {
                                listBoxEnabled.Items.Add(fileName.Replace(Path, ""));
                            }
                            catch
                            {
                                Invoke((MethodInvoker)delegate { listBoxEnabled.Items.Add(fileName.Replace(Path, "")); });
                            }
                        }
                    }
                }
            }
        }

        private void onDirectoryChange(object sender, FileSystemEventArgs e)
        {
            string @object = System.IO.Path.GetExtension(e.FullPath) ?? string.Empty;
            List<string> list = new List<string>();
            list.AddRange(Extensions);
            list.Add(".DISABLE");

            if (list.Any(@object.Equals))
            {
                updateList();
            }
        }

        private void buttonEnable_Click(object sender, EventArgs e)
        {
            List<string> list = listBoxDisabled.SelectedItems.OfType<string>().ToList();

            if (list != null)
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
                    foreach (string item in list)
                    {
                        config.DisabledItems.Remove(Type, item);
                        config.Save();

                        if (Type.Equals(Type.ASI) && !config.Settings.Online)
                        {
                            Launcher.enableMod(Type, item);
                        }
                        else if (!Type.Equals(Type.ASI))
                        {
                            Launcher.enableMod(Type, item);
                        }

                        listBoxEnabled.Items.Add(item);
                        listBoxDisabled.Items.Remove(item);
                    }

                    listBoxDisabled.ClearSelected();
                }
                else
                {
                    MessageBox.Show(Language.MessageChanges, Language.TitleRunning, MessageBoxButtons.OK);
                }
            }
        }

        private void buttonDisable_Click(object sender, EventArgs e)
        {
            List<string> list = listBoxEnabled.SelectedItems.OfType<string>().ToList();

            if (list != null)
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
                    foreach (string item in list)
                    {
                        config.DisabledItems.Add(Type, item);

                        config.Save();

                        Launcher.disableMod(Type, item);

                        listBoxDisabled.Items.Add(item);
                        listBoxEnabled.Items.Remove(item);
                    }

                    listBoxEnabled.ClearSelected();
                }
                else
                {
                    MessageBox.Show(Language.MessageChanges, Language.TitleRunning, MessageBoxButtons.OK);
                }
            }
        }

        private void listBoxEnabled_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
                string[] array2 = array;

                foreach (string text in array2)
                {
                    string fileName = System.IO.Path.GetFileName(text);

                    if(File.Exists(Path + fileName))
                    {
                        DialogResult result = MessageBox.Show(fileName + "already exists. Overwrite?", "Overwrite File?", MessageBoxButtons.YesNo);

                        if(result.Equals(DialogResult.Yes))
                        {
                            File.Copy(text, Path + fileName);
                        }
                    } else
                    {
                        File.Copy(text, Path + fileName);
                    }                
                }
            }
        }

        private void listBoxEnabled_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBoxEnabled_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string str = (Path + listBoxEnabled.SelectedItem).Substring(0, (Path + listBoxEnabled.SelectedItem).LastIndexOf("."));

            if (File.Exists(str + ".ini"))
            {
                Process.Start(str + ".ini");
                return;
            }

            if (File.Exists(str + ".xml"))
            {
                Process.Start(str + ".xml");
                return;
            }

            if (File.Exists(str + ".txt"))
            {
                Process.Start(str + ".txt");
                return;
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "explorer.exe";
            processStartInfo.Arguments = "/select," + Path + listBoxEnabled.SelectedItem;
            Process.Start(processStartInfo);
        }

        private void listBoxDisabled_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string str = (Path + listBoxEnabled.SelectedItem).Substring(0, (Path + listBoxEnabled.SelectedItem).LastIndexOf("."));

            if (File.Exists(str + ".ini"))
            {
                Process.Start(str + ".ini");
                return;
            }

            if (File.Exists(str + ".xml"))
            {
                Process.Start(str + ".xml");
                return;
            }

            if (File.Exists(str + ".txt"))
            {
                Process.Start(str + ".txt");
                return;
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "explorer.exe";
            processStartInfo.Arguments = "/select," + Path + listBoxEnabled.SelectedItem;
            Process.Start(processStartInfo);
        }

        private void listBoxEnabled_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> list = listBoxEnabled.SelectedItems.OfType<int>().ToList();
            listBoxDisabled.ClearSelected();

            foreach (int item in list)
            {
                listBoxEnabled.SetSelected(item, value: true);
            }
        }

        private void listBoxDisabled_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> list = listBoxDisabled.SelectedItems.OfType<int>().ToList();
            listBoxEnabled.ClearSelected();

            foreach (int item in list)
            {
                listBoxDisabled.SetSelected(item, value: true);
            }
        }
    }
}
