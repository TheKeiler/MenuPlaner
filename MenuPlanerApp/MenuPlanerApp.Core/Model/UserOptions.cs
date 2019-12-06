namespace MenuPlanerApp.Core.Model
{
    public class UserOptions
    {
        public UserOptions()
        {
            WantsUserToSeeRecipesWithFructose = true;
            WantsUserToSeeRecipesWithHistamin = true;
            WantsUserToSeeRecipesWithLactose = true;
            WantsUserToSeeRecipesWithCeliac = true;
        }

        public int Id { get; set; }

        public bool WantsUserToSeeRecipesWithFructose { get; set; }

        public bool WantsUserToSeeRecipesWithHistamin { get; set; }

        public bool WantsUserToSeeRecipesWithLactose { get; set; }

        public bool WantsUserToSeeRecipesWithCeliac { get; set; }
    }
}