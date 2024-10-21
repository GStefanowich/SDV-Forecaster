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

using ForecasterText.Constants;
using ForecasterText.Objects.Addons;
using ForecasterText.Objects.Enums;

namespace ForecasterText {
    /// <summary>
    /// The mods main config for storing/reading from disc
    /// </summary>
    public sealed class ForecasterConfig {
        #region Showing Weather
        
        /// <summary>When to show the weather for Stardew Valley</summary>
        public WeatherDisplay StardewValleyWeather { get; set; } = WeatherDisplay.ALWAYS;
        
        /// <summary>When to show the weather for Ginger Island</summary>
        public WeatherDisplay GingerIslandWeather { get; set; } = WeatherDisplay.ALWAYS;
        
        public EmojiSet SunWeatherEmoji { get; set; } = Emoji.SUN;
        public EmojiSet RainWeatherEmoji { get; set; } = Emoji.RAINDROPS;
        public EmojiSet RainGreenWeatherEmoji { get; set; } = new[] { Emoji.RAINDROPS, Emoji.CLOVER };
        public EmojiSet ThunderWeatherEmoji { get; set; } = new[] { Emoji.RAINDROPS, Emoji.LIGHTNING };
        public EmojiSet SnowWeatherEmoji { get; set; } = Emoji.SNOWFLAKE;
        public EmojiSet FestivalWeatherEmoji { get; set; } = new[] { Emoji.SUN, Emoji.BELL };
        public EmojiSet WeddingWeatherEmoji { get; set; } = new[] { Emoji.SUN, Emoji.SMALL_HEART };
        
        // WeatherWonders Weathers
        public EmojiSet AcidRainWeatherEmoji { get; set; } = Emoji.BROKEN_HEART;
        public EmojiSet BlizzardWeatherEmoji { get; set; } = new[] { Emoji.SNOWFLAKE, Emoji.SNOWFLAKE, Emoji.SNOWFLAKE };
        public EmojiSet CloudyWeatherEmoji { get; set; } = Emoji.BUTTERFLY;
        public EmojiSet DilugeWeatherEmoji { get; set; } = new[] { Emoji.RAINDROPS, Emoji.SWIRL };
        public EmojiSet DrizzleWeatherEmoji { get; set; } = Emoji.WATER_DROPLET;
        public EmojiSet DryLightningWeatherEmoji { get; set; } = Emoji.LIGHTNING;
        public EmojiSet HailstormWeatherEmoji { get; set; } = new[] { Emoji.SNOWFLAKE, Emoji.BREAKING_HEART };
        public EmojiSet HeatwaveWeatherEmoji { get; set; } = Emoji.FLAME;
        public EmojiSet MistWeatherEmoji { get; set; } = Emoji.BUBBLE;
        public EmojiSet MuddyRainWeatherEmoji { get; set; } = new[] { Emoji.RAINDROPS, Emoji.u56 };
        public EmojiSet SnowRainMixWeatherEmoji { get; set; } = new[] { Emoji.RAINDROPS, Emoji.SNOWFLAKE };
        public EmojiSet SandstormWeatherEmoji { get; set; } = new[] { Emoji.FLAME, Emoji.CACTUS };
        
        // WeatherWonders Night Events
        public EmojiSet MoonWeatherEmoji { get; set; } = Emoji.MOON;
        public EmojiSet BloodMoonWeatherEmoji { get; set; } = new[] { Emoji.MOON, Emoji.PINK_SKULL };
        public EmojiSet BlueMoonWeatherEmoji { get; set; } = new[] { Emoji.MOON, Emoji.STARDROP };
        public EmojiSet HarvestMoonWeatherEmoji { get; set; } = new[] { Emoji.MOON, Emoji.TURNIP };
        
        #endregion
        #region Luck Emoji
        
        public bool ShowGoodLuck { get; set; } = true;
        public bool ShowNeutralLuck { get; set; } = true;
        public bool ShowBadLuck { get; set; } = true;
        
        public EmojiSet SpiritsEmoji { get; set; } = Emoji.GHOST;
        public EmojiSet VeryHappySpiritEmoji { get; set; } = Emoji.HEART;
        public EmojiSet GoodHumorSpiritEmoji { get; set; } = Emoji.u2;
        public EmojiSet NeutralSpiritEmoji { get; set; } = Emoji.u16;
        public EmojiSet SomewhatAnnoyedSpiritEmoji { get; set; } = Emoji.u18;
        public EmojiSet MildlyPerturbedSpiritEmoji { get; set; } = Emoji.u11;
        public EmojiSet VeryDispleasedSpiritEmoji { get; set; } = Emoji.u14;
        
