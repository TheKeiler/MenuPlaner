using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanerApp.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Ingredient",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    ReferenceUnit = table.Column<string>(maxLength: 60),
                    CompatibleForFructose = table.Column<bool>(),
                    CompatibleForHistamin = table.Column<bool>(),
                    CompatibleForLactose = table.Column<bool>(),
                    CompatibleForCeliac = table.Column<bool>()
                },
                constraints: table => { table.PrimaryKey("PK_Ingredient", x => x.Id); });

            migrationBuilder.CreateTable(
                "MenuPlan",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>()
                },
                constraints: table => { table.PrimaryKey("PK_MenuPlan", x => x.Id); });

            migrationBuilder.CreateTable(
                "Recipe",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    DirectionPictures = table.Column<byte[]>(),
                    IsFavorite = table.Column<bool>()
                },
                constraints: table => { table.PrimaryKey("PK_Recipe", x => x.Id); });

            migrationBuilder.CreateTable(
                "UserOptions",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WantsUserToSeeRecipesWithFructose = table.Column<bool>(),
                    WantsUserToSeeRecipesWithHistamin = table.Column<bool>(),
                    WantsUserToSeeRecipesWithLactose = table.Column<bool>(),
                    WantsUserToSeeRecipesWithCeliac = table.Column<bool>()
                },
                constraints: table => { table.PrimaryKey("PK_UserOptions", x => x.Id); });

            migrationBuilder.CreateTable(
                "IngredientWithAmount",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(),
                    Amount = table.Column<int>(),
                    RecipeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientWithAmount", x => x.Id);
                    table.ForeignKey(
                        "FK_IngredientWithAmount_Ingredient_IngredientId",
                        x => x.IngredientId,
                        "Ingredient",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_IngredientWithAmount_Recipe_RecipeId",
                        x => x.RecipeId,
                        "Recipe",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "RecipeWithAmount",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(),
                    NumbersOfMeals = table.Column<int>(),
                    DayOfWeek = table.Column<int>(),
                    MealDayTime = table.Column<int>(),
                    MenuPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeWithAmount", x => x.Id);
                    table.ForeignKey(
                        "FK_RecipeWithAmount_MenuPlan_MenuPlanId",
                        x => x.MenuPlanId,
                        "MenuPlan",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_RecipeWithAmount_Recipe_RecipeId",
                        x => x.RecipeId,
                        "Recipe",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_IngredientWithAmount_IngredientId",
                "IngredientWithAmount",
                "IngredientId");

            migrationBuilder.CreateIndex(
                "IX_IngredientWithAmount_RecipeId",
                "IngredientWithAmount",
                "RecipeId");

            migrationBuilder.CreateIndex(
                "IX_RecipeWithAmount_MenuPlanId",
                "RecipeWithAmount",
                "MenuPlanId");

            migrationBuilder.CreateIndex(
                "IX_RecipeWithAmount_RecipeId",
                "RecipeWithAmount",
                "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "IngredientWithAmount");

            migrationBuilder.DropTable(
                "RecipeWithAmount");

            migrationBuilder.DropTable(
                "UserOptions");

            migrationBuilder.DropTable(
                "Ingredient");

            migrationBuilder.DropTable(
                "MenuPlan");

            migrationBuilder.DropTable(
                "Recipe");
        }
    }
}