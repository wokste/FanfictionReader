using System;

namespace FanfictionReader {
    public class Story {
        /// <summary>The primary key in the database. 0 is not in the database.</summary>
        public long Pk;
        
        public int Id;
        public string Host;

        public int LastReadChapterId;
        public DateTime AddDate;
        public DateTime LastReadDate;

        public StoryMeta MetaData = new StoryMeta();
        
        public override string ToString() {
            return $"{MetaData.Title} ({LastReadChapterId} / {MetaData.ChapterCount})";
        }
    }
}
