using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionReader {
    class Story {
        /// <summary>The primary key in the database. 0 is not in the database.</summary>
        internal long PK;
        
        internal int Id;
        internal string Title;
        internal int ChapterID;        
        internal string Host;
        internal DateTime AddDate;
        internal DateTime LastReadDate;

        internal string getUrl() {
            return string.Format("http://{0}/s/{1}/{2}", Host, Id, ChapterID);
        }

        public override string ToString() {
            return string.Format("{0} ({1})", Title, ChapterID);
        }
    }
}
