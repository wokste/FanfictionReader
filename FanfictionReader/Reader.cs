using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Net;

namespace FanfictionReader {
    public class Reader {
        private readonly StoryController _storyController;
        private readonly ChapterCache _chapterCache;

        private Story _story;
        
        public Action<Story> OnStoryUpdate;
        public Action<Story> OnStoryDelete;
        public Action<HtmlTemplate> OnPageRender;

        public Story Story {
            get { return _story; }
            set {
                _story = value;
                RefreshPage();
            }
        }

        public int LastReadChapterId {
            get { return _story.LastReadChapterId; }
            set {
                if (value < 0)
                    value = 0;

                if (value > _story.MetaData.ChapterCount)
                    value = _story.MetaData.ChapterCount;

                if (_story.LastReadChapterId == value)
                    return;

                _story.LastReadChapterId = value;
                _storyController.UpdateStoryUserData(_story);
                OnStoryUpdate?.Invoke(_story);
                RefreshPage();
            }
        }

        public Reader() {
            var conn = new SQLiteConnection("URI=file:D:/AppData/Local/FanfictionReader/Fanfictions.sqlite");
            conn.Open();
            _storyController = new StoryController(conn);
            _chapterCache = new ChapterCache(conn);
        }

        public void InsertStory(Story story) {
            OnStoryUpdate?.Invoke(story);
            _storyController.InsertStory(story);
        }

        public void LoadLastStory() {
            var lastReadStory = Properties.Settings.Default.LastReadFic;
            Story = _storyController.GetStory(lastReadStory);
            RefreshPage();
        }
        
        public FilteredStoryList GetStoryList() {
            var filter = new FilteredStoryList(_storyController.GetStoryList());
            
            OnStoryUpdate += filter.StoryUpdated;
            OnStoryDelete += filter.StoryDeleted;

            return filter;
        }

        private  void RefreshPage() {
            var page = new HtmlTemplate();
            page.Chapter =  GetChapter(_story, _story.LastReadChapterId + 1);
            OnPageRender?.Invoke(page);
        }
        
        private Chapter GetChapter(Story story, int chapterId) {
            var storyParser = new FictionpressStoryParser();

            var chapter = _chapterCache.GetChapterIfExists(story, chapterId);
            if (chapter != null)
                return chapter;
            try {
                chapter = storyParser.GetChapter(story, chapterId);
                _chapterCache.SaveChapter(chapter);
                return chapter;
            } catch (WebException ex) {
                chapter = new Chapter {
                    ChapterId = 0,
                    Story = null,
                    HtmlText = $"<p>{ex.Message}</p>",
                    Title = "Error"
                };
                return chapter;
            }
        }

        public void UpdateMeta() {
            foreach (var story in _storyController.GetStoryList()) {
                UpdateMetaStory(story);
            }
        }

        public void UpdateMetaSmart() {
            foreach (var story in _storyController.GetStoryList()) {
                
                UpdateMetaStory(story);
            }
        }

        public void UpdateMetaStory(Story story) {
            var storyParser = new FictionpressStoryParser();

            try {
                var meta = storyParser.GetMeta(story);

                if (meta != null) {
                    story.MetaData = meta;
                    _storyController.UpdateStoryMeta(story);
                }
            } catch (WebException ex) {
                Console.WriteLine(ex.Message);
            } catch (ParseException ex) {
                Console.WriteLine(ex.Message);
            }

            OnStoryUpdate?.Invoke(story);
        }
    }
}
