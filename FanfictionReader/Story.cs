using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionReader {
    class Story {
        internal int Id;
        internal string Title;
        internal int ChapterID;

        internal string getUrl() {
            return string.Format("http://fanfiction.net/s/{0}/{1}", Id, ChapterID);
        }

        public override string ToString() {
            return string.Format("{0} ({1})", Title, ChapterID);
        }

        internal static Story makeFromUrl(string url) {
            // TODO: Data validation
            url = url.Remove(0, 8); // Remove http(s)://

            var words = url.Split('/');

            Story ret = new Story();
            ret.Id = int.Parse(words[2]);
            ret.ChapterID = int.Parse(words[3]);
            ret.Title = words[4];
            return ret;
        }
    }
}
