using MenuPlanerApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanerApp.API.Data
{
    public class MenuPlanerAppApiContext : DbContext
    {
        public MenuPlanerAppApiContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipe { get; set; }

        public DbSet<Ingredient> Ingredient { get; set; }

        public DbSet<MenuPlan> MenuPlan { get; set; }

        public DbSet<IngredientWithAmount> IngredientWithAmount { get; set; }

        public DbSet<RecipeWithAmount> RecipeWithAmount { get; set; }

        public DbSet<UserOptions> UserOptions { get; set; }
    }
}