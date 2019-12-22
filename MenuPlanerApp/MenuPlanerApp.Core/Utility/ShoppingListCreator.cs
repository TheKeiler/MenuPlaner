using System.Collections.Generic;
using System.Linq;
using MenuPlanerApp.Core.Model;

namespace MenuPlanerApp.Core.Utility
{
    public class ShoppingListCreator
    {
        public List<IngredientWithAmount> GenerateShoppingListFromMenuPlan(MenuPlan menuPlan)
        { 
            var shoppingList = new List<IngredientWithAmount>();

            foreach (var recipe in menuPlan.RecipesWithAmounts)
            {
                var multiplier = recipe.NumbersOfMeals;

                foreach (var ingredientWithAmount in recipe.Recipe.Ingredients)
                {
                    ingredientWithAmount.Amount *= multiplier;
                    shoppingList.Add(ingredientWithAmount);
                }
            }

            var summarizedList = SummarizeShoppingList(shoppingList);
            return summarizedList;
        }

        private static List<IngredientWithAmount> SummarizeShoppingList(IEnumerable<IngredientWithAmount> shoppingList)
        {
            var summarizedList = new List<IngredientWithAmount>();
            foreach (var ingredientWithAmount in shoppingList)
            {
                if (summarizedList.Count == 0)
                {
                    summarizedList.Add(ingredientWithAmount);
                }

                var findSameIngredients = summarizedList.Find(i => i.Ingredient.Equals(ingredientWithAmount.Ingredient));

                if (findSameIngredients != null)
                {
                    findSameIngredients.Amount += ingredientWithAmount.Amount;
                }
                else
                {
                    summarizedList.Add(ingredientWithAmount);
                }
            }

            return summarizedList;
        }
    }
}
