using System;
using MenuPlanerApp.Core.Model;

namespace MenuPlanerApp.Core.VerifyData
{
    public class VerifyUserEntries
    {
        public static bool IsIngredientComplete(Ingredient ingredient)
        {
            if (string.IsNullOrEmpty(ingredient.Name))
                return false;
            if (string.IsNullOrEmpty(ingredient.ReferenceUnit))
                return false;
            return true;
        }

        public static bool IsRecipeComplete(Recipe recipe)
        {
            if (string.IsNullOrEmpty(recipe.Name))
                return false;
            if (recipe.Ingredients.Count == 0)
                return false;
            return !string.IsNullOrEmpty(recipe.DirectionPictures);
        }

        public static bool IsIngredientWithAmountComplete(IngredientWithAmount ingredientWithAmount)
        {
            if (string.IsNullOrEmpty(ingredientWithAmount.Ingredient.Name))
                return false;
            return ingredientWithAmount.Amount != 0;
        }

        public static bool IsMenuPlanComplete(MenuPlan menuPlan)
        {
            if (menuPlan.RecipesWithAmounts.Count == 0)
                return false;
            return !menuPlan.StartDate.Equals(new DateTime());
        }
    }
}