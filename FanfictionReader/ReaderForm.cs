using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;

namespace FanfictionReader {
    public partial class ReaderForm : Form {
        private Reader _reader;

        private StoryListView _storyListView;

        public ReaderForm() {
            InitializeComponent();

            _reader = new Reader();

            _reader.OnPageRender += RenderPage;

            _reader.LoadLastStory();

            _storyListView = _reader.GetStoryList();

            _storyListView.OnSourceChange += StoryListBoxRefresh;
            StoryListBoxRefresh();
        }

        private void StoryListBoxRefresh() {
            storyListBox.DataSource = _storyListView.GetList();
            storyListBox.Refresh();
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
            _storyListView.Filter = FilterTextBox.Text;

            // TODO: this should be removed
            storyListBox.Refresh();
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
