using System;
using System.Data.SQLite;
using System.Net;
using System.Windows.Forms;

namespace FanfictionReader {
    public partial class ReaderForm : Form {
        private readonly StoryController _storyController;
        private readonly ChapterCache _chapterCache;

        private Story _story;

        public ReaderForm() {
            InitializeComponent();

            var conn = new SQLiteConnection("URI=file:D:/AppData/Local/FanfictionReader/Fanfictions.sqlite");
            conn.Open();
            _storyController = new StoryController(conn);
            _chapterCache = new ChapterCache(conn);

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
                return;
            }

            story.LastReadDate = DateTime.Now;
            
            var page = new HtmlTemplate();
            page.Chapter = GetChapter(story, story.LastReadChapterId + 1);
            storyReader.DocumentText = page.Html;

            Text = "FanfictionReader - " + page.Title;
            Properties.Settings.Default.LastReadFic = story.Pk;

            Properties.Settings.Default.Save();

            RefreshStoryList();
        }

        private Chapter GetChapter(Story story, int chapterId) {
            var storyParser = new FictionpressStoryParser();
            _storyController.SaveStory(story);

            var chapter = _chapterCache.GetChapterIfExists(story, chapterId);
            if (chapter != null)
                return chapter;
            try {
                chapter = storyParser.GetChapter(story, chapterId);
                storyParser.UpdateMeta(story);
                _chapterCache.SaveChapter(chapter);
                return chapter;
            } catch (WebException ex) {
                chapter = new Chapter();
                chapter.ChapterId = 0;
                chapter.Story = null;
                chapter.HtmlText = $"<p>{ex.Message}</p>";
                chapter.Title = "Error";
                return chapter;
            }
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
