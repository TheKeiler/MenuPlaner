using System.ComponentModel.DataAnnotations;

namespace MenuPlanerApp.API.Model
{
    public class IngredientWithAmount
    {
        public int Id { get; set; }

        [Required]
        public Ingredient Ingredient { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}