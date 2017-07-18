using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace FanfictionReader {
    public class StoryListView {
        private string _filter = "";
        private StorySorting _sorting = StorySorting.Title;

        public string Filter {
            get {
                return _filter;
            }
            set {
                _filter = value;
                OnSourceChange?.Invoke();
            }
        }

        public StorySorting Sorting {
            get {
                return _sorting;
            }
            set {
                _sorting = value;
                OnSourceChange?.Invoke();
            }
        }

        private readonly IList<Story> _storyList;

        public Action OnSourceChange;

        public StoryListView(IList<Story> storyList) {
            _storyList = storyList;
        }

        public void StoryUpdated(Story story) {
            if (!_storyList.Contains(story)) {
                _storyList.Add(story);
            }

            // TODO: Validate whether this is nessesary
            OnSourceChange?.Invoke();
        }

        public void StoryDeleted(Story story) {
            _storyList.Remove(story);

            // TODO: Validate whether this is nessesary
            OnSourceChange?.Invoke();
        }

        public IList GetList() {
            var list = _storyList;

            var filters = Filter.Split(' ').Where(s => s != "");

            foreach (var filter in filters) {
                var func = CreateFilter(filter);

                list = list.Where(func).ToList();
            }

            return SortList(list).Cast<object>().ToList();
        }

        private Func<Story, bool> CreateFilter(string filter) {
            var filterSplit = filter.Split(':');

            // Default when there is no ':' sign
            if (filterSplit.Length == 1) {
                return CreateFilter("title", filterSplit[0]);
            }

            // Negation with a not sign '!'
            if (filterSplit[0].StartsWith("!")) {
                var f = CreateFilter(filterSplit[0].Remove(0,1), filterSplit[1]);
                return (s => !f(s));
            }

            return CreateFilter(filterSplit[0], filterSplit[1]);
        }

        private Func<Story, bool> CreateFilter(string filterKey, string filterValue) {
            var intValue = 0;
            int.TryParse(filterValue, out intValue);

            switch (filterKey) {
                case "t":
                case "title":
                    return (story =>
                        story.MetaData != null &&
                        story.MetaData.Title.IndexOf(filterValue, 0, StringComparison.CurrentCultureIgnoreCase) != -1
                    );
                case "w":
                case "word":
                case "words":
                    return (story =>
                        story.MetaData != null &&
                        story.MetaData.Words >= intValue
                    );
                case "ch":
                case "chapters":
                    return (story =>
                        story.MetaData != null &&
                        story.MetaData.ChapterCount == intValue
                    );
                case "read":
                    if (filterValue == "") {
                        intValue = 100;
                    }

                    return (story =>
                        story.MetaData != null &&
                        story.LastReadChapterId * 100.0 / story.MetaData.ChapterCount  >= intValue
                    );
                case "started":
                    return (story =>
                        story.LastReadChapterId > 0
                    );
            }
            return (story => true);
        }

        public IList<Story> SortList(IList<Story> source) {
            switch(Sorting) 
            {
                case StorySorting.Title:
                    return source.OrderBy(s => s.MetaData.Title).ToList();
                case StorySorting.ChapterCount:
                    return source.OrderBy(s => s.MetaData.ChapterCount).ToList();
                case StorySorting.ChapterRead:
                    return source.OrderBy(s => s.LastReadChapterId).ToList();
                case StorySorting.PecentageRead:
                    return source.OrderBy(s => (float)s.LastReadChapterId / (float)s.MetaData.ChapterCount).ToList();
                case StorySorting.AddDate:
                    return source.OrderBy(s => s.AddDate).ToList();
                case StorySorting.PublishDate:
                    return source.OrderBy(s => s.MetaData.PublishDate).ToList();
                case StorySorting.UpdateDate:
                    return source.OrderBy(s => s.MetaData.UpdateDate).ToList();
                case StorySorting.LastReadDate:
                    return source.OrderBy(s => s.LastReadDate).ToList();
                case StorySorting.Words:
                    return source.OrderBy(s => s.MetaData.Words).ToList();
                default:
                    throw new Exception();
            }
        }

        public enum StorySorting {
            Title, ChapterCount, ChapterRead, PecentageRead, AddDate, UpdateDate, PublishDate, LastReadDate, Words
        }
    }
}
