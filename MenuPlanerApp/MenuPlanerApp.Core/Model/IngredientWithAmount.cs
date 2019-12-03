namespace MenuPlanerApp.Core.Model
{
    public class IngredientWithAmount
    {
        public int Id { get; set; }

        public Ingredient Ingredient { get; set; }

        public int Amount { get; set; }
    }
}