namespace FanfictionReader {
    class Chapter {
        internal Story Story;
        internal int ChapterId;
        internal string Title;
        internal string HtmlText;

        public bool Valid => (Story != null && ChapterId > 0);
    }
}
