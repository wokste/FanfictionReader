using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace FanfictionReader {
    public partial class ReaderForm : Form {
        StoryController storyController;
        SQLiteConnection conn;

        Story shownStory = null;

        public ReaderForm() {
            InitializeComponent();

            conn = new SQLiteConnection("URI=file:D:/AppData/Local/FanfictionReader/Fanfictions.sqlite");
            conn.Open();
            storyController = new StoryController(conn);

            var lastReadStory = Properties.Settings.Default.LastReadFic;
            openStory(storyController.GetStory(lastReadStory));
        }

        private void refreshStoryList() {
            var storyList = storyController.GetStoryList();

            storyListBox.Items.Clear();

            foreach (var story in storyList) {
                storyListBox.Items.Add(story);
            }
        }

        private void openStory(Story story) {
            this.shownStory = story;

            if (story == null) {
                storyReader.Navigate("about:blank");
                this.Text = "FanfictionReader";
                Properties.Settings.Default.LastReadFic = 0;

            } else {
                var storyParser = new FFStoryParser();
                var page = new HTMLTemplate();
                page.Body = storyParser.getStoryText(story.Id, story.ChapterID);
                storyReader.DocumentText = page.MakeHTML();
                this.Text = "FanfictionReader - " + story.ToString();
                Properties.Settings.Default.LastReadFic = story.PK;
            }
            Properties.Settings.Default.Save();

            refreshStoryList();
        }

        private void storyClicked(object sender, EventArgs e) {
            var story = storyListBox.SelectedItem as Story;
            openStory(story);
        }

        private void addStoryMenuClick(object sender, EventArgs e) {
            var frm = new NewStoryForm(storyController);
            frm.Show();
        }

        private void previousChapterMenuClick(object sender, EventArgs e) {
            if (shownStory.ChapterID <= 1)
                return;

            shownStory.ChapterID--;
            storyController.SaveStory(shownStory);
            openStory(shownStory);
        }

        private void nextChapterMenuClick(object sender, EventArgs e) {
            shownStory.ChapterID++;
            storyController.SaveStory(shownStory);
            openStory(shownStory);
        }

        private void refreshLibraryMenuClick(object sender, EventArgs e) {
            refreshStoryList();
        }

        private void refreshStoryMenuClick(object sender, EventArgs e) {
            openStory(shownStory);
        }

        private void urlBoxKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                //TODO: Add story
            }
        }
    }
}
