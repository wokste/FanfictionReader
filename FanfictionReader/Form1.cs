using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace FanfictionReader {
    public partial class ReaderForm : Form {
        StoryController storyController;
        SQLiteConnection conn;

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


    }
}
