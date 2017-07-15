using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FanfictionReader {
    public class FictionpressStoryParser {
        private readonly string _host = "fanfiction.net";

        private readonly Regex _storyTextRegex = new Regex(@"<div[^>]*id='storytext'[^>]*>([\s\S]*?)<\/div>");

        private readonly Regex _metaDataRegex = new Regex(@"Rated:(.+)");
        private readonly Regex _htmlTagRegex = new Regex(@"<[^>]*?>");

        private readonly Regex _titleRegex = new Regex(@"<b class='xcontrast_txt'>(.*?)<\/b>");

        public Chapter GetChapter(Story story, int chapterId) {
            string html = GetHtml(story.Id, chapterId);

            var match = _storyTextRegex.Match(html);

            if (!match.Success) {
                return null;
            }

            return new Chapter {
                HtmlText = match.Groups[1].Value,
                ChapterId = chapterId,
                Story = story,
                Title = ""
            };
        }

        /// <summary>
        /// Update the metadata of the story by making a request to the server.
        /// </summary>
        /// <param name="story">The story of which the metadata should be updated</param>
        /// <returns>If the MetaData is reetrieved correctly, a StoryMeta object with the information. Null can indicate an unresponsive webpage or formatting issues in the HTML page.</returns>
        public StoryMeta GetMeta(Story story) {
            string html;
            try {
                // The metadata is the same for each chapter and chapter 1 always exists.
                html = GetHtml(story.Id, 1);
            }
            catch (WebException) {
                return null;
            }

            var metaDataMatch = _metaDataRegex.Match(html);
            if (!metaDataMatch.Success) {
                return null;
            }

            var metaDataString = metaDataMatch.Value;

            metaDataString = _htmlTagRegex.Replace(metaDataString, "");

            var tokens = metaDataString.Replace("Sci-Fi", "Sci+Fi").Split('-').Select(
                s => s.Trim().Replace("Sci+Fi", "Sci-Fi")
            );

            var meta = new StoryMeta();

            meta.Title = _titleRegex.Match(html).Groups[1].Value;

            foreach (var token in tokens) {
                var split = token.Split(':').Select(s => s.Trim()).ToArray();
                var key = split[0];
                var value = (split.Length > 1) ? split[1] : "";

                UpdateMetaValue(meta, key, value);
            }

            meta.MetaCheckDate = DateTime.Now;
            return meta;
        }

        private void UpdateMetaValue(StoryMeta meta, string key, string value) {
            switch (key) {
                case "Chapters":
                    meta.ChapterCount = TokenToInt(value);
                    return;
                case "Words":
                    meta.Words = TokenToInt(value);
                    return;
                case "Reviews":
                    meta.Reviews = TokenToInt(value);
                    return;
                case "Favs":
                    meta.Favs = TokenToInt(value);
                    return;
                case "Follows":
                    meta.Follows = TokenToInt(value);
                    return;
                case "Status":
                    meta.IsComplete = (value == "Complete");
                    return;
                case "Rated":
                    meta.MinimumAge = TokenToRating(value);
                    return;
                case "Updated":
                    meta.UpdateDate = TokenToDate(value, DateTime.Now);
                    return;
                case "Published":
                    meta.PublishDate = TokenToDate(value, DateTime.Now);
                    return;
                default: {
                    
                    return;
                }
            }
        }
        
        /// <param name="dateStr">A date in the format "8/25/2015, 8/25, 1h or 12m"</param>
        /// <returns>The date</returns>
        public DateTime TokenToDate(string dateStr, DateTime now) {
            var provider = System.Globalization.CultureInfo.InvariantCulture;
            var formats = new[]{ "M/d/yyyy"};
            var postfixes = new Dictionary<string, long> { {"m", TimeSpan.TicksPerMinute}, { "h", TimeSpan.TicksPerHour } };
            DateTime result = now;

            // Format 12m, 10h
            foreach (var postfix in postfixes) {
                if (dateStr.EndsWith(postfix.Key)) {
                    var subStr = dateStr.Remove(dateStr.Length - 1, 1);
                    result -= TimeSpan.FromTicks(postfix.Value * TokenToInt(subStr));
                    return result;
                }
            }

            // Format: 8/25/2015
            if (DateTime.TryParseExact(dateStr, formats, provider, DateTimeStyles.None, out result)) {
                return result;
            }
            
            // Format: 8/25. Appending with current year.
            if (DateTime.TryParseExact(dateStr + "/" + now.Year, formats, provider, DateTimeStyles.None, out result)) {
                return result;
            }

            Console.WriteLine("Could not format date-time: {0}.", dateStr);

            return new DateTime(0);
        }

        /// <param name="rateStr"></param>
        /// <returns>The minimum age or -1 if no formatting could be found</returns>
        private int TokenToRating(string rateStr) {
            rateStr = rateStr.Replace("  ", " ");
            switch (rateStr) {
                case "Fiction K":
                    return 5;
                case "Fiction K+":
                    return 9;
                case "Fiction T":
                    return 13;
                case "Fiction M":
                    return 16;
                case "Fiction MA":
                    return 18;
                default:
                    return 0;
            }
        }

        public int TokenToInt(string token){
            int i;
            return int.TryParse(token.Replace(",", ""), out i) ? i : 0;
        }

        private string GetHtml(int storyId, int chapterId) {
            var uri = new System.Uri($"http://{_host}/s/{storyId}/{chapterId}");
            using (var client = new WebClient {Encoding = Encoding.UTF8 }){
                var html = client.DownloadString(uri);
                return html;
            }
        }
    }
}
