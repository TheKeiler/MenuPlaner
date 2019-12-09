using MenuPlanerApp.Core.Model;

namespace MenuPlanerApp.Core.VerifyData
{
    public class VerifyUserEntries
    {
        public bool IsIngredientComplete(Ingredient ingredient)
        {
            if (string.IsNullOrEmpty(ingredient.Name))
                return false;
            if (string.IsNullOrEmpty(ingredient.ReferenceUnit))
                return false;
            return true;
        }

        public bool IsRecipeComplete(Recipe recipe)
        {
            if (string.IsNullOrEmpty(recipe.Name))
                return false;
            if (recipe.Ingredients.Count == 0)
                return false;
            if (recipe.DirectionPictures.Length == 0)
                return false;
            return true;
        }

        public bool IsIngredientWithAmountComplete(IngredientWithAmount ingredientWithAmount)
        {
            if (string.IsNullOrEmpty(ingredientWithAmount.Ingredient.Name))
                return false;
            if (ingredientWithAmount.Amount == 0)
                return false;
            return true;
        }
    }
}