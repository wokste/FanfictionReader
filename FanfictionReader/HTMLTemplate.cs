namespace FanfictionReader {
    internal class HtmlTemplate {
        private string _html = "<html><head><meta charset='utf-8'></head><body>{Chapter}</body></html>";
        internal Chapter Chapter;

        internal string Html => (_html.Replace("{Chapter}", Chapter.HtmlText));

        internal string Title {
            get {
                var storyTitle = Chapter.Story?.MetaData?.Title;
                var chapterTitle = Chapter.Title;


                return storyTitle + " - " + chapterTitle;
            }
        }
    }
}