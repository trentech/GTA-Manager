using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GTA_Manager
{
    partial class TabPageControl
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Type type;
        private string directory;
        private List<string> extensions = new List<string>();
        private TableLayoutPanel tableMiddle;
        private Button buttonEnable;
        private Button buttonDisable;
        public ListBox listBoxEnabled;
        public ListBox listBoxDisabled;
        private Label labelDisabled;
        private Label labelEnabled;
        private TableLayoutPanel tableMain;

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
            this.tableMiddle = new System.Windows.Forms.TableLayoutPanel();
            this.buttonEnable = new System.Windows.Forms.Button();
            this.buttonDisable = new System.Windows.Forms.Button();
            this.listBoxEnabled = new System.Windows.Forms.ListBox();
            this.listBoxDisabled = new System.Windows.Forms.ListBox();
            this.labelDisabled = new System.Windows.Forms.Label();
            this.labelEnabled = new System.Windows.Forms.Label();
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableMiddle.SuspendLayout();
            this.tableMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableMiddle
            // 
            this.tableMiddle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tableMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableMiddle.Controls.Add(this.buttonEnable, 0, 2);
            this.tableMiddle.Controls.Add(this.buttonDisable, 0, 1);
            this.tableMiddle.Location = new System.Drawing.Point(265, 20);
            this.tableMiddle.Margin = new System.Windows.Forms.Padding(0);
            this.tableMiddle.Name = "tableMiddle";
            this.tableMiddle.RowCount = 4;
            this.tableMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMiddle.Size = new System.Drawing.Size(53, 540);
            this.tableMiddle.TabIndex = 4;
            // 
            // buttonEnable
            // 
            this.buttonEnable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEnable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnable.Location = new System.Drawing.Point(3, 273);
            this.buttonEnable.Name = "buttonEnable";
            this.buttonEnable.Size = new System.Drawing.Size(47, 23);
            this.buttonEnable.TabIndex = 3;
            this.buttonEnable.Text = "<<";
            this.buttonEnable.UseVisualStyleBackColor = true;
            this.buttonEnable.Click += new System.EventHandler(this.buttonEnable_Click);
            // 
            // buttonDisable
            // 
            this.buttonDisable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDisable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDisable.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDisable.Location = new System.Drawing.Point(3, 243);
            this.buttonDisable.Name = "buttonDisable";
            this.buttonDisable.Size = new System.Drawing.Size(47, 23);
            this.buttonDisable.TabIndex = 2;
            this.buttonDisable.Text = ">>";
            this.buttonDisable.UseVisualStyleBackColor = true;
            this.buttonDisable.Click += new System.EventHandler(this.buttonDisable_Click);
            // 
            // listBoxEnabled
            // 
            this.listBoxEnabled.AllowDrop = true;
            this.listBoxEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxEnabled.FormattingEnabled = true;
            this.listBoxEnabled.Location = new System.Drawing.Point(0, 20);
            this.listBoxEnabled.Margin = new System.Windows.Forms.Padding(0);
            this.listBoxEnabled.Name = "listBoxEnabled";
            this.listBoxEnabled.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxEnabled.Size = new System.Drawing.Size(263, 537);
            this.listBoxEnabled.TabIndex = 0;
            this.listBoxEnabled.SelectedIndexChanged += new System.EventHandler(this.listBoxEnabled_SelectedIndexChanged);
            this.listBoxEnabled.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxEnabled_DragDrop);
            this.listBoxEnabled.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxEnabled_DragEnter);
            this.listBoxEnabled.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxEnabled_MouseDoubleClick);
            // 
            // listBoxDisabled
            // 
            this.listBoxDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxDisabled.FormattingEnabled = true;
            this.listBoxDisabled.Location = new System.Drawing.Point(321, 20);
            this.listBoxDisabled.Margin = new System.Windows.Forms.Padding(0);
            this.listBoxDisabled.Name = "listBoxDisabled";
            this.listBoxDisabled.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxDisabled.Size = new System.Drawing.Size(263, 537);
            this.listBoxDisabled.TabIndex = 1;
            this.listBoxDisabled.SelectedIndexChanged += new System.EventHandler(this.listBoxDisabled_SelectedIndexChanged);
            this.listBoxDisabled.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxDisabled_MouseDoubleClick);
            // 
            // labelDisabled
            // 
            this.labelDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDisabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDisabled.Location = new System.Drawing.Point(324, 0);
            this.labelDisabled.Name = "labelDisabled";
            this.labelDisabled.Size = new System.Drawing.Size(257, 20);
            this.labelDisabled.TabIndex = 0;
            this.labelDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelEnabled
            // 
            this.labelEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEnabled.Location = new System.Drawing.Point(3, 0);
            this.labelEnabled.Name = "labelEnabled";
            this.labelEnabled.Size = new System.Drawing.Size(257, 20);
            this.labelEnabled.TabIndex = 0;
            this.labelEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableMain
            // 
            this.tableMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableMain.AutoSize = true;
            this.tableMain.ColumnCount = 3;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMain.Controls.Add(this.labelEnabled, 0, 0);
            this.tableMain.Controls.Add(this.labelDisabled, 2, 0);
            this.tableMain.Controls.Add(this.listBoxDisabled, 2, 1);
            this.tableMain.Controls.Add(this.listBoxEnabled, 0, 1);
            this.tableMain.Controls.Add(this.tableMiddle, 1, 1);
            this.tableMain.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableMain.Location = new System.Drawing.Point(0, 1);
            this.tableMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableMain.Size = new System.Drawing.Size(584, 560);
            this.tableMain.TabIndex = 0;
            // 
            // TabPageControl
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableMain);
            this.MinimumSize = new System.Drawing.Size(5, 5);
            this.Name = "TabPageControl";
            this.Size = new System.Drawing.Size(584, 561);
            this.tableMiddle.ResumeLayout(false);
            this.tableMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}