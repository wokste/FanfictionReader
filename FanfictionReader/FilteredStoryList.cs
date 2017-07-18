using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace FanfictionReader {
    public class FilteredStoryList : IListSource {
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

        public FilteredStoryList(IList<Story> storyList) {
            _storyList = storyList;
        }

        public bool ContainsListCollection => (_storyList != null);

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
            var shownStoryList = _storyList;

            var filters = Filter.Split(' ').Where(s => s != "");

            foreach (var filter in filters) {
                shownStoryList = shownStoryList.Where(
                    story => (
                        story.MetaData.Title != null &&
                        story.MetaData.Title.IndexOf(filter, 0, StringComparison.CurrentCultureIgnoreCase) != -1
                    )
                ).ToList();
            }

            return SortList(shownStoryList).Cast<object>().ToList();
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
