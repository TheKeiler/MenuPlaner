using System;
using System.Collections.Generic;

namespace MenuPlanerApp.Core.Model
{
    public class MenuPlan
    {
        public MenuPlan()
        {
            StartDate = new DateTime();
            RecipesWithAmounts = new List<RecipeWithAmount>();
        }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public List<RecipeWithAmount> RecipesWithAmounts { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var m = (MenuPlan) obj;
            return StartDate.Equals(m.StartDate) && RecipesWithAmounts.Count == m.RecipesWithAmounts.Count;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 5 + StartDate.GetHashCode();
            hash = hash * 5 + RecipesWithAmounts.Count.GetHashCode();
            return hash;
        }
    }
}