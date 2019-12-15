using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanerApp.API.Migrations
{
    public partial class definedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientWithAmount_Ingredient_IngredientId",
                table: "IngredientWithAmount");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeWithAmount_Recipe_RecipeId",
                table: "RecipeWithAmount");

            migrationBuilder.DropIndex(
                name: "IX_RecipeWithAmount_RecipeId",
                table: "RecipeWithAmount");

            migrationBuilder.DropIndex(
                name: "IX_IngredientWithAmount_IngredientId",
                table: "IngredientWithAmount");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeWithAmount");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "IngredientWithAmount");

            migrationBuilder.AddColumn<int>(
                name: "RecipeWithAmountId",
                table: "Recipe",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IngredientWithAmountId",
                table: "Ingredient",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RecipeWithAmountId",
                table: "Recipe",
                column: "RecipeWithAmountId",
                unique: true,
                filter: "[RecipeWithAmountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_IngredientWithAmountId",
                table: "Ingredient",
                column: "IngredientWithAmountId",
                unique: true,
                filter: "[IngredientWithAmountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_IngredientWithAmount_IngredientWithAmountId",
                table: "Ingredient",
                column: "IngredientWithAmountId",
                principalTable: "IngredientWithAmount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_RecipeWithAmount_RecipeWithAmountId",
                table: "Recipe",
                column: "RecipeWithAmountId",
                principalTable: "RecipeWithAmount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_IngredientWithAmount_IngredientWithAmountId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_RecipeWithAmount_RecipeWithAmountId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RecipeWithAmountId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_IngredientWithAmountId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "RecipeWithAmountId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "IngredientWithAmountId",
                table: "Ingredient");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "RecipeWithAmount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "IngredientWithAmount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeWithAmount_RecipeId",
                table: "RecipeWithAmount",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientWithAmount_IngredientId",
                table: "IngredientWithAmount",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientWithAmount_Ingredient_IngredientId",
                table: "IngredientWithAmount",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeWithAmount_Recipe_RecipeId",
                table: "RecipeWithAmount",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
