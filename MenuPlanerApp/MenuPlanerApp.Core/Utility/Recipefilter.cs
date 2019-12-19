using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanerApp.Core.Model;
using MenuPlanerApp.Core.Repository;

namespace MenuPlanerApp.Core.Utility
{
    public class RecipeFilter
    {

        public async Task<List<Recipe>> FilterRecipes(List<Recipe> recipesList)
        {
            if (recipesList.Count == 0) return recipesList;
            var userOptionRepo = new UserOptionsRepositoryWeb();
            var userOptions = await userOptionRepo.GetOptionById(1);
            if (userOptions == null) return recipesList;
            var filteredList = FilterRecipesAccordingToOptions(recipesList, userOptions);
            return filteredList;
        }

        private List<Recipe> FilterRecipesAccordingToOptions(List<Recipe> recipeList, UserOptions userOptions)
        {
            if (userOptions.WantsUserToSeeRecipesWithCeliac && userOptions.WantsUserToSeeRecipesWithFructose && userOptions.WantsUserToSeeRecipesWithHistamin && userOptions.WantsUserToSeeRecipesWithLactose)
            {
                return recipeList;
            }

            var recipes = new List<Recipe>(recipeList);

            if (!userOptions.WantsUserToSeeRecipesWithCeliac)
            {
                recipes = FilterCeliacRecipes(recipes);
            }

            if (!userOptions.WantsUserToSeeRecipesWithFructose)
            {
                recipes = FilterFructoseRecipes(recipes);
            }

            if (!userOptions.WantsUserToSeeRecipesWithHistamin)
            {
                recipes = FilterHistaminRecipes(recipes);
            }

            if (!userOptions.WantsUserToSeeRecipesWithLactose)
            {
                recipes = FilterLactoseRecipes(recipes);
            }

            return recipes;
        }


        private List<Recipe> FilterCeliacRecipes(List<Recipe> recipes)
        {
            var rec = new List<Recipe>(recipes);

            foreach (var recipe in recipes)
            {
                var recipeContainsCeliac = false;

                foreach (var ingr in recipe.Ingredients.Where(ingr => !ingr.Ingredient.CompatibleForCeliac))
                {
                    recipeContainsCeliac = true;
                }

                if (recipeContainsCeliac)
                {
                    rec.Remove(recipe);
                }
            }
            return rec;
        }

        private List<Recipe> FilterFructoseRecipes(List<Recipe> recipes)
        {
            var rec = new List<Recipe>(recipes);
            
            foreach (var recipe in recipes)
            {
                var recipeContainsFructose = false;

                foreach (var ingr in recipe.Ingredients.Where(ingr => !ingr.Ingredient.CompatibleForFructose))
                {
                    recipeContainsFructose = true;
                }

                if (recipeContainsFructose)
                {
                    rec.Remove(recipe);
                }
            }
            return rec;
        }

        private List<Recipe> FilterHistaminRecipes(List<Recipe> recipes)
        {
            var rec = new List<Recipe>(recipes);

            foreach (var recipe in recipes)
            {
                var recipeContainsHistamin = false;

                foreach (var ingr in recipe.Ingredients.Where(ingr => !ingr.Ingredient.CompatibleForHistamin))
                {
                    recipeContainsHistamin = true;
                }

                if (recipeContainsHistamin)
                {
                    rec.Remove(recipe);
                }
            }
            return rec;
        }

        private List<Recipe> FilterLactoseRecipes(List<Recipe> recipes)
        {
            var rec = new List<Recipe>(recipes);

            foreach (var recipe in recipes)
            {
                var recipeContainsLactose = false;

                foreach (var ingr in recipe.Ingredients.Where(ingr => !ingr.Ingredient.CompatibleForLactose))
                {
                    recipeContainsLactose = true;
                }

                if (recipeContainsLactose)
                {
                    rec.Remove(recipe);
                }
            }
            return rec;
        }

    }
}
