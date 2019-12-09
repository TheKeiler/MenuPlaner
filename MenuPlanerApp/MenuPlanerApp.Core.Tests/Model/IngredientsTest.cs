using MenuPlanerApp.Core.Model;
using NUnit.Framework;

namespace MenuPlanerApp.Core.Tests.Model
{
    [TestFixture]
    internal class IngredientsTest
    {
        [Test]
        public void EqualsWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var ingredient1 = new Ingredient();
            var ingredient2 = new Ingredient();

            ingredient1.Name = "Test";
            ingredient1.Description = "frisch";

            ingredient2.Name = "Test2";
            ingredient2.Description = "frisch";

            //Act
            var equal = ingredient1.Equals(ingredient2);
            //Assert
            Assert.That(equal, Is.False);
        }

        [Test]
        public void EqualsWithTwoSameObjectsShouldBeTrue()
        {
            //Arrange
            var ingredient1 = new Ingredient();
            var ingredient2 = new Ingredient();

            ingredient1.Name = "Test";
            ingredient1.Description = "frisch";

            ingredient2.Name = "Test";
            ingredient2.Description = "frisch";

            //Act
            var equal = ingredient1.Equals(ingredient2);
            //Assert
            Assert.That(equal, Is.True);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var ingredient1 = new Ingredient();
            var ingredient2 = new Ingredient();

            ingredient1.Name = "Test";
            ingredient1.Description = "frisch";

            ingredient2.Name = "Test2";
            ingredient2.Description = "frisch";

            //Act
            var hash1 = ingredient1.GetHashCode();
            var hash2 = ingredient2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.False);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeTrue()
        {
            //Arrange
            var ingredient1 = new Ingredient();
            var ingredient2 = new Ingredient();

            ingredient1.Name = "Test";
            ingredient1.Description = "frisch";

            ingredient2.Name = "Test";
            ingredient2.Description = "frisch";

            //Act
            var hash1 = ingredient1.GetHashCode();
            var hash2 = ingredient2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.True);
        }
    }
}