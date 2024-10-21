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
using ForecasterText.Objects.Enums;
using StardewValley;
using ForecasterText.Constants;

namespace ForecasterText.Objects.Addons {
    public static class WeatherIconsExt {
        public static string GetString(this WeatherIcons icon) => icon switch {
            WeatherIcons.SUN => Game1.weather_sunny,
            WeatherIcons.RAIN => Game1.weather_rain,
            WeatherIcons.GREEN_RAIN => Game1.weather_green_rain,
            WeatherIcons.LIGHTNING => Game1.weather_lightning,
            WeatherIcons.FESTIVAL => Game1.weather_festival,
            WeatherIcons.SNOW => Game1.weather_snow,
            WeatherIcons.WEDDING => Game1.weather_wedding,
            // WeatherWonders Weathers
            WeatherIcons.ACID_RAIN => WeatherWondersIds.acid_rain,
            WeatherIcons.BLIZZARD => WeatherWondersIds.blizzard,
            WeatherIcons.CLOUDY => WeatherWondersIds.cloudy,
            WeatherIcons.DILUGE => WeatherWondersIds.deluge,
            WeatherIcons.DRIZZLE => WeatherWondersIds.drizzle,
            WeatherIcons.DRY_LIGHTNING => WeatherWondersIds.dry_lightning,
            WeatherIcons.HAILSTORM => WeatherWondersIds.hailstorm,
            WeatherIcons.HEATWAVE => WeatherWondersIds.heatwave ,
            WeatherIcons.MIST => WeatherWondersIds.mist,
            WeatherIcons.MUDDY_RAIN => WeatherWondersIds.muddy_rain,
            WeatherIcons.SNOW_RAIN_MIX => WeatherWondersIds.snow_rain_mix,
            WeatherIcons.SANDSTORM => WeatherWondersIds.sandstorm,
            // WeatherWonders Night Events
            WeatherIcons.MOON => WeatherWondersIds.moon,
            WeatherIcons.BLOOD_MOON => WeatherWondersIds.blood_moon,
            WeatherIcons.BLUE_MOON => WeatherWondersIds.blue_moon,
            WeatherIcons.HARVEST_MOON => WeatherWondersIds.harvest_moon,
            _ => throw new ArgumentOutOfRangeException(nameof(icon), icon, null)
        };
    }
}
