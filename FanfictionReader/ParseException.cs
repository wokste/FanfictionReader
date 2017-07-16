using System;

namespace FanfictionReader {
    public class ParseException : Exception {
        public ParseException(string message) : base(message) {
        }
    }
}
