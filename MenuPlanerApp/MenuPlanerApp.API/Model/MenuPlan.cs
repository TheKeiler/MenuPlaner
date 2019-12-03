using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenuPlanerApp.API.Model
{
    public class MenuPlan
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        public List<RecipeWithAmount> Recipes { get; set; }
    }
}