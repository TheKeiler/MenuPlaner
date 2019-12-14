using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenuPlanerApp.API.Model
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public List<IngredientWithAmount> Ingredients { get; set; }

        [Required]
        public string DirectionPictures { get; set; }

        public bool IsFavorite { get; set; }
    }
}