using System.Collections.Generic;

namespace MenuPlanerApp.Core.Model
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<IngredientWithAmount>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<IngredientWithAmount> Ingredients { get; set; }

        public string DirectionPictures { get; set; }

        public bool IsFavorite { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var r = (Recipe) obj;
            return Name.Equals(r.Name) && Description.Equals(r.Description) && Ingredients.Count == r.Ingredients.Count;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 5 + Name.GetHashCode();
            hash = hash * 5 + Description.GetHashCode();
            hash = hash * 5 + Ingredients.Count.GetHashCode();
            return hash;
        }
    }
}