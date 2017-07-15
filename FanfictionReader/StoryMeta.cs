using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionReader {
    public class StoryMeta {
        public string Title;

        public int AuthorId = 0;
        public int ChapterCount = 0;
        public int Words = 0;
        public bool IsComplete = false;
        public int MinimumAge = 5;
        public DateTime UpdateDate;
        public DateTime PublishDate;
        public DateTime MetaCheckDate;
        
        // TODO: Add the following fields in the database
        public int Favs = 0;
        public int Follows = 0;
        public int Reviews = 0;
    }
}
