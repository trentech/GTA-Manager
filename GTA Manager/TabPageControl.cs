using GTA_Manager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GTA_Manager
{
    public partial class TabPageControl : UserControl
    {
        public enum Type
        {
            ASI,
            DOTNET,
            RAGE,
            LUA,
            LSPDFR
        }

        public TabPageControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public Type getType()
        {
            return type;
        }

        public string getDirectory()
        {
            return directory;
        }

        public List<string> getExtensions()
        {
            return extensions;
        }

        public void init(Type type)
        {
            labelDisabled.Text = Language.labelDisabled;
            labelEnabled.Text = Language.labelEnabled;

            this.type = type;
            Text = type.ToString();

            string directoryName = new Config().getSetting("Directory");

            switch (type)
            {
                case Type.ASI:
                    if (Directory.Exists(directoryName + @"asi\"))
                    {
                        directory = directoryName + @"asi\";
                    }
                    else
                    {
                        directory = directoryName;
                    }
                    extensions.Add(".asi");
                    break;
                case Type.DOTNET:
                    directory = directoryName + @"scripts\";
                    extensions.Add(".dll");
                    extensions.Add(".cs");
                    extensions.Add(".vb");
                    break;
                case Type.RAGE:
                    directory = directoryName + @"Plugins\";
                    extensions.Add(".dll");
                    break;
                case Type.LUA:
                    directory = directoryName + @"scripts\addins\";
                    extensions.Add(".lua");
                    break;
                case Type.LSPDFR:
                    directory = directoryName + @"Plugins\LSPDFR\";
                    extensions.Add(".dll");
                    break;
            }

            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = directory;
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

            foreach (string file in Directory.GetFiles(directory))
            {
                string fileName = file.Replace(".DISABLE", "");

                foreach (string extension in extensions)
                {
                    if (fileName.Contains(extension))
                    {
                        if (new Config().getDisabled(type.ToString()).Contains(file.Replace(directory, "")))
                        {
                            try
                            {
                                listBoxDisabled.Items.Add(fileName.Replace(directory, ""));
                            }
                            catch
                            {
                                Invoke((MethodInvoker)delegate { listBoxDisabled.Items.Add(fileName.Replace(directory, "")); });
                            }
                        }
                        else
                        {
                            try
                            {
                                listBoxEnabled.Items.Add(fileName.Replace(directory, ""));
                            }
                            catch
                            {
                                Invoke((MethodInvoker)delegate { listBoxEnabled.Items.Add(fileName.Replace(directory, "")); });
                            }
                        }
                    }
                }
            }
        }

        private void onDirectoryChange(object sender, FileSystemEventArgs e)
        {
            string @object = Path.GetExtension(e.FullPath) ?? string.Empty;
            List<string> list = new List<string>();
            list.AddRange(extensions);
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
                Process[] processesByName = Process.GetProcessesByName("GTA5");

                if (processesByName.Length == 0)
                {
                    Config config = new Config();

                    foreach (string item in list)
                    {
                        config.removeDisabled(type.ToString(), item);
                        config.save();

                        if (type.Equals(Type.ASI) && !Boolean.Parse(config.getSetting("Online")))
                        {
                            Launcher.enableMod(type, item);
                        }
                        else if (!type.Equals(Type.ASI))
                        {
                            Launcher.enableMod(type, item);
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
                Process[] processesByName = Process.GetProcessesByName("GTA5");

                if (processesByName.Length == 0)
                {
                    Config config = new Config();

                    foreach (string item in list)
                    {
                        config.addDisabled(type.ToString(), item);
                        config.save();

                        Launcher.disableMod(type, item);

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
                    string fileName = Path.GetFileName(text);

                    if(File.Exists(directory + fileName))
                    {
                        DialogResult result = MessageBox.Show(fileName + "already exists. Overwrite?", "Overwrite File?", MessageBoxButtons.YesNo);

                        if(result.Equals(DialogResult.Yes))
                        {
                            File.Copy(text, directory + fileName);
                        }
                    } else
                    {
                        File.Copy(text, directory + fileName);
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
            string str = (directory + listBoxEnabled.SelectedItem).Substring(0, (directory + listBoxEnabled.SelectedItem).LastIndexOf("."));

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
            processStartInfo.Arguments = "/select," + directory + listBoxEnabled.SelectedItem;
            Process process = Process.Start(processStartInfo);
        }

        private void listBoxDisabled_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string str = (directory + listBoxEnabled.SelectedItem).Substring(0, (directory + listBoxEnabled.SelectedItem).LastIndexOf("."));

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
            processStartInfo.Arguments = "/select," + directory + listBoxEnabled.SelectedItem;
            Process process = Process.Start(processStartInfo);
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
