using System;
using System.Windows.Forms;

namespace FanfictionReader {
    public partial class ReaderForm : Form {
        private Reader _reader;

        public ReaderForm() {
            InitializeComponent();

            _reader = new Reader();

            _reader.OnPageRender += RenderPage;
            _reader.OnStoryUpdate += StoryUpdated;
            _reader.OnStoryDelete += StoryDeleted;

            _reader.LoadLastStory();
            InitStoryList();
        }

        private void StoryUpdated(Story story) {
            if (storyListBox.Items.Contains(story)) {
                storyListBox.Refresh();
            }
            else {
                storyListBox.Items.Add(story);
            }
        }
        
        private void StoryDeleted(Story story) {
            storyListBox.Items.Remove(story);
        }

        private void InitStoryList() {
            //TODO: This function should be called in the constructor only.
            var storyList = _reader.GetStoryList();

            storyListBox.Items.Clear();

            foreach (var story in storyList) {
                storyListBox.Items.Add(story);
            }
        }

        private void RenderPage(HtmlTemplate page) {
            storyReader.DocumentText = page.Html;
            Text = "FanfictionReader - " + page.Title;
        }

        private void StoryClicked(object sender, EventArgs e) {
            var story = storyListBox.SelectedItem as Story;
            _reader.Story = story;
        }

        private void AddStoryMenuClick(object sender, EventArgs e) {
            var frm = new NewStoryForm(_reader);
            frm.Show();
        }

        private void PreviousChapterMenuClick(object sender, EventArgs e) {
            _reader.LastReadChapterId--;
        }

        private void NextChapterMenuClick(object sender, EventArgs e) {
            _reader.LastReadChapterId++;
        }
    }
}
