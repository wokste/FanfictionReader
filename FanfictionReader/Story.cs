using System;

namespace FanfictionReader {
    internal class Story {
        /// <summary>The primary key in the database. 0 is not in the database.</summary>
        internal long Pk;
        
        internal int Id;
        internal string Title;
        internal int LastReadChapterId;
        internal string Host;
        internal DateTime AddDate;
        internal DateTime LastReadDate;

        internal int AuthorId = 0;
        internal int ChapterCount = 0;
        internal int Words = 0;
        internal bool IsComplete = false;
        internal int MinimumAge = 5;
        internal DateTime UpdateDate;
        internal DateTime PublishDate;
        internal DateTime MetaCheckDate;

        // TODO: Add the following fields in the database
        internal int Favs = 0;
        internal int Follows = 0;
        internal int Reviews = 0;

        public override string ToString() {
            return $"{Title} ({LastReadChapterId} / {ChapterCount})";
        }
    }
}
