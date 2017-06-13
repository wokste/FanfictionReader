using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionReader {
    class Chapter {
        internal Story Story;
        internal int ChapterId;
        internal string Title;
        internal string HtmlText;

        public bool Valid => (Story != null && ChapterId > 0);
    }
}
