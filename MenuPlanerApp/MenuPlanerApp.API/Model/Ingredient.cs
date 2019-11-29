namespace MenuPlanerApp.API.Model
{
    public class Ingredient
    {
        public int IngrediantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReferenceUnit { get; set; }
        public bool CompatibleForFructose { get; set; }
        public bool CompatibleForHistamin { get; set; }
        public bool CompatibleForLactose { get; set; }
        public bool CompatibleForCeliac { get; set; }
    }
}