namespace ForecasterText.Objects {
    internal sealed class MessageSource {
        public string Source;
        public string Message;
        
        private MessageSource(string source, string message) {
            this.Source = source;
            this.Message = message;
        }
        
        public override string ToString() => $"{this.Source}: {this.Message}";
        
        public static MessageSource TV(string message = "") {
            if (message is null)
                return null;
            return new MessageSource("TV", message);
        }
        public static MessageSource Calendar(string message = "") {
            if (message is null)
                return null;
            return new MessageSource("Calendar", message);
        }
    }
}
