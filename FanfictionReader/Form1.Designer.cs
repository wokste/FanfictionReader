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
            this.storyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousChapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextChapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // storyReader
            // 
            this.storyReader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storyReader.Location = new System.Drawing.Point(205, 24);
            this.storyReader.MinimumSize = new System.Drawing.Size(20, 20);
            this.storyReader.Name = "storyReader";
            this.storyReader.Size = new System.Drawing.Size(803, 466);
            this.storyReader.TabIndex = 0;
            // 
            // storyListBox
            // 
            this.storyListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.storyListBox.FormattingEnabled = true;
            this.storyListBox.Location = new System.Drawing.Point(0, 24);
            this.storyListBox.Name = "storyListBox";
            this.storyListBox.Size = new System.Drawing.Size(205, 466);
            this.storyListBox.TabIndex = 1;
            this.storyListBox.SelectedIndexChanged += new System.EventHandler(this.storyClicked);
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
            this.refreshToolStripMenuItem});
            this.libraryToolStripMenuItem.Name = "libraryToolStripMenuItem";
            this.libraryToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.libraryToolStripMenuItem.Text = "Library";
            // 
            // addStoryToolStripMenuItem
            // 
            this.addStoryToolStripMenuItem.Name = "addStoryToolStripMenuItem";
            this.addStoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addStoryToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addStoryToolStripMenuItem.Text = "New Story";
            this.addStoryToolStripMenuItem.Click += new System.EventHandler(this.addStoryMenuClick);
            // 
            // storyToolStripMenuItem
            // 
            this.storyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previousChapterToolStripMenuItem,
            this.nextChapterToolStripMenuItem,
            this.refreshToolStripMenuItem1});
            this.storyToolStripMenuItem.Name = "storyToolStripMenuItem";
            this.storyToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.storyToolStripMenuItem.Text = "Story";
            // 
            // previousChapterToolStripMenuItem
            // 
            this.previousChapterToolStripMenuItem.Name = "previousChapterToolStripMenuItem";
            this.previousChapterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.previousChapterToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.previousChapterToolStripMenuItem.Text = "Previous Chapter";
            this.previousChapterToolStripMenuItem.Click += new System.EventHandler(this.previousChapterMenuClick);
            // 
            // nextChapterToolStripMenuItem
            // 
            this.nextChapterToolStripMenuItem.Name = "nextChapterToolStripMenuItem";
            this.nextChapterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.nextChapterToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.nextChapterToolStripMenuItem.Text = "Next Chapter";
            this.nextChapterToolStripMenuItem.Click += new System.EventHandler(this.nextChapterMenuClick);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshLibraryMenuClick);
            // 
            // refreshToolStripMenuItem1
            // 
            this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            this.refreshToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem1.Size = new System.Drawing.Size(218, 22);
            this.refreshToolStripMenuItem1.Text = "Refresh";
            this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshStoryMenuClick);
            // 
            // ReaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 490);
            this.Controls.Add(this.storyReader);
            this.Controls.Add(this.storyListBox);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "ReaderForm";
            this.Text = "FanfictionReader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
    }
}

