using System;


namespace MenuPlanerApp.Core.UnitTests.Model
{
    using MenuPlanerApp.Core.Repository;
    using NUnit.Framework;
    [TestFixture]
    class IngredientsRepositoryTests
    {
        [Test]
        public void TestHasRepoSomeData()
        {
            //Arrange
            var ingredientsRepository = new IngredientsRepository();
            //Act
            var data = ingredientsRepository.GetAllIngredients();
            //Assert
            Assert.That(data.Count, Is.GreaterThan(0));
        }
    }
}
