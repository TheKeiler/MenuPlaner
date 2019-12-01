using System.Collections.Generic;
using Android.Graphics;

namespace MenuPlanerApp.Core.Model
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IngredientWithAmount> Ingredients { get; set; }
        public List<Bitmap> DirectionPictures { get; set; }
        public bool IsFavorite { get; set; }
    }
}