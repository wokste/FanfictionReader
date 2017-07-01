using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionReader {
    class StoryMeta {
        internal string Title;

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
    }
}
