using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Net;
using System.Threading.Tasks;

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
                var task = RefreshPageAsync();
            }
        }

        internal int LastReadChapterId {
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
                var task = RefreshPageAsync();
            }
        }

        internal Reader() {
            var conn = new SQLiteConnection("URI=file:D:/AppData/Local/FanfictionReader/Fanfictions.sqlite");
            conn.Open();
            _storyController = new StoryController(conn);
            _chapterCache = new ChapterCache(conn);
        }

        internal Task InsertStoryAsync(Story story) {
            OnStoryUpdate?.Invoke(story);
            return Task.Run(() => _storyController.InsertStory(story));
        }

        internal void LoadLastStory() {
            var lastReadStory = Properties.Settings.Default.LastReadFic;
            Story = _storyController.GetStory(lastReadStory);
            var task = RefreshPageAsync();
        }
        
        internal IList<Story> GetStoryList() {
            return _storyController.GetStoryList();
        }

        private async Task RefreshPageAsync() {
            var page = new HtmlTemplate();
            page.Chapter = await GetChapterAsync(_story, _story.LastReadChapterId + 1);
            OnPageRender?.Invoke(page);
        }

        private Task<Chapter> GetChapterAsync(Story story, int chapterId) {
            return Task.Run(() => GetChapter(story, chapterId));
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

        private Task UpdateMetaAsync() {
            return Task.Run(() => UpdateMeta());
        }

        public void UpdateMeta() {
            foreach (var story in _storyController.GetStoryList()) {
                var storyParser = new FictionpressStoryParser();
                
                try {
                    var meta = storyParser.GetMeta(story);

                    if (meta != null) {
                        story.MetaData = meta;
                        _storyController.UpdateStoryMeta(story);
                    }

                    
                } catch (WebException ex) {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}
