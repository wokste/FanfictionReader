namespace FanfictionReader {
    public class HtmlTemplate : IHtmlElement  {
        private string _html = "<html><head><meta charset='utf-8'></head><body>{Body}</body></html>";
        public IHtmlElement BodyElement;

        public string HtmlText => _html.Replace("{Body}", BodyElement.HtmlText);
        public string Title => BodyElement.Title;
    }
}