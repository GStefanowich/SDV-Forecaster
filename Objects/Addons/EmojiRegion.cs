using Microsoft.Xna.Framework;

namespace ForecasterText.Objects.Addons {
    /// <summary>
    /// An Emoji based on its position within the emoji filesheet
    /// </summary>
    public struct EmojiRegion {
        public uint Id { get; init; }
        
        public Rectangle Rectangle { get; init; }
    }
}
