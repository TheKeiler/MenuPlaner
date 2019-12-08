namespace MenuPlanerApp.Core.Model
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ReferenceUnit { get; set; }

        public bool CompatibleForFructose { get; set; }

        public bool CompatibleForHistamin { get; set; }

        public bool CompatibleForLactose { get; set; }

        public bool CompatibleForCeliac { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var i = (Ingredient) obj;
            return Name.Equals(i.Name) && Description.Equals(i.Description);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 5 + Name.GetHashCode();
            hash = hash * 5 + Description.GetHashCode();
            return hash;
        }
    }
}