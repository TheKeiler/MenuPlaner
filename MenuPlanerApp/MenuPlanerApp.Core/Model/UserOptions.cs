namespace MenuPlanerApp.Core.Model
{
    public class UserOptions
    {
        public int Id { get; set; }

        public bool WantsUserToFilterFructose { get; set; }

        public bool WantsUserToFilterHistamin { get; set; }

        public bool WantsUserToFilterLactose { get; set; }

        public bool WantsUsertoFilterCeliac { get; set; }
    }
}