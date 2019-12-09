using System;
using System.Collections.Generic;
using System.Text;
using MenuPlanerApp.Core.Model;
using NUnit.Framework;

namespace MenuPlanerApp.Core.Tests.Model
{
    [TestFixture]
    internal class MenuPlanTest
    {
        [Test]
        public void EqualsWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var menu1 = new MenuPlan();
            var menu2 = new MenuPlan();

            var recipes = new List<RecipeWithAmount>()
            {
                new RecipeWithAmount(),
                new RecipeWithAmount(),
                new RecipeWithAmount()
            };

            menu1.Recipes = recipes;
            menu1.StartDate = new DateTime(1991, 03, 11);

            menu2.Recipes = recipes;
            menu2.StartDate = new DateTime(1991, 03, 12);

            //Act
            var equal = menu1.Equals(menu2);

            //Assert
            Assert.That(equal, Is.False);
        }

        [Test]
        public void EqualsWithTwoDifferentObjectsShouldBeTrue()
        {
            //Arrange
            var menu1 = new MenuPlan();
            var menu2 = new MenuPlan();

            var recipes = new List<RecipeWithAmount>()
            {
                new RecipeWithAmount(),
                new RecipeWithAmount(),
                new RecipeWithAmount()
            };

            menu1.Recipes = recipes;
            menu1.StartDate = new DateTime(1991, 03, 11);

            menu2.Recipes = recipes;
            menu2.StartDate = new DateTime(1991, 03, 11);

            //Act
            var equal = menu1.Equals(menu2);

            //Assert
            Assert.That(equal, Is.True);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeTrue()
        {
            //Arrange
            var menu1 = new MenuPlan();
            var menu2 = new MenuPlan();

            var recipes = new List<RecipeWithAmount>()
            {
                new RecipeWithAmount(),
                new RecipeWithAmount(),
                new RecipeWithAmount()
            };

            menu1.Recipes = recipes;
            menu1.StartDate = new DateTime(1991, 03, 11);

            menu2.Recipes = recipes;
            menu2.StartDate = new DateTime(1991, 03, 11);

            //Act
            var hash1 = menu1.GetHashCode();
            var hash2 = menu2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.True);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var menu1 = new MenuPlan();
            var menu2 = new MenuPlan();

            var recipes = new List<RecipeWithAmount>()
            {
                new RecipeWithAmount(),
                new RecipeWithAmount(),
                new RecipeWithAmount()
            };

            menu1.Recipes = recipes;
            menu1.StartDate = new DateTime(1991, 03, 11);

            menu2.Recipes = recipes;
            menu2.StartDate = new DateTime(1991, 03, 13);

            //Act
            var hash1 = menu1.GetHashCode();
            var hash2 = menu2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.False);
        }
    }
}
