using ForecasterText.Objects.Enums;

namespace ForecasterText {
    public sealed class ForecasterConfig {
        public WeatherDisplay StardewValleyWeather { get; set; } = WeatherDisplay.ALWAYS;
        public WeatherDisplay GingerIslandWeather { get; set; } = WeatherDisplay.ALWAYS;
        
        public bool ShowGoodLuck { get; set; } = true;
        public bool ShowNeutralLuck { get; set; } = true;
        public bool ShowBadLuck { get; set; } = true;
        
        public bool ShowNewRecipes { get; set; } = true;
        public bool ShowExistingRecipes { get; set; } = false;
        
        public uint SpiritsEmoji { get; set; } = 119u;
        public uint VeryHappySpiritEmoji { get; set; } = 43u;
        public uint GoodHumorSpiritEmoji { get; set; } = 2u;
        public uint NeutralSpiritEmoji { get; set; } = 16u;
        public uint SomewhatAnnoyedSpiritEmoji { get; set; } = 18u;
        public uint MildlyPerturbedSpiritEmoji { get; set; } = 11u;
        public uint VeryDispleasedSpiritEmoji { get; set; } = 14u;
        
        public uint NewRecipeEmoji { get; set; } = 132u;
        public uint KnownRecipeEmoji { get; set; } = 135u;
        
        public uint SunWeatherEmoji { get; set; } = 99u;
        public uint RainWeatherEmoji { get; set; } = 100u;
        public uint ThunderWeatherEmoji { get; set; } = 102u;
        public uint SnowWeatherEmoji { get; set; } = 103u;
        public uint FestivalWeatherEmoji { get; set; } = 151u;
        public uint WeddingWeatherEmoji { get; set; } = 46u;
    }
}
