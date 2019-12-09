namespace MenuPlanerApp.Core.Model
{
    public class IngredientWithAmount
    {
        public int Id { get; set; }

        public Ingredient Ingredient { get; set; }

        public decimal Amount { get; set; }

        public IngredientWithAmount()
        {
            Ingredient = new Ingredient();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var i = (IngredientWithAmount) obj;
            return Ingredient.Equals(i.Ingredient) && Amount == i.Amount;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 5 + Ingredient.GetHashCode();
            hash = hash * 5 + Amount.GetHashCode();
            return hash;
        }
    }
}