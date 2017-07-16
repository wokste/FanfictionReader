using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace FanfictionReader {
    public partial class ReaderForm : Form {
        private Reader _reader;

        private IList<Story> _storyList;

        public ReaderForm() {
            InitializeComponent();

            _reader = new Reader();

            _reader.OnPageRender += RenderPage;
            _reader.OnStoryUpdate += StoryUpdated;
            _reader.OnStoryDelete += StoryDeleted;

            _reader.LoadLastStory();

            _storyList = _reader.GetStoryList();
            UpdateShownStories();
        }

        private void StoryUpdated(Story story) {
            if (!_storyList.Contains(story)) {
                _storyList.Add(story);
            }
            UpdateShownStories();
        }
        
        private void StoryDeleted(Story story) {
            _storyList.Remove(story);
            UpdateShownStories();
        }

        private void UpdateShownStories() {
            IList<Story> shownStoryList = _storyList;

            var filters = FilterTextBox.Text.Split(' ').Where(s => s != "");

            foreach (var filter in filters) {
                shownStoryList = shownStoryList.Where(
                    story => (
                        story.MetaData.Title != null &&
                        story.MetaData.Title.IndexOf(filter, 0, StringComparison.CurrentCultureIgnoreCase) != -1
                    )
                ).ToList();
            }
            
            _storyList = _storyList.OrderBy(s => s.MetaData.Title).ToList();

            storyListBox.DataSource = shownStoryList;
        }

        private void RenderPage(HtmlTemplate page) {
            storyReader.DocumentText = page.Html;
            Text = "FanfictionReader - " + page.Title;
        }

        private void StoryClicked(object sender, EventArgs e) {
            var item = storyListBox.SelectedItem;
            if (item != null) {
                _reader.Story = (Story)item;
            }
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

        private void FilterTextBox_TextChanged(object sender, EventArgs e) {
            UpdateShownStories();
        }

        private void refreshMetaToolStripMenuItem_Click(object sender, EventArgs e) {
            _reader.UpdateMeta();
        }

        private void firstChapterToolStripMenuItem_Click(object sender, EventArgs e) {
            _reader.LastReadChapterId = 0;
        }

        private void lastChapterToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_reader.Story.MetaData != null) {
                _reader.LastReadChapterId = _reader.Story.MetaData.ChapterCount - 1;
            }
        }

        private void refreshMetaToolStripMenuItem1_Click(object sender, EventArgs e) {
            _reader.UpdateMetaStory(_reader.Story);
        }
    }
}
