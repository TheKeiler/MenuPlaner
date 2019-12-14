using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuPlanerApp.API.Model
{
    public class RecipeWithAmount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Recipe Recipe { get; set; }

        [Required]
        public int NumbersOfMeals { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public MealDayTimeEnum MealDayTime { get; set; }
    }
}