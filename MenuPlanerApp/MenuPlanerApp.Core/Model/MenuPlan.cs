using System.Collections.Generic;
using Java.Sql;

namespace MenuPlanerApp.Core.Model
{
    public class MenuPlan
    {
        public int RecipeId { get; set; }
        public Date StartDate { get; set; }
        public List<RecipeWithAmount> Recipes { get; set; }
    }
}