using System;
using System.Collections.Generic;
using System.Text;
using MenuPlanerApp.Core.Model;
using NUnit.Framework;

namespace MenuPlanerApp.Core.Tests.Model
{
    [TestFixture]
    internal class RecipeWithAmountTest
    {
        [Test]
        public void EqualsWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var recipewa1 = new RecipeWithAmount();
            var recipewa2 = new RecipeWithAmount();

            var ingredient = new Ingredient();
            ingredient.Name = "bla";
            ingredient.Description = "test";
            ingredient.ReferenceUnit = "gramm";

            var ingredient1 = new IngredientWithAmount();
            ingredient1.Ingredient = ingredient;
            ingredient1.Amount = 120.5m;

            var ingredient2 = new IngredientWithAmount();
            ingredient2.Ingredient = ingredient;
            ingredient2.Amount = 130.5m;

            var recipe = new Recipe();
            recipe.Ingredients.Add(ingredient1);
            recipe.Ingredients.Add(ingredient2);
            recipe.Name = "rec1";
            recipe.Description = "";

            recipewa1.Recipe = recipe;
            recipewa1.DayOfWeek = DayOfWeek.Friday;
            recipewa1.MealDayTime = MealDayTimeEnum.Dinner;
            recipewa1.NumbersOfMeals = 2;

            recipewa2.Recipe = recipe;
            recipewa2.DayOfWeek = DayOfWeek.Friday;
            recipewa2.MealDayTime = MealDayTimeEnum.Dinner;
            recipewa2.NumbersOfMeals = 1;

            //Act
            var equals = recipewa1.Equals(recipewa2);
            
            //Assert
            Assert.That(equals, Is.False);
        }

        [Test]
        public void EqualsWithTwoDifferentObjectsShouldBeTrue()
        {
            //Arrange
            var recipewa1 = new RecipeWithAmount();
            var recipewa2 = new RecipeWithAmount();

            var ingredient = new Ingredient();
            ingredient.Name = "bla";
            ingredient.Description = "test";
            ingredient.ReferenceUnit = "gramm";

            var ingredient1 = new IngredientWithAmount();
            ingredient1.Ingredient = ingredient;
            ingredient1.Amount = 120.5m;

            var ingredient2 = new IngredientWithAmount();
            ingredient2.Ingredient = ingredient;
            ingredient2.Amount = 130.5m;

            var recipe = new Recipe();
            recipe.Ingredients.Add(ingredient1);
            recipe.Ingredients.Add(ingredient2);
            recipe.Name = "rec1";
            recipe.Description = "";

            recipewa1.Recipe = recipe;
            recipewa1.DayOfWeek = DayOfWeek.Friday;
            recipewa1.MealDayTime = MealDayTimeEnum.Dinner;
            recipewa1.NumbersOfMeals = 2;

            recipewa2.Recipe = recipe;
            recipewa2.DayOfWeek = DayOfWeek.Friday;
            recipewa2.MealDayTime = MealDayTimeEnum.Dinner;
            recipewa2.NumbersOfMeals = 2;

            //Act
            var equals = recipewa1.Equals(recipewa2);

            //Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeTrue()
        {
            //Arrange
            var recipewa1 = new RecipeWithAmount();
            var recipewa2 = new RecipeWithAmount();

            var ingredient = new Ingredient();
            ingredient.Name = "bla";
            ingredient.Description = "test";
            ingredient.ReferenceUnit = "gramm";

            var ingredient1 = new IngredientWithAmount();
            ingredient1.Ingredient = ingredient;
            ingredient1.Amount = 120.5m;

            var ingredient2 = new IngredientWithAmount();
            ingredient2.Ingredient = ingredient;
            ingredient2.Amount = 130.5m;

            var recipe = new Recipe();
            recipe.Ingredients.Add(ingredient1);
            recipe.Ingredients.Add(ingredient2);
            recipe.Name = "rec1";
            recipe.Description = "";

            recipewa1.Recipe = recipe;
            recipewa1.DayOfWeek = DayOfWeek.Friday;
            recipewa1.MealDayTime = MealDayTimeEnum.Dinner;
            recipewa1.NumbersOfMeals = 2;

            recipewa2.Recipe = recipe;
            recipewa2.DayOfWeek = DayOfWeek.Friday;
            recipewa2.MealDayTime = MealDayTimeEnum.Dinner;
            recipewa2.NumbersOfMeals = 2;

            //Act
            var hash1 = recipewa1.GetHashCode();
            var hash2 = recipewa2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.True);
        }

        [Test]
        public void GetHashWithTwoDifferentObjectsShouldBeFalse()
        {
            //Arrange
            var recipewa1 = new RecipeWithAmount();
            var recipewa2 = new RecipeWithAmount();

            var ingredient = new Ingredient();
            ingredient.Name = "bla";
            ingredient.Description = "test";
            ingredient.ReferenceUnit = "gramm";

            var ingredient1 = new IngredientWithAmount();
            ingredient1.Ingredient = ingredient;
            ingredient1.Amount = 120.5m;

            var ingredient2 = new IngredientWithAmount();
            ingredient2.Ingredient = ingredient;
            ingredient2.Amount = 130.5m;

            var recipe = new Recipe();
            recipe.Ingredients.Add(ingredient1);
            recipe.Ingredients.Add(ingredient2);
            recipe.Name = "rec1";
            recipe.Description = "";

            recipewa1.Recipe = recipe;
            recipewa1.DayOfWeek = DayOfWeek.Friday;
            recipewa1.MealDayTime = MealDayTimeEnum.Lunch;
            recipewa1.NumbersOfMeals = 2;

            recipewa2.Recipe = recipe;
            recipewa2.DayOfWeek = DayOfWeek.Friday;
            recipewa2.MealDayTime = MealDayTimeEnum.Dinner;
            recipewa2.NumbersOfMeals = 2;

            //Act
            var hash1 = recipewa1.GetHashCode();
            var hash2 = recipewa2.GetHashCode();

            //Assert
            Assert.That(hash1.Equals(hash2), Is.False);
        }
    }
}