        #endregion
        #region Recipe Emoji
        
        public bool ShowNewRecipes { get; set; } = true;
        public EmojiSet NewRecipeEmoji { get; set; } = Emoji.EXCLAMATION;
        
        public bool ShowExistingRecipes { get; set; } = false;
        public EmojiSet KnownRecipeEmoji { get; set; } = Emoji.PAPER;
        
        #endregion
        #region Birthdays
        
        public bool ShowBirthdays { get; set; } = false;
        public bool UseVillagerNames { get; set; } = false;
        public EmojiSet BirthdayEmoji { get; set; } = Emoji.PRESENT;
        
        #endregion
        #region Multiplayer
        
        public bool SendToOthers { get; set; } = false;
        public bool UseSameForOthers { get; set; } = true;
        public ForecasterConfig? Child { get; set; } = null;
        
        #endregion
        #region Other Reminders
        
        public bool TravelingMerchantReminder { get; set; }
        
        #endregion
        #region Getters
        
        public EmojiSet? GetEmojis(WeatherIcons icon) => icon switch {
            WeatherIcons.SUN => this.SunWeatherEmoji,
            WeatherIcons.RAIN => this.RainWeatherEmoji,
            WeatherIcons.GREEN_RAIN => this.RainGreenWeatherEmoji,
            WeatherIcons.LIGHTNING => this.ThunderWeatherEmoji,
            WeatherIcons.FESTIVAL => this.FestivalWeatherEmoji,
            WeatherIcons.SNOW => this.SnowWeatherEmoji,
            WeatherIcons.WEDDING => this.WeddingWeatherEmoji,
            
            // WeatherWonders Weathers
            WeatherIcons.ACID_RAIN => this.AcidRainWeatherEmoji,
            WeatherIcons.BLIZZARD => this.BlizzardWeatherEmoji,
            WeatherIcons.CLOUDY => this.CloudyWeatherEmoji,
            WeatherIcons.DILUGE => this.DilugeWeatherEmoji,
            WeatherIcons.DRIZZLE => this.DrizzleWeatherEmoji,
            WeatherIcons.DRY_LIGHTNING => this.DryLightningWeatherEmoji,
            WeatherIcons.HAILSTORM => this.HailstormWeatherEmoji,
            WeatherIcons.HEATWAVE => this.HeatwaveWeatherEmoji,
            WeatherIcons.MIST => this.MistWeatherEmoji,
            WeatherIcons.MUDDY_RAIN => this.MuddyRainWeatherEmoji,
            WeatherIcons.SNOW_RAIN_MIX => this.SnowRainMixWeatherEmoji,
            WeatherIcons.SANDSTORM => this.SandstormWeatherEmoji,
            
            // WeatherWonders Night Events
            WeatherIcons.MOON => this.MoonWeatherEmoji,
            WeatherIcons.BLOOD_MOON => this.BloodMoonWeatherEmoji,
            WeatherIcons.BLUE_MOON => this.BlueMoonWeatherEmoji,
            WeatherIcons.HARVEST_MOON => this.HarvestMoonWeatherEmoji,
            _ => EmojiSet.ZERO
        };
        public EmojiSet? GetEmojis(SpiritMoods icon) => icon switch {
            SpiritMoods.VERY_HAPPY => this.VeryHappySpiritEmoji,
            SpiritMoods.GOOD_HUMOR => this.GoodHumorSpiritEmoji,
            SpiritMoods.NEUTRAL => this.NeutralSpiritEmoji,
            SpiritMoods.SOMEWHAT_ANNOYED => this.SomewhatAnnoyedSpiritEmoji,
            SpiritMoods.MILDLY_PERTURBED => this.MildlyPerturbedSpiritEmoji,
            SpiritMoods.VERY_DISPLEASED => this.VeryDispleasedSpiritEmoji,
            _ => EmojiSet.ZERO
        };
        public EmojiSet? GetEmojis(MiscEmoji icon) => icon switch {
            MiscEmoji.SPIRITS => this.SpiritsEmoji,
            MiscEmoji.BIRTHDAY => this.BirthdayEmoji,
            MiscEmoji.NEW_RECIPE => this.NewRecipeEmoji,
            MiscEmoji.KNOWN_RECIPE => this.KnownRecipeEmoji,
            _ => EmojiSet.ZERO
        };
        
        #endregion
    }
}
