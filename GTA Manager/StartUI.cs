using GTA_Manager.Properties;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace GTA_Manager
{
    public partial class StartUI : Form
    {
        public StartUI()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Config config = new Config();

            config.setSetting("Language", comboBox1.SelectedIndex.ToString());
            config.save();

            Thread.CurrentThread.CurrentCulture = new CultureInfo(Program.getRegionCode());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Program.getRegionCode());

            Hide();
            MainUI mainUI = new MainUI();
            Program.CenterToScreen(mainUI);
            mainUI.ShowDialog();
            Close();
        }
    }
}
