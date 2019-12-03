using System.Collections.Generic;

namespace MenuPlanerApp.Core.Model
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<IngredientWithAmount> Ingredients { get; set; }

        public byte[] DirectionPictures { get; set; }

        public bool IsFavorite { get; set; }
    }
}