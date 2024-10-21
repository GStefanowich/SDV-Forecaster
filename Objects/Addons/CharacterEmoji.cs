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

using System.Globalization;
using ForecasterText.Constants;
using StardewValley;

namespace ForecasterText.Objects.Addons {
    public static class CharacterEmoji {
        public static string GetName(this Character character)
            => character switch {
                NPC npc => npc.getName(),
                _ => character.Name
            };
        
        public static bool HasEmoji(this Character character)
            => GetEmoji(character.GetName()) is not null;
        
        public static bool HasEmoji(string name)
            => GetEmoji(name) is not null;
        
        public static uint? GetEmoji(string name)
            => name.ToLower(CultureInfo.InvariantCulture) switch {
                "abigail" => Emoji.ABIGAIL,
                "penny" => Emoji.PENNY,
                "maru" => Emoji.MARU,
                "leah" => Emoji.LEAH,
                "haley" => Emoji.HALEY,
                "emily" => Emoji.EMILY,
                "alex" => Emoji.ALEX,
                "shane" => Emoji.SHANE,
                "sebastian" => Emoji.SEBASTIAN,
                "sam" => Emoji.SAM,
                "harvey" => Emoji.HARVEY,
                "elliot" => Emoji.ELLIOT,
                "sandy" => Emoji.SANDY,
                "evelyn" => Emoji.EVELYN,
                "marnie" => Emoji.MARNIE,
                "caroline" => Emoji.CAROLINE,
                "robin" => Emoji.ROBIN,
                "pierre" => Emoji.PIERRE,
                "pam" => Emoji.PAM,
                "jodi" => Emoji.JODI,
                "lewis" => Emoji.LEWIS,
                "linus" => Emoji.LINUS,
                "marlon" => Emoji.MARLON,
                "willy" => Emoji.WILLY,
                "wizard" => Emoji.WIZARD,
                "morris" => Emoji.MORRIS,
                "jas" => Emoji.JAS,
                "vincent" => Emoji.VINCENT,
                "krobus" => Emoji.KROBUS,
                "dwarf" => Emoji.DWARF,
                "gus" => Emoji.GUS,
                "gunther" => Emoji.GUNTHER,
                "george" => Emoji.GEORGE,
                "demetrius" => Emoji.DEMETRIUS,
                "clint" => Emoji.CLINT,
                _ => null
            };
    }
}
