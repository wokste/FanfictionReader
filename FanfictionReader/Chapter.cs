namespace FanfictionReader {
    public class Chapter {
        public Story Story;
        public int ChapterId;
        public string Title;
        public string HtmlText;

        public bool Valid => (Story != null && ChapterId > 0);
    }
}
