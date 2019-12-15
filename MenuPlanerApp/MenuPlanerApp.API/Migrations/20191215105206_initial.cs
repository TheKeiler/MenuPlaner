using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanerApp.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    ReferenceUnit = table.Column<string>(maxLength: 60, nullable: false),
                    CompatibleForFructose = table.Column<bool>(nullable: false),
                    CompatibleForHistamin = table.Column<bool>(nullable: false),
                    CompatibleForLactose = table.Column<bool>(nullable: false),
                    CompatibleForCeliac = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    DirectionPictures = table.Column<string>(nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WantsUserToSeeRecipesWithFructose = table.Column<bool>(nullable: false),
                    WantsUserToSeeRecipesWithHistamin = table.Column<bool>(nullable: false),
                    WantsUserToSeeRecipesWithLactose = table.Column<bool>(nullable: false),
                    WantsUserToSeeRecipesWithCeliac = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientWithAmount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IngredientWithAmountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientWithAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientWithAmount_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientWithAmount_Recipe_IngredientWithAmountId",
                        column: x => x.IngredientWithAmountId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeWithAmount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(nullable: false),
                    NumbersOfMeals = table.Column<int>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false),
                    MealDayTime = table.Column<int>(nullable: false),
                    RecipeWithAmountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeWithAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeWithAmount_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeWithAmount_MenuPlan_RecipeWithAmountId",
                        column: x => x.RecipeWithAmountId,
                        principalTable: "MenuPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientWithAmount_IngredientId",
                table: "IngredientWithAmount",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientWithAmount_IngredientWithAmountId",
                table: "IngredientWithAmount",
                column: "IngredientWithAmountId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeWithAmount_RecipeId",
                table: "RecipeWithAmount",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeWithAmount_RecipeWithAmountId",
                table: "RecipeWithAmount",
                column: "RecipeWithAmountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientWithAmount");

            migrationBuilder.DropTable(
                name: "RecipeWithAmount");

            migrationBuilder.DropTable(
                name: "UserOptions");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "MenuPlan");
        }
    }
}
