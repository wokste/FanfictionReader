using System;
using System.Text.RegularExpressions;

namespace FanfictionReader {
    class StoryParser {
        StoryController StoryController;

        internal StoryParser(StoryController StoryController) {
            this.StoryController = StoryController;
        }

        internal void AddUrl(string url) {

            // Remove "http://", "https://", "www." etc from url
            url = Regex.Replace(url, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", string.Empty, RegexOptions.IgnoreCase);

            var words = url.Split('/');

            Story story = new Story();
            story.Host = words[0];
            story.Id = int.Parse(words[2]);
            story.ChapterID = int.Parse(words[3]);
            story.Title = words[4];

            StoryController.AddStory(story);
        }
    }
}
