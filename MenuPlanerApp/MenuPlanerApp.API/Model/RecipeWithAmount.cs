using System;
using System.ComponentModel.DataAnnotations;

namespace MenuPlanerApp.API.Model
{
    public class RecipeWithAmount
    {
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