using MenuPlanerApp.Core.Model;
using NUnit.Framework;

namespace MenuPlanerApp.Core.Tests.Model
{
    [TestFixture]
    internal class UserOptionsTest
    {
        [Test]
        public void EqualsWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var opt1 = new UserOptions();
            var opt2 = new UserOptions();

            opt1.WantsUserToSeeRecipesWithCeliac = true;
            opt1.WantsUserToSeeRecipesWithFructose = true;

            opt2.WantsUserToSeeRecipesWithCeliac = true;
            opt2.WantsUserToSeeRecipesWithFructose = false;

            //Act
            var equals = opt1.Equals(opt2);

            //Assert
            Assert.That(equals, Is.False);
        }

        [Test]
        public void EqualsWithTwoDifferentObjectsShouldBeTrue()
        {
            //Arrange
            var opt1 = new UserOptions();
            var opt2 = new UserOptions();

            opt1.WantsUserToSeeRecipesWithCeliac = false;
            opt1.WantsUserToSeeRecipesWithFructose = false;

            opt2.WantsUserToSeeRecipesWithCeliac = false;
            opt2.WantsUserToSeeRecipesWithFructose = false;

            //Act
            var equals = opt1.Equals(opt2);

            //Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeTrue()
        {
            //Arrange
            var opt1 = new UserOptions();
            var opt2 = new UserOptions();

            opt1.WantsUserToSeeRecipesWithCeliac = false;
            opt1.WantsUserToSeeRecipesWithFructose = false;

            opt2.WantsUserToSeeRecipesWithCeliac = false;
            opt2.WantsUserToSeeRecipesWithFructose = false;

            //Act
            var hash1 = opt1.GetHashCode();
            var hash2 = opt2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.True);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var opt1 = new UserOptions();
            var opt2 = new UserOptions();

            opt1.WantsUserToSeeRecipesWithCeliac = false;
            opt1.WantsUserToSeeRecipesWithFructose = false;

            opt2.WantsUserToSeeRecipesWithCeliac = true;
            opt2.WantsUserToSeeRecipesWithFructose = false;

            //Act
            var hash1 = opt1.GetHashCode();
            var hash2 = opt2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.False);
        }
    }
}
