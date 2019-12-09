using MenuPlanerApp.Core.Model;
using NUnit.Framework;

namespace MenuPlanerApp.Core.Tests.Model
{
    [TestFixture]
    internal class RecipeTest
    {
        [Test]
        public void EqualsWithTwoDiffrentObjectsShouldBeFalse()
        {
            //Arrange
            var recipe1 = new Recipe();
            var recipe2 = new Recipe();

            recipe1.Name = "Test";
            recipe1.Description = "frisch";
            recipe1.Ingredients.Add(new IngredientWithAmount());
            recipe1.Ingredients.Add(new IngredientWithAmount());

            recipe2.Name = "Test2";
            recipe2.Description = "frisch";
            recipe2.Ingredients.Add(new IngredientWithAmount());
            recipe2.Ingredients.Add(new IngredientWithAmount());

            //Act
            var equal = recipe1.Equals(recipe2);
            //Assert
            Assert.That(equal, Is.False);
        }

        [Test]
        public void EqualsWithTwoSameObjectsShouldBeTrue()
        {
            //Arrange
            var recipe1 = new Recipe();
            var recipe2 = new Recipe();

            recipe1.Name = "Test";
            recipe1.Description = "frisch";
            recipe1.Ingredients.Add(new IngredientWithAmount());
            recipe1.Ingredients.Add(new IngredientWithAmount());

            recipe2.Name = "Test";
            recipe2.Description = "frisch";
            recipe2.Ingredients.Add(new IngredientWithAmount());
            recipe2.Ingredients.Add(new IngredientWithAmount());

            //Act
            var equal = recipe1.Equals(recipe2);
            //Assert
            Assert.That(equal, Is.True);
        }

        [Test]
        public void GetHashWithTwoDiffrentObjectsShouldBeFalse()
        {
            //Arrange
            var recipe1 = new Recipe();
            var recipe2 = new Recipe();

            recipe1.Name = "Test";
            recipe1.Description = "frisch";
            recipe1.Ingredients.Add(new IngredientWithAmount());
            recipe1.Ingredients.Add(new IngredientWithAmount());

            recipe2.Name = "Test2";
            recipe2.Description = "frisch";
            recipe2.Ingredients.Add(new IngredientWithAmount());
            recipe2.Ingredients.Add(new IngredientWithAmount());

            //Act
            var hash1 = recipe1.GetHashCode();
            var hash2 = recipe2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.False);
        }

        [Test]
        public void GetHashWithTwoDiffrentObjectsShouldBeTrue()
        {
            //Arrange
            var recipe1 = new Recipe();
            var recipe2 = new Recipe();

            recipe1.Name = "Test2";
            recipe1.Description = "frisch";
            recipe1.Ingredients.Add(new IngredientWithAmount());
            recipe1.Ingredients.Add(new IngredientWithAmount());

            recipe2.Name = "Test2";
            recipe2.Description = "frisch";
            recipe2.Ingredients.Add(new IngredientWithAmount());
            recipe2.Ingredients.Add(new IngredientWithAmount());

            //Act
            var hash1 = recipe1.GetHashCode();
            var hash2 = recipe2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.True);
        }
    }
}
