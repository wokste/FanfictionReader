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

        internal string getStoryText(int storyID, int chapterID) {
            var html = getHTML(storyID, chapterID);

            var match = STORY_TEXT_REGEX.Match(html);

            if (match != null && match.Success)
                return match.Value;

            return "";
        }

        internal Story getMeta(int storyID) {
            // It doesn't matter what chapter you take. The metadata is always the same.
            var html = getHTML(storyID, 1);
            var metadataMatch = METADATA_REGEX.Match(html);
            var metaData = metadataMatch.Value;

            metaData = HTML_TAG_REGEX.Replace(metaData, "");

            var tokens = metaData.Replace("Sci-Fi", "Sci+Fi").Split('-').Select(
                (s) => { return s.Trim().Replace("Sci+Fi", "Sci-Fi"); }
            );

            Story story = new Story();

            /*
            var crossOver = matchString(CROSSOVER_REGEX, body);
            if (crossOver) {
                story.sourceName = crossOver;
            } else {
                story.sourceMedium = matchString(SOURCE_MEDIUM_REGEX, body);
                story.sourceName = matchString(SOURCE_NAME_REGEX, body);
            }
            */

            /*
            console.log(tokens[2]);
            if (this.isGenre(tokens[2])) {
                story.genre = tokens[2].split('/');
                if (!this.startsWithTerminator(tokens[3])) {
                    story.characters = tokens[3].split(',').map(character => character.trim().replace('[', '').replace(']', ''));
                }
            } else if (!this.startsWithTerminator(tokens[2])) {
                story.characters = tokens[2].split(',').map(character => character.trim().replace('[', '').replace(']', ''));
            }
            */

            foreach (var token in tokens) {
                var split = token.Split(':').Select(s => s.Trim()).ToArray();
                var type = split[0];
                var value = (split.Length > 1) ? split[1] : "";

                switch (type) {
                    case "Chapters":
                        story.Chapters = tokenToInt(value);
                        break;
                    case "Words":
                        story.Words = tokenToInt(value);
                        break;
                    case "Reviews":
                        story.Reviews = tokenToInt(value);
                        break;
                    case "Favs":
                        story.Favs = tokenToInt(value);
                        break;
                    case "Follows":
                        story.Follows = tokenToInt(value);
                        break;
                    case "Complete":
                        story.Complete = true;
                        break;
                }
            }

            Console.Write("Chapters:{0}\nFavs:{1}\nFollows:{2}\nReviews:{3}\nWords:{4}\n\n", story.Chapters, story.Favs, story.Follows, story.Reviews, story.Words);

            return story;
        }

        private int tokenToInt(string token) {
            //TODO: change this in tryparse

            return int.Parse(token.Replace(",", ""));
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
