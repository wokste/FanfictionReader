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
            this.SuspendLayout();
            // 
            // storyReader
            // 
            this.storyReader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storyReader.Location = new System.Drawing.Point(205, 0);
            this.storyReader.MinimumSize = new System.Drawing.Size(20, 20);
            this.storyReader.Name = "storyReader";
            this.storyReader.Size = new System.Drawing.Size(803, 490);
            this.storyReader.TabIndex = 0;
            // 
            // storyListBox
            // 
            this.storyListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.storyListBox.FormattingEnabled = true;
            this.storyListBox.Location = new System.Drawing.Point(0, 0);
            this.storyListBox.Name = "storyListBox";
            this.storyListBox.Size = new System.Drawing.Size(205, 490);
            this.storyListBox.TabIndex = 1;
            this.storyListBox.SelectedIndexChanged += new System.EventHandler(this.storyClicked);
            // 
            // ReaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 490);
            this.Controls.Add(this.storyReader);
            this.Controls.Add(this.storyListBox);
            this.Name = "ReaderForm";
            this.Text = "FanfictionReader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser storyReader;
        private System.Windows.Forms.ListBox storyListBox;
    }
}

