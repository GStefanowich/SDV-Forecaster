/*
 * This software is licensed under the MIT License
 * https://github.com/GStefanowich/SDV-Forecaster
 *
 * Copyright (c) 2019 Gregory Stefanowich
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using ForecasterText.Objects.Addons;
using ForecasterText.Objects.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;

namespace ForecasterText.Objects {
    internal sealed class ConfigEmojiMenu : ConfigEmojiComponent {
        private const int GAME_EMOJI = 197;
        private const int PER_ROW = 12;
        private const int ROWS = 5;
        private const int SHIFT = ConfigEmojiMenu.ROWS * ConfigEmojiMenu.PER_ROW;
        
        internal static Texture2D? EmojiTextures;
        internal static Texture2D? ChatBoxTexture;
        
        internal readonly Mod Mod;
        public readonly IConfT9N T9N;
        
        private readonly List<EmojiRegion> Emojis;
        private readonly List<ClickableComponent> EmojiButtons;
        private readonly ClickableComponent UpArrowButton;
        private readonly ClickableComponent DownArrowButton;
        
        private readonly Blink Blinking = new();
        
        private readonly Func<EmojiSet>? Getter;
        private readonly Action<EmojiSet>? Setter;
        
        public override EmojiSet Value {
            get => this.Getter?.Invoke() ?? EmojiSet.ZERO;
            set {
                this.Blinking.Reset();
                this.Setter?.Invoke(value);
            }
        }
        
        public int TotalEmojis { get; private init; }
        public int PageIndex { get; private set; }
        
        public ConfigEmojiMenu(
            Mod mod,
            IConfT9N t9N,
            Func<EmojiSet>? get,
            Action<EmojiSet>? set
        ) {
            ConfigEmojiMenu.EmojiTextures ??= Game1.content.Load<Texture2D>(@"LooseSprites\emojis");
            ConfigEmojiMenu.ChatBoxTexture ??= Game1.content.Load<Texture2D>(@"LooseSprites\chatBox");
            
            this.Mod = mod;
            this.T9N = t9N;
            
            this.Getter = get;
            this.Setter = set;
            
            this.Width = (16 + ConfigEmojiMenu.PER_ROW * 10 * 4) + 44; // 300
            this.Height = (16 + ConfigEmojiMenu.ROWS * 10 * 4) + 32; // 248
            
            this.Emojis = new List<EmojiRegion>();
            this.EmojiButtons = ConfigEmojiMenu.GenerateImageRegions()
                .ToList();
            this.UpArrowButton = new ClickableComponent(new Rectangle(16 + ConfigEmojiMenu.PER_ROW * 10 * 4, 16, 32, 20), "");
            this.DownArrowButton = new ClickableComponent(new Rectangle(16 + ConfigEmojiMenu.PER_ROW * 10 * 4, 156, 32, 20), "");
            
            // Total possible amount of emojis based on the sheet size (9 width * 9 height per emoji)
            this.TotalEmojis = ConfigEmojiMenu.EmojiTextures.Width / 9 * (ConfigEmojiMenu.EmojiTextures.Height / 9);
            
            this.ResetView();
        }
        
        public override void OnDraw(SpriteBatch b, Vector2I vector) {
            base.OnDraw(b, vector);
            this.DrawBox(b, vector);
            
            // Cache the scale update so if multiple emoji are selected we don't call the next scale value multiple times
            float? scale = null;
            
            int max = Math.Min(this.EmojiButtons.Count, this.Emojis.Count);
            
            for (int index = 0; index < max; ++index) {
                // Get the emoji
                EmojiRegion data = this.Emojis[index];
                ClickableComponent emoji = this.EmojiButtons[index];
                
                // Update the scale of the icon
                bool selected = this.Value.Contains(data.Id);
                if (selected) {
                    emoji.scale = scale ??= this.Blinking.Scale;
                } else if (emoji.scale < 1.0)
                    emoji.scale += Blink.SHIFT;
                
                // Draw the emoji
                b.Draw(
                    ConfigEmojiMenu.EmojiTextures,
                    new Vector2((float) (emoji.bounds.X + vector.X + 16), (float) (emoji.bounds.Y + vector.Y + 16)),
                    data.Rectangle,
                    selected ? Color.White : (Color.DimGray * 0.8f),
                    0.0f,
                    new Vector2(4.5f, 4.5f),
                    emoji.scale * 4f,
                    SpriteEffects.None,
                    0.9f
                );
            }
            
            if (this.UpArrowButton.scale < 1.0)
                this.UpArrowButton.scale += 0.05f;
            if (this.DownArrowButton.scale < 1.0)
                this.DownArrowButton.scale += 0.05f;
            
            // Draw the up/down buttons
            b.Draw(ConfigEmojiMenu.ChatBoxTexture, new Vector2((float) (this.UpArrowButton.bounds.X + vector.X + 16), (float) (this.UpArrowButton.bounds.Y + vector.Y + 10)), new Rectangle(156, 300, 32, 20), Color.White * (this.PageIndex == 0 ? 0.25f : 1f), 0.0f, new Vector2(16f, 10f), this.UpArrowButton.scale, SpriteEffects.None, 0.9f);
            b.Draw(ConfigEmojiMenu.ChatBoxTexture, new Vector2((float) (this.DownArrowButton.bounds.X + vector.X + 16), (float) (this.DownArrowButton.bounds.Y + vector.Y + 10)), new Rectangle(192, 300, 32, 20), Color.White * (this.PageIndex >= this.TotalEmojis - ConfigEmojiMenu.SHIFT ? 0.25f : 1f), 0.0f, new Vector2(16f, 10f), this.DownArrowButton.scale, SpriteEffects.None, 0.9f);
        }
        
        protected override void OnClick(MouseButton button, Vector2I bounds, Vector2I mouse) {
            Rectangle decrease = new(this.UpArrowButton.bounds.X + bounds.X, this.UpArrowButton.bounds.Y + bounds.Y, 32, 20);
            Rectangle increase = new(this.DownArrowButton.bounds.X + bounds.X, this.DownArrowButton.bounds.Y + bounds.Y, 32, 20);
            
            if (mouse.IsIn(decrease)) {
                if (this.PageIndex != 0)
                    Game1.playSound("Cowboy_Footstep");
                
                this.PageIndex = Math.Max(0, this.PageIndex - ConfigEmojiMenu.SHIFT);
                this.UpArrowButton.scale = 0.75f;
                
                this.ResetIcons();
            } else if (mouse.IsIn(increase)) {
                if (this.PageIndex < this.TotalEmojis - ConfigEmojiMenu.SHIFT)
                    Game1.playSound("Cowboy_Footstep");
                
                this.PageIndex = Math.Min(this.PageIndex + ConfigEmojiMenu.SHIFT, ConfigEmojiMenu.SHIFT * (int)(Math.Floor((double)this.TotalEmojis / ConfigEmojiMenu.SHIFT)));
                this.DownArrowButton.scale = 0.75f;
                
                this.ResetIcons();
            } else {
                Vector2I relative = mouse - bounds;
                
                double top = (relative.Y - 16d) / 40d;
                double left = (relative.X - 16d) / 40d;
                
                if (top >= 0.0f && left >= 0.0f) {
                    uint column = (uint)Math.Floor(left);
                    uint row = (uint)Math.Floor(top);
                    
                    if (column < ConfigEmojiMenu.PER_ROW && row < ConfigEmojiMenu.ROWS) {
                        int cursor = (int)((row * ConfigEmojiMenu.PER_ROW) + column);
                        if ( cursor < this.Emojis.Count ) {
                            EmojiRegion data = this.Emojis[cursor];
                            
                            // If the player is holding LeftControl then we want to select multiple
                            if ( this.Mod.Helper.Input.IsDown(SButton.LeftControl) ) {
                                // If the set already contains the emote
                                if ( button is MouseButton.RIGHT ) {
                                    this.Value -= data.Id;
                                } else if ( button is MouseButton.LEFT ) {
                                    this.Value += data.Id;
                                }
                            } else if ( button is MouseButton.LEFT ) {
                                this.Value = data.Id;
                            }
                            
                            Game1.playSound("coin");
                        }
                    }
                }
            }
        }
        
        public bool WithinBounds(int emote)
            => emote >= 0 && emote < this.TotalEmojis;
        
        public void ResetView() {
            this.PageIndex = 0;
            uint lowest = this.Value.First();
            
            // Make sure we scroll the current emoji into view by default
            while (lowest > this.PageIndex + ConfigEmojiMenu.SHIFT)
                this.PageIndex += ConfigEmojiMenu.SHIFT;
            
            this.ResetIcons();
        }
        
        public void ResetIcons() {
            // Calculate emojis by ignoring any that have a solid texture
            this.Emojis.Clear();
            this.Emojis.AddRange(this.GetEmojiFromSheet());
            
            // Reset the emoji scale
            this.EmojiButtons.ForEach(icon => icon.scale = 1.0f);
            
            // Reset the pulse
            this.Blinking.Reset();
        }
        
        private bool IsMuteImageTile(Color[] color, Rectangle rectangle, int startRow = 0) {
            int pixelCount = rectangle.Width * rectangle.Height;
            int firstPixel = startRow * rectangle.Width;
            int lastPixel = firstPixel + pixelCount - 1;
            
            if ( firstPixel >= 0 ) {
                Color first = color[firstPixel];
                
                for ( int i = firstPixel + 1; i <= lastPixel; i++ ) {
                    if ( !first.Equals(color[i]) ) {
                        return false;
                    }
                }
            }
            
            return true;
        }
        
        private IEnumerable<EmojiRegion> GetEmojiFromSheet() {
            for (int index = 0; index < this.EmojiButtons.Count; ++index) {
                int offset = index + this.PageIndex;
                
                Rectangle sourceRectangle = new(
                    offset * 9 % ConfigEmojiMenu.EmojiTextures!.Width,
                    offset * 9 / ConfigEmojiMenu.EmojiTextures.Width * 9,
                    9,
                    9
                );
                
                if (ConfigEmojiMenu.EmojiTextures.Bounds.Contains(sourceRectangle)) {
                    int pixelCount = sourceRectangle.Width * sourceRectangle.Height;
                    Color[] sourceData = ArrayPool<Color>.Shared.Rent(pixelCount);
                    try {
                        // Get the data of the area of the texture file
                        ConfigEmojiMenu.EmojiTextures.GetData(0, sourceRectangle, sourceData, 0, pixelCount);
                        
                        // If the area is not fully transparent or a shared color
                        if (!this.IsMuteImageTile(sourceData, sourceRectangle)) {
                            yield return new EmojiRegion {
                                Id = (uint)offset,
                                Rectangle = sourceRectangle
                            };
                        }
                    } finally {
                        ArrayPool<Color>.Shared.Return(sourceData);
                    }
                }
            }
        }
        
        private static IEnumerable<ClickableComponent> GenerateImageRegions() {
            for (int row = 0; row < ConfigEmojiMenu.ROWS; row++) {
                for (int e = 0; e < ConfigEmojiMenu.PER_ROW; e++) {
                    yield return new ClickableComponent(new Rectangle(16 + e * 10 * 4, 16 + row * 10 * 4, 36, 36), (row + e * ConfigEmojiMenu.PER_ROW).ToString());
                }
            }
        }
    }
}
