using MenuPlanerApp.Core.Repository;
using NUnit.Framework;

namespace MenuPlanerApp.Core.Tests.Repository
{
    [TestFixture]
    internal class IngredientsRepositoryTests
    {
        [Test]
        public void RepositoryHasSomeData()
        {
            //Arrange
            var repo = new IngredientsRepositoryWeb();
            //Act
            var data = repo.GetAllIngredients();
            //Assert
            Assert.That(data.Result.Count, Is.GreaterThan(0));
        }
    }
}