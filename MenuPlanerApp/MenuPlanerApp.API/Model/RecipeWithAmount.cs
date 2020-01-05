using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuPlanerApp.API.Model
{
    public class RecipeWithAmount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }

        [Required]
        public int NumbersOfMeals { get; set; }

        [Required]
        public int DayOfWeek { get; set; }

        [Required]
        public int MealDayTime { get; set; }
    }
}