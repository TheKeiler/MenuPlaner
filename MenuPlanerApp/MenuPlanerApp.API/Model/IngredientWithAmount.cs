using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuPlanerApp.API.Model
{
    public class IngredientWithAmount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Ingredient Ingredient { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
    }
}