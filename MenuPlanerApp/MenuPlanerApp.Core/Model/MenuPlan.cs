using System;
using System.Collections.Generic;

namespace MenuPlanerApp.Core.Model
{
    public class MenuPlan
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public List<RecipeWithAmount> Recipes { get; set; }

        public MenuPlan()
        {
            StartDate = new DateTime();
            Recipes = new List<RecipeWithAmount>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var m = (MenuPlan) obj;
            return StartDate.Equals(m.StartDate) && Recipes.Count == m.Recipes.Count;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 5 + StartDate.GetHashCode();
            hash = hash * 5 + Recipes.Count.GetHashCode();
            return hash;
        }
    }
}