using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FanfictionReader {
    class FFStoryParser {
        private string host = "fanfiction.net";

        private readonly Regex _storyTextRegex = new Regex(@"<div[^>]*id='storytext'[^>]*>([\s\S]*?)<\/div>");

        private readonly Regex _metadataRegex = new Regex(@"Rated:(.+)");
        private readonly Regex _htmlTagRegex = new Regex(@"<[^>]*?>");

        internal string GetStoryText(int storyId, int chapterId) {
            var html = GetHtml(storyId, chapterId);

            var match = _storyTextRegex.Match(html);

            if (match.Success)
                return match.Value;

            return "";
        }

        internal bool UpdateMeta(Story story) {
            // The metadata is the same for each chapter and chapter 1 always exists.
            var html = GetHtml(story.Id, 1);
            var metadataMatch = _metadataRegex.Match(html);
            var metaData = metadataMatch.Value;

            metaData = _htmlTagRegex.Replace(metaData, "");

            var tokens = metaData.Replace("Sci-Fi", "Sci+Fi").Split('-').Select(
                (s) => s.Trim().Replace("Sci+Fi", "Sci-Fi")
            );

            foreach (var token in tokens) {
                var split = token.Split(':').Select(s => s.Trim()).ToArray();
                var key = split[0];
                var value = (split.Length > 1) ? split[1] : "";

                UpdateMetaValue(story, key, value);
            }

            return true;
        }

        private void UpdateMetaValue(Story story, string key, string value) {
            switch (key) {
                case "Chapters":
                    story.Chapters = tokenToInt(value);
                    return;
                case "Words":
                    story.Words = tokenToInt(value);
                    return;
                case "Reviews":
                    story.Reviews = tokenToInt(value);
                    return;
                case "Favs":
                    story.Favs = tokenToInt(value);
                    return;
                case "Follows":
                    story.Follows = tokenToInt(value);
                    return;
                case "Complete":
                    story.Complete = true;
                    return;
                case "Rated":
                    story.MinimiumAge = tokenToRating(value);
                    return;
                case "Updated":
                    story.UpdateDate = tokenToDate(value);
                    return;
                case "Published":
                    story.PublishDate = tokenToDate(value);
                    return;
            }
        }


        /// <param name="dateStr">A date in the format "8/25/2015"</param>
        /// <returns>The date</returns>
        private DateTime tokenToDate(string dateStr) {
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
        private int tokenToRating(string rateStr) {
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
                    return -1;
            }
        }

        private int tokenToInt(string token){
            int i;
            
            if (!int.TryParse(token.Replace(",", ""), out i)) {
                return 0;
            }

            return i;
        }

        private string GetHtml(int storyId, int chapterId) {
            var url = $"http://{host}/s/{storyId}/{chapterId}";
            var client = new System.Net.WebClient {Encoding = Encoding.UTF8};
            var html = client.DownloadString(url);
           
            return html;
        }
    }
}
