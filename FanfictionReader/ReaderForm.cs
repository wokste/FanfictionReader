using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace FanfictionReader {
    public partial class ReaderForm : Form {
        private readonly StoryController _storyController;

        private Story _story;

        public ReaderForm() {
            InitializeComponent();

            var conn = new SQLiteConnection("URI=file:D:/AppData/Local/FanfictionReader/Fanfictions.sqlite");
            conn.Open();
            _storyController = new StoryController(conn);

            var lastReadStory = Properties.Settings.Default.LastReadFic;
            OpenStory(_storyController.GetStory(lastReadStory));
        }

        private void RefreshStoryList() {
            var storyList = _storyController.GetStoryList();

            storyListBox.Items.Clear();

            foreach (var story in storyList) {
                storyListBox.Items.Add(story);
            }
        }

        private void OpenStory(Story story) {
            _story = story;

            if (story == null) {
                storyReader.Navigate("about:blank");
                Text = "FanfictionReader";
                Properties.Settings.Default.LastReadFic = 0;

            } else {
                story.LastReadDate = DateTime.Now;

                var storyParser = new FictionpressStoryParser();
                storyParser.UpdateMeta(story);

                var page = new HtmlTemplate
                {
                    Body = storyParser.GetStoryText(story.Id, story.LastReadChapterId)
                };
                storyReader.DocumentText = page.MakeHtml();
                Text = "FanfictionReader - " + story;
                Properties.Settings.Default.LastReadFic = story.SqlPk;
            }
            Properties.Settings.Default.Save();

            RefreshStoryList();
        }

        private void StoryClicked(object sender, EventArgs e) {
            var story = storyListBox.SelectedItem as Story;
            OpenStory(story);
        }

        private void AddStoryMenuClick(object sender, EventArgs e) {
            var frm = new NewStoryForm(_storyController);
            frm.Show();
        }

        private void PreviousChapterMenuClick(object sender, EventArgs e) {
            if (_story.LastReadChapterId <= 1)
                return;

            _story.LastReadChapterId--;
            _storyController.SaveStory(_story);
            OpenStory(_story);
        }

        private void NextChapterMenuClick(object sender, EventArgs e) {
            _story.LastReadChapterId++;
            _storyController.SaveStory(_story);
            OpenStory(_story);
        }

        private void RefreshLibraryMenuClick(object sender, EventArgs e) {
            RefreshStoryList();
        }

        private void RefreshStoryMenuClick(object sender, EventArgs e) {
            OpenStory(_story);
        }
    }
}
