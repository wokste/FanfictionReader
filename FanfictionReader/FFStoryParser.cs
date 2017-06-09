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
            
            throw new NotImplementedException();
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
