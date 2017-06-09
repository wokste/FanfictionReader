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

        // TODO: Add the following fields in the database
        internal int Chapters = 0;
        internal int Favs = 0;
        internal int Follows = 0;
        internal int Reviews = 0;
        internal int Words = 0;
        internal bool Complete = false;

        public override string ToString() {
            return string.Format("{0} ({1})", Title, ChapterID);
        }
    }
}
