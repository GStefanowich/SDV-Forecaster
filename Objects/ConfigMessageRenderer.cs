using System.Collections.Generic;
using StardewValley.Menus;

namespace ForecasterText.Objects {
    internal delegate IEnumerable<ChatSnippet> ConfigMessageRenderer(ConfigEmojiMessage message);
    internal delegate string ConfigMessageParsingRenderer(ConfigEmojiMessage message);
}
