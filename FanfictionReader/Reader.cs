using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Net;

namespace FanfictionReader {
    class Reader {
        private readonly StoryController _storyController;
        private readonly ChapterCache _chapterCache;

        private Story _story;
        
        internal Action<Story> OnStoryUpdate;
        internal Action<Story> OnStoryDelete;

        internal Action<HtmlTemplate> OnPageRender;

        internal Reader() {
            var conn = new SQLiteConnection("URI=file:D:/AppData/Local/FanfictionReader/Fanfictions.sqlite");
            conn.Open();
            _storyController = new StoryController(conn);
            _chapterCache = new ChapterCache(conn);
        }

        public void SelectStory(Story story) {
            _story = story;
            RefreshPage();
        }

        internal void SaveStory(Story story) {
            _storyController.SaveStory(story);
            OnStoryUpdate?.Invoke(story);
        }

        internal void LoadLastStory() {
            var lastReadStory = Properties.Settings.Default.LastReadFic;
            _story = _storyController.GetStory(lastReadStory);
            RefreshPage();
        }

        private void RefreshPage() {
            var page = new HtmlTemplate();
            page.Chapter = GetChapter(_story, _story.LastReadChapterId + 1);

            OnPageRender?.Invoke(page);
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

        internal IList<Story> GetStoryList() {
            return _storyController.GetStoryList();
        }

        internal void PreviousChapter() {
            if (_story.LastReadChapterId <= 0)
                return;

            _story.LastReadChapterId--;
            _storyController.SaveStory(_story);
            RefreshPage();
            
            OnStoryUpdate?.Invoke(_story);
        }

        internal void NextChapter() {
            _story.LastReadChapterId++;
            _storyController.SaveStory(_story);
            RefreshPage();
            
            OnStoryUpdate?.Invoke(_story);
        }
    }
}
