using System;

namespace MenuPlanerApp.Core.Model
{
    public class RecipeWithAmount
    {
        public RecipeWithAmount()
        {
            Recipe = new Recipe();
            DayOfWeek = new DayOfWeek();
            MealDayTime = new MealDayTimeEnum();
        }

        public int Id { get; set; }

        public Recipe Recipe { get; set; }

        public int NumbersOfMeals { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public MealDayTimeEnum MealDayTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var r = (RecipeWithAmount) obj;
            return Recipe.Equals(r.Recipe) && NumbersOfMeals == r.NumbersOfMeals && DayOfWeek.Equals(r.DayOfWeek) &&
                   MealDayTime.Equals(r.MealDayTime);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 5 + Recipe.GetHashCode();
            hash = hash * 5 + NumbersOfMeals.GetHashCode();
            hash = hash * 5 + DayOfWeek.GetHashCode();
            hash = hash * 5 + MealDayTime.GetHashCode();
            return hash;
        }
    }
}