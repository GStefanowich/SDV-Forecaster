using System.Linq;
using StardewModdingAPI;
using StardewValley;

namespace ForecasterText.Objects.Messages {
    public sealed class BirthdaysMessage : ISourceMessage {
        /// <inheritdoc />
        public string Write(Farmer farmer, ITranslationHelper t9N, ForecasterConfig config) {
            // If not showing birthdays
            if (!config.ShowBirthdays)
                return null;
            
            // Get a list of todays birthdays
            MessageSource birthdays = ISourceMessage.GetBirthdays(farmer.friendshipData.FieldDict.Keys
                .Select(name => Game1.getCharacterFromName(name, true))
                .Where(npc => npc is not null && npc.isBirthday(Game1.currentSeason, Game1.dayOfMonth)), config);
            
            return birthdays is null ? null : birthdays.Write(farmer, t9N, config);
        }
    }
}
