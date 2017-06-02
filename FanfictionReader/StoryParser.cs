using System;
using System.Text.RegularExpressions;

namespace FanfictionReader {
    class StoryParser {
        /// <summary>
        /// Parses the metadata from the url to a story.
        /// </summary>
        /// <param name="url">A web-address pointing to a story</param>
        /// <returns>The information from this url, in a Story object. No metadata from the webpage is parsed.</returns>
        internal Story FromUrl(string url) {
            // Remove "http://", "https://", "www." etc from url
            url = Regex.Replace(url, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", string.Empty, RegexOptions.IgnoreCase);

            var words = url.Split('/');

            Story story = new Story();
            story.Host = words[0];
            story.PK = 0;
            story.Id = int.Parse(words[2]);
            story.ChapterID = int.Parse(words[3]);
            story.Title = words[4];

            return story;
        }
    }
}
