namespace FanfictionReader {
    public class HtmlTemplate {
        private string _html = "<html><head><meta charset='utf-8'></head><body>{Chapter}</body></html>";
        public Chapter Chapter;

        public string Html => (_html.Replace("{Chapter}", Chapter.HtmlText));

        public string Title {
            get {
                var storyTitle = Chapter.Story?.MetaData?.Title;
                var chapterTitle = Chapter.Title;


                return storyTitle + " - " + chapterTitle;
            }
        }
    }
}