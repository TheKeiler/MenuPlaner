using System.ComponentModel.DataAnnotations;

namespace MenuPlanerApp.API.Model
{
    public class UserOptions
    {
        public int Id { get; set; }

        [Required]
        public bool WantsUserToFilterFructose { get; set; }

        [Required]
        public bool WantsUserToFilterHistamin { get; set; }

        [Required]
        public bool WantsUserToFilterLactose { get; set; }

        [Required]
        public bool WantsUserToFilterCeliac { get; set; }
    }
}