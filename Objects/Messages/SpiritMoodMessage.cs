using System;
using ForecasterText.Objects.Enums;
using StardewModdingAPI;
using StardewValley;

namespace ForecasterText.Objects.Messages {
    public sealed class SpiritMoodMessage : ISourceMessage {
        /// <inheritdoc />
        public string Write(Farmer farmer, ITranslationHelper t9N, ForecasterConfig config) {
            SpiritMoods mood = this.GetSpiritMood(farmer);
            
            if ( // If any of the "Show Luck" options is turned off
                (mood is SpiritMoods.GOOD_HUMOR or SpiritMoods.VERY_HAPPY && !config.ShowGoodLuck)
                || (mood is SpiritMoods.NEUTRAL && !config.ShowNeutralLuck)
                || (mood is SpiritMoods.SOMEWHAT_ANNOYED or SpiritMoods.MILDLY_PERTURBED or SpiritMoods.VERY_DISPLEASED && !config.ShowBadLuck)
            ) return null;
            
            return ISourceMessage.GetDailyLuck(mood)
                .Write(farmer, t9N, config);
        }
        
        private SpiritMoods GetSpiritMood(Farmer who) {
            if (who.team.sharedDailyLuck.Value == -0.12)
                return SpiritMoods.VERY_DISPLEASED; // Furious (TV.cs.13191)
            if (who.DailyLuck == 0.0)
                return SpiritMoods.NEUTRAL; // Neutral (TV.cs.13201)
            if (who.DailyLuck >= -0.07 && who.DailyLuck < -0.02) {
                Random random = new Random((int) Game1.stats.DaysPlayed + (int) Game1.uniqueIDForThisGame / 2);
                if (random.NextDouble() < 0.5)
                    return SpiritMoods.SOMEWHAT_ANNOYED; // Somewhat Annoyed (TV.cs.13193)
                return SpiritMoods.MILDLY_PERTURBED; // Mildly Perturbed (TV.cs.13195)
            }
            if (who.DailyLuck >= -0.07 && who.team.sharedDailyLuck.Value != 0.12) {
                if (who.DailyLuck > 0.07)
                    return SpiritMoods.VERY_HAPPY; // Very Happy (TV.cs.13198)
                if (who.DailyLuck <= 0.02)
                    return SpiritMoods.NEUTRAL; // Neutral (TV.cs.13200)
                return SpiritMoods.GOOD_HUMOR; // Good Humor (TV.cs.13199)
            }
            if (who.DailyLuck >= -0.07)
                return SpiritMoods.GOOD_HUMOR; // Joyous (TV.cs.13197)
            return SpiritMoods.VERY_DISPLEASED; // Very Displeased (TV.cs.13192)
        }
    }
}
