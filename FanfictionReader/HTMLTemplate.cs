namespace FanfictionReader {
    internal class HTMLTemplate {
        private string HTML = "<html><head><meta charset='utf-8'></head><body>{Body}</body></html>";
        internal string Body;

        internal HTMLTemplate() {

        }

        internal string MakeHTML() {
            return HTML.Replace("{Body}", Body);
        }
        
    }
}