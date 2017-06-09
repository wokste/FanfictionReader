using System.Text.RegularExpressions;

namespace FanfictionReader {
    internal class UrlParser {
        /// <summary>
        /// Parses the metadata from the url to a story.
        /// </summary>
        /// <param name="url">A web-address pointing to a story</param>
        /// <returns>The information from this url, in a Story object. No metadata from the webpage is parsed.</returns>
        internal Story UrlToStory(string url) {
            // Remove "http://", "https://", "www." etc from url
            url = Regex.Replace(url, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", "", RegexOptions.IgnoreCase);

            var words = url.Split('/');

            var story = new Story
            {
                Host = words[0],
                SqlPk = 0,
                Id = int.Parse(words[2]),
                ChapterId = int.Parse(words[3]),
                Title = words[4]
            };

            return story;
        }
    }
}
