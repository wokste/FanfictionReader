using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace FanfictionReader {
    internal class FictionpressStoryParser {
        private readonly string _host = "fanfiction.net";

        private readonly Regex _storyTextRegex = new Regex(@"<div[^>]*id='storytext'[^>]*>([\s\S]*?)<\/div>");

        private readonly Regex _metadataRegex = new Regex(@"Rated:(.+)");
        private readonly Regex _htmlTagRegex = new Regex(@"<[^>]*?>");

        internal Chapter GetChapter(Story story, int chapterId) {
            string html = GetHtml(story.Id, chapterId);

            var match = _storyTextRegex.Match(html);

            if (!match.Success) {
                return null;
            }

            return new Chapter {
                HtmlText = match.Value,
                ChapterId = chapterId,
                StoryPk = story.Pk,
                Title = ""
            };
        }

        /// <summary>
        /// Update the metadata of the story by making a request to the server.
        /// </summary>
        /// <param name="story">The story of which the metadata should be updated</param>
        /// <returns>Whether it was successfull in making an update. False can indicate an unresponsive webpage or formatting issues in the HTML page.</returns>
        internal bool UpdateMeta(Story story) {
            string html = "";
            try {
                // The metadata is the same for each chapter and chapter 1 always exists.
                html = GetHtml(story.Id, 1);
            }
            catch (WebException) {
                return false;
            }

            var metadataMatch = _metadataRegex.Match(html);
            if (!metadataMatch.Success) {
                return false;
            }

            var metaData = metadataMatch.Value;

            metaData = _htmlTagRegex.Replace(metaData, "");

            var tokens = metaData.Replace("Sci-Fi", "Sci+Fi").Split('-').Select(
                s => s.Trim().Replace("Sci+Fi", "Sci-Fi")
            );

            foreach (var token in tokens) {
                var split = token.Split(':').Select(s => s.Trim()).ToArray();
                var key = split[0];
                var value = (split.Length > 1) ? split[1] : "";

                UpdateMetaValue(story, key, value);
            }

            story.MetaCheckDate = DateTime.Now;
            return true;
        }

        private void UpdateMetaValue(Story story, string key, string value) {
            switch (key) {
                case "Chapters":
                    story.ChapterCount = TokenToInt(value);
                    return;
                case "Words":
                    story.Words = TokenToInt(value);
                    return;
                case "Reviews":
                    story.Reviews = TokenToInt(value);
                    return;
                case "Favs":
                    story.Favs = TokenToInt(value);
                    return;
                case "Follows":
                    story.Follows = TokenToInt(value);
                    return;
                case "Complete":
                    story.IsComplete = true;
                    return;
                case "Rated":
                    story.MinimumAge = TokenToRating(value);
                    return;
                case "Updated":
                    story.UpdateDate = TokenToDate(value);
                    return;
                case "Published":
                    story.PublishDate = TokenToDate(value);
                    return;
                default: {
                    
                    return;
                }
            }
        }


        /// <param name="dateStr">A date in the format "8/25/2015"</param>
        /// <returns>The date</returns>
        private DateTime TokenToDate(string dateStr) {
            var provider = System.Globalization.CultureInfo.InvariantCulture;

            var format = "M/d/yyyy";
            try {
                return DateTime.ParseExact(dateStr, format, provider);
            } catch (FormatException) {
                Console.WriteLine("{0} is not in the correct format {1}.", dateStr, format);
            }
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

        private int TokenToInt(string token){
            int i;
            return int.TryParse(token.Replace(",", ""), out i) ? i : 0;
        }

        private string GetHtml(int storyId, int chapterId) {
            var url = $"http://{_host}/s/{storyId}/{chapterId}";
            using (var client = new System.Net.WebClient {Encoding = Encoding.UTF8 }){
                var html = client.DownloadString(url);
                return html;
            }
        }
    }
}
