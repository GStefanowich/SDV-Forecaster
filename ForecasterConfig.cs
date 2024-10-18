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
        
        public uint SunWeatherEmoji { get; set; } = 99u;
        public uint RainWeatherEmoji { get; set; } = 100u;
        public uint ThunderWeatherEmoji { get; set; } = 102u;
        public uint SnowWeatherEmoji { get; set; } = 103u;
        public uint FestivalWeatherEmoji { get; set; } = 151u;
        public uint WeddingWeatherEmoji { get; set; } = 46u;
        // WeatherWonders Weathers
        public uint AcidRainWeatherEmoji { get; set; } = 45u;
        public uint BlizzardWeatherEmoji { get; set; } = 103u;
        public uint CloudyWeatherEmoji { get; set; } = 105u;
        public uint DilugeWeatherEmoji { get; set; } = 49u;
        public uint DrizzleWeatherEmoji { get; set; } = 65u;
        public uint DryLightningWeatherEmoji { get; set; } = 102u;
        public uint HailstormWeatherEmoji { get; set; } = 44u;
        public uint HeatwaveWeatherEmoji { get; set; } = 143u;
        public uint MistWeatherEmoji { get; set; } = 104u;
        public uint MuddyRainWeatherEmoji { get; set; } = 56u;
        public uint SnowRainMixWeatherEmoji { get; set; } = 103u;
        public uint SandstormWeatherEmoji { get; set; } = 148u;
        // WeatherWonders Night Events
        public uint MoonWeatherEmoji { get; set; } = 98u;
        public uint BloodMoonWeatherEmoji { get; set; } = 34u;
        public uint BlueMoonWeatherEmoji { get; set; } = 87u;
        public uint HarvestMoonWeatherEmoji { get; set; } = 58u;
        #endregion
        #region Luck Emoji

        public bool ShowGoodLuck { get; set; } = true;
        public bool ShowNeutralLuck { get; set; } = true;
        public bool ShowBadLuck { get; set; } = true;
        
        public uint SpiritsEmoji { get; set; } = 119u;
        public uint VeryHappySpiritEmoji { get; set; } = 43u;
        public uint GoodHumorSpiritEmoji { get; set; } = 2u;
        public uint NeutralSpiritEmoji { get; set; } = 16u;
        public uint SomewhatAnnoyedSpiritEmoji { get; set; } = 18u;
        public uint MildlyPerturbedSpiritEmoji { get; set; } = 11u;
        public uint VeryDispleasedSpiritEmoji { get; set; } = 14u;
        
        #endregion
        #region Recipe Emoji
        
        public bool ShowNewRecipes { get; set; } = true;
        public uint NewRecipeEmoji { get; set; } = 132u;
        
        public bool ShowExistingRecipes { get; set; } = false;
        public uint KnownRecipeEmoji { get; set; } = 135u;
        
        #endregion
        #region Birthdays
        
        public bool ShowBirthdays { get; set; } = false;
        public bool UseVillagerNames { get; set; } = false;
        public uint BirthdayEmoji { get; set; } = 152u;
        
        #endregion
        #region Multiplayer
        
        public bool SendToOthers { get; set; } = false;
        public bool UseSameForOthers { get; set; } = true;
        public ForecasterConfig? Child { get; set; } = null;
        
        #endregion
        #region Getters
        
        public uint? GetEmoji(WeatherIcons icon) => icon switch {
            WeatherIcons.SUN => this.SunWeatherEmoji,
            WeatherIcons.RAIN => this.RainWeatherEmoji,
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
            _ => 0u
        };
        public uint? GetEmoji(SpiritMoods icon) => icon switch {
            SpiritMoods.VERY_HAPPY => this.VeryHappySpiritEmoji,
            SpiritMoods.GOOD_HUMOR => this.GoodHumorSpiritEmoji,
            SpiritMoods.NEUTRAL => this.NeutralSpiritEmoji,
            SpiritMoods.SOMEWHAT_ANNOYED => this.SomewhatAnnoyedSpiritEmoji,
            SpiritMoods.MILDLY_PERTURBED => this.MildlyPerturbedSpiritEmoji,
            SpiritMoods.VERY_DISPLEASED => this.VeryDispleasedSpiritEmoji,
            _ => 0u
        };
        public uint? GetEmoji(MiscEmoji icon) => icon switch {
            MiscEmoji.SPIRITS => this.SpiritsEmoji,
            MiscEmoji.BIRTHDAY => this.BirthdayEmoji,
            MiscEmoji.NEW_RECIPE => this.NewRecipeEmoji,
            MiscEmoji.KNOWN_RECIPE => this.KnownRecipeEmoji,
            _ => 0u
        };
        
        #endregion
    }
}
