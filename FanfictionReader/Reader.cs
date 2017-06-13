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

        internal Story Story {
            get { return _story; }
            set {
                _story = value;
                RefreshPage();
            }
        }

        internal int LastReadChapterId {
            get { return _story.LastReadChapterId; }
            set {
                if (value < 0)
                    value = 0;

                if (value > _story.ChapterCount)
                    value = _story.ChapterCount;

                if (_story.LastReadChapterId == value)
                    return;

                _story.LastReadChapterId = value;
                _storyController.SaveStory(_story);
                RefreshPage();
                OnStoryUpdate?.Invoke(_story);
            }
        }

        internal Reader() {
            var conn = new SQLiteConnection("URI=file:D:/AppData/Local/FanfictionReader/Fanfictions.sqlite");
            conn.Open();
            _storyController = new StoryController(conn);
            _chapterCache = new ChapterCache(conn);
        }
        

        internal void SaveStory(Story story) {
            _storyController.SaveStory(story);
            OnStoryUpdate?.Invoke(story);
        }

        internal void LoadLastStory() {
            var lastReadStory = Properties.Settings.Default.LastReadFic;
            Story = _storyController.GetStory(lastReadStory);
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
    }
}
