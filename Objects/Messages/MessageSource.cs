using StardewModdingAPI;
using StardewValley;

namespace ForecasterText.Objects.Messages {
    internal sealed class MessageSource : ISourceMessage {
        public string T9N;
        public ISourceMessage Message;
        
        private MessageSource(string t9N, ISourceMessage message) {
            this.T9N = $"source.{t9N}";
            this.Message = message;
        }
        
        /// <inheritdoc/>
        public string Write(Farmer farmer, ITranslationHelper t9N, ForecasterConfig config)
            => t9N.Get(this.T9N, new {
                content = this.Message.Write(farmer, t9N, config)
            });
        
        public static MessageSource TV(ISourceMessage message)
            => message is null ? null : new MessageSource("tv", message);
        
        public static MessageSource Calendar(ISourceMessage message)
            => message is null ? null : new MessageSource("calendar", message);
    }
}
