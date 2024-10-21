/*
 * This software is licensed under the MIT License
 * https://github.com/GStefanowich/SDV-Forecaster
 *
 * Copyright (c) 2024 Gregory Stefanowich
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ForecasterText.Objects.Addons {
    /// <summary>
    /// A set of defined Emoji values (Can be one or more)
    /// </summary>
    [JsonConverter(typeof(EmojiSetConverter))]
    public struct EmojiSet : IEnumerable<uint> {
        public static readonly EmojiSet ZERO = 0u;
        
        public int Count => this.Values.Length;
        private readonly uint[] Values;
        
        public EmojiSet(uint[] values) {
            this.Values = values;
        }
        public EmojiSet(IEnumerable<uint> values): this(values.ToArray()) {}
        
        public uint First() {
            uint? lowest = null;

            foreach (uint value in this.Values) {
                if (lowest is null || value < lowest) {
                    lowest = value;
                }
            }
            
            return lowest ?? 0u;
        }
        
        public bool Contains(uint id) {
            for (int i = 0; i < this.Values.Length; i++) {
                if (this.Values[i] == id) {
                    return true;
                }
            }
            return false;
        }
        
        public bool AreEquals(EmojiSet other) {
            if (this.Count != other.Count) {
                return false;
            }
            
            for (int i = 0; i < this.Count; i++) {
                if (!this.Contains(other.Values[i]))
                    return false;
            }
            
            return true;
        }
        
        /// <inheritdoc />
        public IEnumerator<uint> GetEnumerator()
            => ((IEnumerable<uint>) this.Values).GetEnumerator();
        
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
        
        #region Operators
        
        public static implicit operator EmojiSet(uint id) {
            return new EmojiSet(new[] { id });
        }
        
        public static implicit operator EmojiSet(uint[] ids) {
            return new EmojiSet(ids);
        }
        
        public static EmojiSet operator +(EmojiSet list, uint id) {
            return new EmojiSet(list.Concat(new[] { id }));
        }
        
        public static EmojiSet operator -(EmojiSet list, uint id) {
            // Can't return a list that is empty
            // Also don't create a new list if the id isn't contained
            if (list.Count is 1 || !list.Contains(id)) {
                return list;
            }
            
            return new EmojiSet(list.Except(new[] { id }));
        }
        
        #endregion
    }
}
