
namespace MenuPlanerApp.Core.Model
{
    public class UserOptions
    {
        public int Id { get; set; }

        public bool WantsUserToSeeRecipesWithFructose { get; set; }

        public bool WantsUserToSeeRecipesWithHistamin { get; set; }

        public bool WantsUserToSeeRecipesWithLactose { get; set; }

        public bool WantsUserToSeeRecipesWithCeliac { get; set; }

        public UserOptions()
        {
            this.WantsUserToSeeRecipesWithFructose = true;
            this.WantsUserToSeeRecipesWithHistamin = true;
            this.WantsUserToSeeRecipesWithLactose = true;
            this.WantsUserToSeeRecipesWithCeliac = true;
        }
    }


}