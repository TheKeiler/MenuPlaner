using System.Collections.Generic;
using System.Linq;
using MenuPlanerApp.Core.Model;

namespace MenuPlanerApp.Core.Repository
{
    public class IngredientsRepository
    {
        private static readonly List<Ingredient> AllIngrediants = new List<Ingredient>
        {
            new Ingredient
            {
                IngrediantId = 1, Name = "Apfel", Description = "Green", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = false, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 2, Name = "Hackfleisch", Description = "Fein", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = false,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 3, Name = "Weissbrot", Description = "In Scheiben", ReferenceUnit = "Gramm",
                CompatibleForCeliac = false, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 4, Name = "Milch", Description = "", ReferenceUnit = "liter", CompatibleForCeliac = true,
                CompatibleForFructose = true, CompatibleForHistamin = true, CompatibleForLactose = false
            },
            new Ingredient
            {
                IngrediantId = 5, Name = "Knoblauch", Description = "Zehen", ReferenceUnit = "Stück",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 6, Name = "Petersilie", Description = "", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 7, Name = "Butter", Description = "", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 8, Name = "Senf", Description = "Grobkörnig", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 9, Name = "Paprika", Description = "Edelsüss", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 10, Name = "Salz", Description = "", ReferenceUnit = "Teelöffel",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 11, Name = "Petersilie", Description = "", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 12, Name = "Pfeffer", Description = "", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 13, Name = "Bratbutter", Description = "", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            },
            new Ingredient
            {
                IngrediantId = 14, Name = "Rüebli", Description = "", ReferenceUnit = "Gramm",
                CompatibleForCeliac = true, CompatibleForFructose = true, CompatibleForHistamin = true,
                CompatibleForLactose = true
            }
        };

        public List<Ingredient> GetAllIngredients()
        {
            return AllIngrediants;
        }

        public Ingredient GetIngredientById(int id)
        {
            return AllIngrediants.FirstOrDefault(p => p.IngrediantId == id);
        }
    }
}