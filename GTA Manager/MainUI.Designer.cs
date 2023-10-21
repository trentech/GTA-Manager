using System.IO;
using System.Windows.Forms;

namespace GTA_Manager
{
    partial class MainUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label labelVersion;
        private Label labelAuthor;
        private TabControl tabControl;
        private CheckBox checkBoxRage;
        private TextBox textBoxDirectory;
        private Button buttonPlay;
        private Button buttonAdd;
        private Button buttonRemove;
        private ComboBox comboBoxLang;
        private Button buttonOpen;
        private ComboBox comboBoxMode;
        private TabPage tabPageScriptHook;
        private TabPage tabPageScriptHookDotNet;
        private TabPage tabPageRage;
        private TabPage tabPageLua;
        private TabPage tabPageLuaLegacy;
        private TabPage tabPageLSPDFR;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.checkBoxRage = new System.Windows.Forms.CheckBox();
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.comboBoxLang = new System.Windows.Forms.ComboBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVersion.Location = new System.Drawing.Point(523, 0);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(100, 13);
            this.labelVersion.TabIndex = 10;
            this.labelVersion.Text = "Version: ";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAuthor
            // 
            this.labelAuthor.Location = new System.Drawing.Point(0, 0);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(100, 13);
            this.labelAuthor.TabIndex = 9;
            this.labelAuthor.Text = "Author: TrenTech";
            this.labelAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Location = new System.Drawing.Point(3, 80);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(620, 487);
            this.tabControl.TabIndex = 4;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // checkBoxRage
            // 
            this.checkBoxRage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxRage.Location = new System.Drawing.Point(6, 45);
            this.checkBoxRage.Name = "checkBoxRage";
            this.checkBoxRage.Size = new System.Drawing.Size(142, 24);
            this.checkBoxRage.TabIndex = 3;
            this.checkBoxRage.Text = "Rage Plugin Hook";
            this.checkBoxRage.UseVisualStyleBackColor = true;
            this.checkBoxRage.CheckedChanged += new System.EventHandler(this.checkBoxRage_CheckedChanged);
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDirectory.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxDirectory.ForeColor = System.Drawing.Color.Red;
            this.textBoxDirectory.Location = new System.Drawing.Point(6, 24);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.ReadOnly = true;
            this.textBoxDirectory.Size = new System.Drawing.Size(323, 20);
            this.textBoxDirectory.TabIndex = 0;
            this.textBoxDirectory.TabStop = false;
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Location = new System.Drawing.Point(416, 23);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMode.TabIndex = 1;
            this.comboBoxMode.SelectionChangeCommitted += new System.EventHandler(this.comboBoxMode_SelectionChangeCommitted);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPlay.Location = new System.Drawing.Point(543, 22);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 2;
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAdd.Location = new System.Drawing.Point(6, 573);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRemove.Location = new System.Drawing.Point(87, 573);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 6;
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // comboBoxLang
            // 
            this.comboBoxLang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLang.FormattingEnabled = true;
            this.comboBoxLang.Items.AddRange(new object[] {
            "en-US",
            "de-DE",
            "es-ES",
            "fr-FR",
            "el-GR",
            "pl-PL",
            "pt-BR",
            "it-IT",
            "ru-RU",
            "he-IL",
            "da-DK",
            "tr-TR"});
            this.comboBoxLang.Location = new System.Drawing.Point(497, 574);
            this.comboBoxLang.Margin = new System.Windows.Forms.Padding(1);
            this.comboBoxLang.Name = "comboBoxLang";
            this.comboBoxLang.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLang.TabIndex = 8;
            this.comboBoxLang.SelectionChangeCommitted += new System.EventHandler(this.comboBoxLang_SelectionChangeCommitted);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOpen.Location = new System.Drawing.Point(186, 573);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 7;
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(335, 22);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 0;
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // MainUI
            // 
            this.ClientSize = new System.Drawing.Size(624, 601);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.comboBoxLang);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.comboBoxMode);
            this.Controls.Add(this.textBoxDirectory);
            this.Controls.Add(this.checkBoxRage);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelVersion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(420, 400);
            this.Name = "MainUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUI_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonBrowse;
    }
}