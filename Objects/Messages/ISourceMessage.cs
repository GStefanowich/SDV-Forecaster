using System.Collections.Generic;
using ForecasterText.Objects.Enums;
using StardewModdingAPI;
using StardewValley;

namespace ForecasterText.Objects.Messages {
    internal interface ISourceMessage {
        string Write(Farmer farmer, ITranslationHelper t9N, ForecasterConfig config);
        string Write(ForecasterConfig config, ITranslationHelper t9N)
            => this.Write(Game1.player, t9N, config);
        
        public static MessageSource GetDailyLuck(SpiritMoods mood)
            => MessageSource.TV(new MessageBuilder("tv.spirits")
                .AddEmoji("spirit", MiscEmoji.SPIRITS)
                .AddEmoji("mood", mood));
        
        public static MessageSource GetQueenOfSauce(string recipe, bool hasRecipe)
            => MessageSource.TV(new MessageBuilder("tv.recipe")
                .AddEmoji("icon", hasRecipe ? MiscEmoji.KNOWN_RECIPE : MiscEmoji.NEW_RECIPE)
                .AddTranslation("recipe", $@"Data\CookingRecipes:{recipe}", content => content?.Split('/') is {Length: >=5} split ? split[4] : recipe));
        
        public static MessageSource GetBirthdays(IEnumerable<object> characters, ForecasterConfig config) {
            MessageBuilder builder = null;
            
            foreach (object obj in characters) {
                // Create the build if it doesn't exist
                builder ??= new MessageBuilder("tv.birthday")
                    .AddEmoji("icon", MiscEmoji.BIRTHDAY);
                
                if (!config.UseVillagerNames)
                    _ = obj is Character character ? builder.AddEmoji("...", character) : builder.AddNpcEmoji("...", obj as string);
                else {
                    builder.PadText("...", ' ') // Add a space between names
                        .AddText("...", obj as string);
                }
            }
            
            return MessageSource.Calendar(builder);
        }
    }
}
