using StardewModdingAPI;
using StardewValley;

namespace ForecasterText.Objects.Messages {
    public sealed class QueenOfSauceMessage : ISourceMessage {
        private readonly ModEntry Mod;
        
        public QueenOfSauceMessage(ModEntry mod) {
            this.Mod = mod;
        }
        
        /// <inheritdoc />
        public string Write(Farmer farmer, ITranslationHelper t9N, ForecasterConfig config) {
            IRecipeFinder recipeFinder = this.Mod.GetRecipeFinder(farmer);
            
            // Get the recipe name
            if (recipeFinder.GetAnyRecipe() is not string recipeName)
                return null;
            
            bool hasRecipe = this.PlayerHasRecipe(farmer, recipeName);
            if ((hasRecipe && !config.ShowExistingRecipes) || (!hasRecipe && !config.ShowNewRecipes))
                return null;
            
            return ISourceMessage.GetQueenOfSauce(recipeName, hasRecipe)
                .Write(farmer, t9N, config);
        }
        
        /// <summary>Check if a farmer knows a recipe</summary>
        public bool PlayerHasRecipe(Farmer farmer, string recipe)
            => farmer.cookingRecipes.ContainsKey(recipe);
    }
}
