﻿using System;
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
                _storyList.OrderBy(s => s.LastReadChapterId);
            }
            storyListBox.Refresh();
        }
        
        private void StoryDeleted(Story story) {
            _storyList.Remove(story);
            storyListBox.Refresh();
        }

        private void UpdateShownStories() {
            IList<Story> shownStoryList = _storyList;

            var filters = FilterTextBox.Text.Split(' ').Where(s => s != "");

            foreach (var filter in filters) {
                shownStoryList = shownStoryList.Where(
                    story => (
                        story.Title.IndexOf(filter, 0, StringComparison.CurrentCultureIgnoreCase) != -1
                    )
                ).ToList();
            }

            storyListBox.DataSource = shownStoryList;
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

        private void FilterTextBox_TextChanged(object sender, EventArgs e) {
            UpdateShownStories();
        }
    }
}
