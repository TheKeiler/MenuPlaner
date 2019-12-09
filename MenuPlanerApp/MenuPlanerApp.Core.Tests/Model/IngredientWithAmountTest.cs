using MenuPlanerApp.Core.Model;
using NUnit.Framework;

namespace MenuPlanerApp.Core.Tests.Model
{
    [TestFixture]
    internal class IngredientWithAmountTest
    {
        [Test]
        public void EqualsWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var ingredientWithAmount1 = new IngredientWithAmount();
            var ingredientWithAmount2 = new IngredientWithAmount();

            var ingredient1 = new Ingredient();
            ingredient1.Name = "bla";
            ingredient1.Description = "test";
            ingredient1.ReferenceUnit = "gramm";

            ingredientWithAmount1.Ingredient = ingredient1;
            ingredientWithAmount1.Amount = 264.6m;

            ingredientWithAmount2.Ingredient = ingredient1;
            ingredientWithAmount2.Amount = 287.5m;

            //Act
            var equal = ingredientWithAmount1.Equals(ingredientWithAmount2);

            //Assert
            Assert.That(equal, Is.False);
        }

        [Test]
        public void EqualsWithTwoDifferentObjectsShouldBeTrue()
        {
            //Arrange
            var ingredientWithAmount1 = new IngredientWithAmount();
            var ingredientWithAmount2 = new IngredientWithAmount();

            var ingredient1 = new Ingredient();
            ingredient1.Name = "bla";
            ingredient1.Description = "test";
            ingredient1.ReferenceUnit = "gramm";

            ingredientWithAmount1.Ingredient = ingredient1;
            ingredientWithAmount1.Amount = 264.6m;

            ingredientWithAmount2.Ingredient = ingredient1;
            ingredientWithAmount2.Amount = 264.6m;

            //Act
            var equal = ingredientWithAmount1.Equals(ingredientWithAmount2);

            //Assert
            Assert.That(equal, Is.True);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var ingredientWithAmount1 = new IngredientWithAmount();
            var ingredientWithAmount2 = new IngredientWithAmount();

            var ingredient1 = new Ingredient();
            ingredient1.Name = "bla";
            ingredient1.Description = "test";
            ingredient1.ReferenceUnit = "gramm";

            ingredientWithAmount1.Ingredient = ingredient1;
            ingredientWithAmount1.Amount = 264.6m;

            ingredientWithAmount2.Ingredient = ingredient1;
            ingredientWithAmount2.Amount = 264.5m;

            //Act
            var hash1 = ingredientWithAmount1.GetHashCode();
            var hash2 = ingredientWithAmount2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.False);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeTrue()
        {
            //Arrange
            var ingredientWithAmount1 = new IngredientWithAmount();
            var ingredientWithAmount2 = new IngredientWithAmount();

            var ingredient1 = new Ingredient();
            ingredient1.Name = "bla";
            ingredient1.Description = "test";
            ingredient1.ReferenceUnit = "gramm";

            ingredientWithAmount1.Ingredient = ingredient1;
            ingredientWithAmount1.Amount = 264.6m;

            ingredientWithAmount2.Ingredient = ingredient1;
            ingredientWithAmount2.Amount = 264.6m;

            //Act
            var hash1 = ingredientWithAmount1.GetHashCode();
            var hash2 = ingredientWithAmount2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.True);
        }
    }
}