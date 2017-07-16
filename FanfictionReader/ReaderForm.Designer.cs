namespace FanfictionReader {
    partial class ReaderForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.storyReader = new System.Windows.Forms.WebBrowser();
            this.storyListBox = new System.Windows.Forms.ListBox();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.libraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousChapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextChapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SortTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.refreshMetaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.firstChapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastChapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // storyReader
            // 
            this.storyReader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storyReader.Location = new System.Drawing.Point(0, 0);
            this.storyReader.MinimumSize = new System.Drawing.Size(20, 20);
            this.storyReader.Name = "storyReader";
            this.storyReader.Size = new System.Drawing.Size(809, 466);
            this.storyReader.TabIndex = 0;
            // 
            // storyListBox
            // 
            this.storyListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storyListBox.FormattingEnabled = true;
            this.storyListBox.Location = new System.Drawing.Point(0, 56);
            this.storyListBox.Name = "storyListBox";
            this.storyListBox.Size = new System.Drawing.Size(195, 410);
            this.storyListBox.TabIndex = 1;
            this.storyListBox.SelectedIndexChanged += new System.EventHandler(this.StoryClicked);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.libraryToolStripMenuItem,
            this.storyToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1008, 24);
            this.MainMenu.TabIndex = 2;
            this.MainMenu.Text = "MainMenu";
            // 
            // libraryToolStripMenuItem
            // 
            this.libraryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStoryToolStripMenuItem,
            this.refreshMetaToolStripMenuItem});
            this.libraryToolStripMenuItem.Name = "libraryToolStripMenuItem";
            this.libraryToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.libraryToolStripMenuItem.Text = "Library";
            // 
            // addStoryToolStripMenuItem
            // 
            this.addStoryToolStripMenuItem.Name = "addStoryToolStripMenuItem";
            this.addStoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addStoryToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.addStoryToolStripMenuItem.Text = "New Story";
            this.addStoryToolStripMenuItem.Click += new System.EventHandler(this.AddStoryMenuClick);
            // 
            // refreshMetaToolStripMenuItem
            // 
            this.refreshMetaToolStripMenuItem.Name = "refreshMetaToolStripMenuItem";
            this.refreshMetaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F5)));
            this.refreshMetaToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.refreshMetaToolStripMenuItem.Text = "Refresh Meta";
            this.refreshMetaToolStripMenuItem.Click += new System.EventHandler(this.refreshMetaToolStripMenuItem_Click);
            // 
            // storyToolStripMenuItem
            // 
            this.storyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstChapterToolStripMenuItem,
            this.previousChapterToolStripMenuItem,
            this.nextChapterToolStripMenuItem,
            this.lastChapterToolStripMenuItem,
            this.toolStripSeparator1,
            this.refreshMetaToolStripMenuItem1});
            this.storyToolStripMenuItem.Name = "storyToolStripMenuItem";
            this.storyToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.storyToolStripMenuItem.Text = "Story";
            // 
            // previousChapterToolStripMenuItem
            // 
            this.previousChapterToolStripMenuItem.Name = "previousChapterToolStripMenuItem";
            this.previousChapterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.previousChapterToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.previousChapterToolStripMenuItem.Text = "Previous Chapter";
            this.previousChapterToolStripMenuItem.Click += new System.EventHandler(this.PreviousChapterMenuClick);
            // 
            // nextChapterToolStripMenuItem
            // 
            this.nextChapterToolStripMenuItem.Name = "nextChapterToolStripMenuItem";
            this.nextChapterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.nextChapterToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.nextChapterToolStripMenuItem.Text = "Next Chapter";
            this.nextChapterToolStripMenuItem.Click += new System.EventHandler(this.NextChapterMenuClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.storyListBox);
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.storyReader);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 466);
            this.splitContainer1.SplitterDistance = 195;
            this.splitContainer1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.FilterTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SortTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(195, 56);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterTextBox.Location = new System.Drawing.Point(53, 3);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(139, 20);
            this.FilterTextBox.TabIndex = 0;
            this.FilterTextBox.TextChanged += new System.EventHandler(this.FilterTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sort";
            // 
            // SortTextBox
            // 
            this.SortTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SortTextBox.Location = new System.Drawing.Point(53, 31);
            this.SortTextBox.Name = "SortTextBox";
            this.SortTextBox.Size = new System.Drawing.Size(139, 20);
            this.SortTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter";
            // 
            // refreshMetaToolStripMenuItem1
            // 
            this.refreshMetaToolStripMenuItem1.Name = "refreshMetaToolStripMenuItem1";
            this.refreshMetaToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshMetaToolStripMenuItem1.Size = new System.Drawing.Size(234, 22);
            this.refreshMetaToolStripMenuItem1.Text = "Refresh Meta";
            this.refreshMetaToolStripMenuItem1.Click += new System.EventHandler(this.refreshMetaToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // firstChapterToolStripMenuItem
            // 
            this.firstChapterToolStripMenuItem.Name = "firstChapterToolStripMenuItem";
            this.firstChapterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Left)));
            this.firstChapterToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.firstChapterToolStripMenuItem.Text = "First Chapter";
            this.firstChapterToolStripMenuItem.Click += new System.EventHandler(this.firstChapterToolStripMenuItem_Click);
            // 
            // lastChapterToolStripMenuItem
            // 
            this.lastChapterToolStripMenuItem.Name = "lastChapterToolStripMenuItem";
            this.lastChapterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Right)));
            this.lastChapterToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.lastChapterToolStripMenuItem.Text = "Last Chapter";
            this.lastChapterToolStripMenuItem.Click += new System.EventHandler(this.lastChapterToolStripMenuItem_Click);
            // 
            // ReaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 490);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "ReaderForm";
            this.Text = "FanfictionReader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser storyReader;
        private System.Windows.Forms.ListBox storyListBox;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem storyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousChapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextChapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStoryToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FilterTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SortTextBox;
        private System.Windows.Forms.ToolStripMenuItem refreshMetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firstChapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastChapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem refreshMetaToolStripMenuItem1;
    }
}

