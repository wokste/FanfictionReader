namespace FanfictionReader {
    public class Chapter : IHtmlElement {
        public Story Story;
        public int ChapterId;
        public string ChapterTitle;
        public string HtmlText { get; set; }

        //public bool Valid => (Story != null && ChapterId > 0);

        public string Title {
            get { 
                var storyTitle = Story?.MetaData?.Title;

                return storyTitle + " - " + ChapterTitle;
            }
        }
    }
}
