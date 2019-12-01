using System;

namespace MenuPlanerApp.Core.Model
{
    public class RecipeWithAmount
    {
        public Recipe Recipe { get; set; }
        public int NumbersOfMeals { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public MealDayTimeEnum MealDayTime { get; set; }
    }
}