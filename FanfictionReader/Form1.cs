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
            listStories();
            openStory(null);
        }

        private void listStories() {
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

            } else {
                storyReader.Navigate(story.getUrl());
                this.Text = "FanfictionReader - " + story.ToString();
            }
        }

        private void storyClicked(object sender, EventArgs e) {
            var story = storyListBox.SelectedItem as Story;
            openStory(story);
        }

        private void addStoryMenuClick(object sender, EventArgs e) {
            var frm = new NewStoryForm(storyController, null);
            frm.Show();
        }

        private void previousChapterMenuClick(object sender, EventArgs e) {
            if (shownStory.ChapterID <= 1)
                return;

            shownStory.ChapterID--;
            openStory(shownStory);
            storyController.SaveStory(shownStory);
        }

        private void nextChapterMenuClick(object sender, EventArgs e) {
            shownStory.ChapterID++;
            openStory(shownStory);
            storyController.SaveStory(shownStory);
        }

        private void refreshLibraryMenuClick(object sender, EventArgs e) {
            listStories();
        }

        private void refreshStoryMenuClick(object sender, EventArgs e) {
            openStory(shownStory);
        }
    }
}
