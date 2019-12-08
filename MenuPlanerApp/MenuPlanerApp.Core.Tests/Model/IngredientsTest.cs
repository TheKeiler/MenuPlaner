using MenuPlanerApp.Core.Model;
using NUnit.Framework;


namespace MenuPlanerApp.Core.Tests.Model
{
    [TestFixture]
    internal class IngredientsTest
    {
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
        public void EqualsWithTwoDiffrentObjectsShouldBeFalse()
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
    }
}
