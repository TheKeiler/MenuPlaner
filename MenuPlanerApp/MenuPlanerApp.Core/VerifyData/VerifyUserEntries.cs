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
    }
}