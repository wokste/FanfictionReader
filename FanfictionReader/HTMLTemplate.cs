namespace FanfictionReader {
    internal class HtmlTemplate {
        private string _html = "<html><head><meta charset='utf-8'></head><body>{Body}</body></html>";
        internal string Body;

        internal string MakeHtml() {
            return _html.Replace("{Body}", Body);
        }
        
    }
}