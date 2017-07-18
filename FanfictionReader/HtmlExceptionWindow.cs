using System;

namespace FanfictionReader {
    class HtmlExceptionWindow : IHtmlElement {
        public Exception Exception;

        public string HtmlText => Exception.Message;
        public string Title => Exception.GetType().ToString();
    }
}
