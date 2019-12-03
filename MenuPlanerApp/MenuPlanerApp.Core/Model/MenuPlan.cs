using System;
using System.Collections.Generic;

namespace MenuPlanerApp.Core.Model
{
    public class MenuPlan
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public List<RecipeWithAmount> Recipes { get; set; }
    }
}