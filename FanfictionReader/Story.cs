using System;

namespace FanfictionReader {
    internal class Story {
        /// <summary>The primary key in the database. 0 is not in the database.</summary>
        internal long Pk;
        
        internal int Id;
        internal string Host;

        internal int LastReadChapterId;
        internal DateTime AddDate;
        internal DateTime LastReadDate;

        internal StoryMeta MetaData = new StoryMeta();
        
        public override string ToString() {
            return $"{MetaData.Title} ({LastReadChapterId} / {MetaData.ChapterCount})";
        }
    }
}
