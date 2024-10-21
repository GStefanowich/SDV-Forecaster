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

using ForecasterText.Objects.Addons;
using ForecasterText.Objects.Enums;
using ForecasterText.Constants;
using StardewModdingAPI;
using StardewValley;

namespace ForecasterText.Objects.Messages {
    public abstract class WeatherMessage : ISourceMessage {
        protected abstract string T9N { get; }
        
        protected WeatherMessage() {}
        
        protected abstract string GetWeather();
        protected abstract WeatherDisplay GetDisplay(ForecasterConfig config);
        protected virtual bool ShouldDisplayFor(Farmer farmer) => true;
        
        /// <inheritdoc />
        public string? Write(Farmer farmer, ITranslationHelper t9N, ForecasterConfig config) {
            if (!this.ShouldDisplayFor(farmer))
                return null;
            
            WeatherDisplay display = this.GetDisplay(config);
            string weather = this.GetWeather();
            
            if (!this.ShowWeather(display, weather))
                return null;
            
            WeatherIcons? emoji = weather switch {
                Game1.weather_sunny => WeatherIcons.SUN,
                Game1.weather_festival => WeatherIcons.FESTIVAL,
                Game1.weather_wedding => WeatherIcons.WEDDING,
                Game1.weather_rain => WeatherIcons.RAIN,
                Game1.weather_green_rain => WeatherIcons.GREEN_RAIN,
                Game1.weather_debris when Game1.currentSeason.Equals("winter") => WeatherIcons.SNOW,
                Game1.weather_debris => WeatherIcons.SUN,
                Game1.weather_lightning => WeatherIcons.LIGHTNING,
                Game1.weather_snow => WeatherIcons.SNOW,
                // WeatherWonders Weathers
                WeatherWondersIds.acid_rain => WeatherIcons.ACID_RAIN,
                WeatherWondersIds.blizzard => WeatherIcons.BLIZZARD,
                WeatherWondersIds.cloudy => WeatherIcons.CLOUDY,
                WeatherWondersIds.deluge => WeatherIcons.DILUGE,
                WeatherWondersIds.drizzle => WeatherIcons.DRIZZLE,
                WeatherWondersIds.dry_lightning => WeatherIcons.DRY_LIGHTNING,
                WeatherWondersIds.hailstorm => WeatherIcons.HAILSTORM,
                WeatherWondersIds.heatwave => WeatherIcons.HEATWAVE,
                WeatherWondersIds.mist => WeatherIcons.MIST,
                WeatherWondersIds.muddy_rain => WeatherIcons.MUDDY_RAIN,
                WeatherWondersIds.snow_rain_mix => WeatherIcons.SNOW_RAIN_MIX,
                WeatherWondersIds.sandstorm => WeatherIcons.SANDSTORM,
                WeatherWondersIds.blood_moon => WeatherIcons.BLOOD_MOON,
                WeatherWondersIds.blue_moon => WeatherIcons.BLUE_MOON,
                WeatherWondersIds.harvest_moon => WeatherIcons.HARVEST_MOON,
                _ => null
            };
            
            // If no icons, or no results
            if (emoji is null)
                return null;
            
            MessageBuilder builder = new MessageBuilder(this.T9N)
                .AddEmoji("...", emoji.Value);
            
            return MessageSource.TV(builder)
                .Write(farmer, t9N, config);
        }
        
