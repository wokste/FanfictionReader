using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FanfictionReader {
    class FFStoryParser {
        private string host = "fanfiction.net";

        private readonly Regex STORY_TEXT_REGEX = new Regex(@"<div[^>]*id='storytext'[^>]*>([\s\S]*?)<\/div>");

        private readonly Regex METADATA_REGEX = new Regex(@"Rated:(.+)");
        private readonly Regex HTML_TAG_REGEX = new Regex(@"<[^>]*?>");

        internal FFStoryParser() {
        }

        internal string GetStoryText(int storyID, int chapterID) {
            var html = getHTML(storyID, chapterID);

            var match = STORY_TEXT_REGEX.Match(html);

            if (match != null && match.Success)
                return match.Value;

            return "";
        }

        internal bool UpdateMeta(Story story) {
            // The metadata is the same for each chapter and chapter 1 always exists.
            var html = getHTML(story.Id, 1);
            var metadataMatch = METADATA_REGEX.Match(html);
            var metaData = metadataMatch.Value;

            metaData = HTML_TAG_REGEX.Replace(metaData, "");

            var tokens = metaData.Replace("Sci-Fi", "Sci+Fi").Split('-').Select(
                (s) => { return s.Trim().Replace("Sci+Fi", "Sci-Fi"); }
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

        private int tokenToInt(string token) {
            var i = 0;
            
            if (!int.TryParse(token.Replace(",", ""), out i)) {
                return 0;
            }

            return i;
        }

        private string getHTML(int storyID, int chapterID) {
            var url = string.Format("http://{0}/s/{1}/{2}", host, storyID, chapterID);
            var client = new System.Net.WebClient();
            client.Encoding = Encoding.UTF8;
            var html = client.DownloadString(url);
           
            return html;
        }
    }
}
