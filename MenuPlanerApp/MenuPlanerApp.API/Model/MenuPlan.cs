using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuPlanerApp.API.Model
{
    public class MenuPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual ICollection<RecipeWithAmount> RecipesWithAmounts { get; set; }
    }
}