        protected virtual bool ShowWeather(WeatherDisplay display, string weather) {
            return display switch {
                WeatherDisplay.NEVER => false,
                WeatherDisplay.ALWAYS => true,
                _ => weather switch {
                    Game1.weather_sunny => display is WeatherDisplay.NOT_RAINING,
                    Game1.weather_festival => display is WeatherDisplay.NOT_RAINING,
                    Game1.weather_wedding => display is WeatherDisplay.NOT_RAINING,
                    Game1.weather_rain => display is WeatherDisplay.RAINING,
                    Game1.weather_debris => display is WeatherDisplay.NOT_RAINING,
                    Game1.weather_lightning => display is WeatherDisplay.RAINING,
                    Game1.weather_snow => display is WeatherDisplay.NOT_RAINING,
                    Game1.weather_green_rain => display is WeatherDisplay.RAINING,
                    // WeatherWonders Weathers
                    WeatherWondersIds.acid_rain => display is WeatherDisplay.RAINING,
                    WeatherWondersIds.blizzard => display is WeatherDisplay.RAINING,
                    WeatherWondersIds.cloudy => display is WeatherDisplay.NOT_RAINING,
                    WeatherWondersIds.deluge => display is WeatherDisplay.RAINING,
                    WeatherWondersIds.drizzle => display is WeatherDisplay.RAINING,
                    WeatherWondersIds.dry_lightning => display is WeatherDisplay.NOT_RAINING,
                    WeatherWondersIds.hailstorm => display is WeatherDisplay.RAINING,
                    WeatherWondersIds.heatwave => display is WeatherDisplay.NOT_RAINING,
                    WeatherWondersIds.mist => display is WeatherDisplay.NOT_RAINING,
                    WeatherWondersIds.muddy_rain => display is WeatherDisplay.RAINING,
                    WeatherWondersIds.snow_rain_mix => display is WeatherDisplay.RAINING,
                    WeatherWondersIds.sandstorm => display is WeatherDisplay.RAINING,
                    // WeatherWonders Night Events
                    WeatherWondersIds.blood_moon => display is WeatherDisplay.NOT_RAINING,
                    WeatherWondersIds.blue_moon => display is WeatherDisplay.NOT_RAINING,
                    WeatherWondersIds.harvest_moon => display is WeatherDisplay.NOT_RAINING,
                    _ => false
                }
            };
        }
        
        public class TestDisplay : PelicanTown {
            private readonly WeatherIcons Weather;
            
            public TestDisplay(WeatherIcons weather): base() {
                this.Weather = weather;
            }
            
            /// <inheritdoc />
            protected override bool ShouldDisplayFor(Farmer farmer)
                => true;
            
            /// <inheritdoc />
            protected override string GetWeather()
                => this.Weather.GetString();
            
            /// <inheritdoc />
            protected override bool ShowWeather(WeatherDisplay display, string weather)
                => true;
        }
        public class PelicanTown : WeatherMessage {
            protected override string T9N => "tv.weather.pelican_town";
            
            public PelicanTown(): base() {}
            
            /// <inheritdoc />
            protected override string GetWeather() {
                WorldDate date = new(Game1.Date); // Copy the date so we don't modify the origin
                ++date.TotalDays; // Increase by one to get the next days weather
                return !Game1.IsMasterGame ? Game1.getWeatherModificationsForDate(date, Game1.netWorldState.Value.WeatherForTomorrow) : Game1.getWeatherModificationsForDate(date, Game1.weatherForTomorrow);
            }
            
            /// <inheritdoc />
            protected override WeatherDisplay GetDisplay(ForecasterConfig config)
                => config.StardewValleyWeather;
        }
        public class GingerIsland : WeatherMessage {
            protected override string T9N => "tv.weather.ginger_island";
            
            public GingerIsland(): base() {}
            
            /// <inheritdoc />
            protected override string GetWeather() {
                return Game1.netWorldState.Value.GetWeatherForLocation(
                    Game1.getLocationFromName("IslandSouth").GetLocationContextId()
                ).weatherForTomorrow.Value;
            }
            
            /// <inheritdoc />
            protected override WeatherDisplay GetDisplay(ForecasterConfig config)
                => config.GingerIslandWeather;
            
            /// <inheritdoc />
            protected override bool ShouldDisplayFor(Farmer farmer)
                => farmer.hasOrWillReceiveMail("Visited_Island");
        }
    }
}
