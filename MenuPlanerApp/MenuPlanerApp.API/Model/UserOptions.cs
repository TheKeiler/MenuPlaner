using System.ComponentModel.DataAnnotations;

namespace MenuPlanerApp.API.Model
{
    public class UserOptions
    {
        public int Id { get; set; }

        [Required]
        public bool WantsUserToSeeRecipesWithFructose { get; set; }

        [Required]
        public bool WantsUserToSeeRecipesWithHistamin { get; set; }

        [Required]
        public bool WantsUserToSeeRecipesWithLactose { get; set; }

        [Required]
        public bool WantsUserToSeeRecipesWithCeliac { get; set; }
    }
}