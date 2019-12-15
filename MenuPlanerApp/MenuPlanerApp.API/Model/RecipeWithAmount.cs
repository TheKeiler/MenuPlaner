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
        public virtual Recipe Recipe { get; set; }

        [Required]
        public int NumbersOfMeals { get; set; }

        [Required]
        public virtual DayOfWeek DayOfWeek { get; set; }

        [Required]
        public virtual MealDayTimeEnum MealDayTime { get; set; }

        [ForeignKey("MenuPlanId")]
        public MenuPlan MenuPlan { get; set; }
    }
}