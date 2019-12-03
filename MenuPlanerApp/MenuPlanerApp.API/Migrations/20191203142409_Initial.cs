using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanerApp.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    IsFavorite = table.Column<bool>(),
                    NumbersOfMeals = table.Column<int>(),
                    DayOfWeek = table.Column<int>(),
                    MealDayTime = table.Column<int>(),
                    MenuPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                    table.ForeignKey(
                        "FK_Recipe_MenuPlan_MenuPlanId",
                        x => x.MenuPlanId,
                        "MenuPlan",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    CompatibleForCeliac = table.Column<bool>(),
                    Amount = table.Column<int>(),
                    RecipeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        "FK_Ingredient_Recipe_RecipeId",
                        x => x.RecipeId,
                        "Recipe",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Ingredient_RecipeId",
                "Ingredient",
                "RecipeId");

            migrationBuilder.CreateIndex(
                "IX_Recipe_MenuPlanId",
                "Recipe",
                "MenuPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Ingredient");

            migrationBuilder.DropTable(
                "Recipe");

            migrationBuilder.DropTable(
                "MenuPlan");
        }
    }
}