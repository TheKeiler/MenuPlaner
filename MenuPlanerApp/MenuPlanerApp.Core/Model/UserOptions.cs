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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var u = (UserOptions) obj;
            return WantsUserToSeeRecipesWithFructose == u.WantsUserToSeeRecipesWithFructose &&
                   WantsUserToSeeRecipesWithHistamin == u.WantsUserToSeeRecipesWithHistamin &&
                   WantsUserToSeeRecipesWithLactose == u.WantsUserToSeeRecipesWithLactose &&
                   WantsUserToSeeRecipesWithCeliac == u.WantsUserToSeeRecipesWithCeliac;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 5 + WantsUserToSeeRecipesWithFructose.GetHashCode();
            hash = hash * 5 + WantsUserToSeeRecipesWithHistamin.GetHashCode();
            hash = hash * 5 + WantsUserToSeeRecipesWithLactose.GetHashCode();
            hash = hash * 5 + WantsUserToSeeRecipesWithCeliac.GetHashCode();
            return hash;
        }
    }
}