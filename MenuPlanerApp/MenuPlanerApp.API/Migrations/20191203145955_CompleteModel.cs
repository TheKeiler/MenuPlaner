using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanerApp.API.Migrations
{
    public partial class CompleteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Ingredient_Recipe_RecipeId",
                "Ingredient");

            migrationBuilder.DropForeignKey(
                "FK_Recipe_MenuPlan_MenuPlanId",
                "Recipe");

            migrationBuilder.DropIndex(
                "IX_Recipe_MenuPlanId",
                "Recipe");

            migrationBuilder.DropIndex(
                "IX_Ingredient_RecipeId",
                "Ingredient");

            migrationBuilder.DropColumn(
                "DayOfWeek",
                "Recipe");

            migrationBuilder.DropColumn(
                "MealDayTime",
                "Recipe");

            migrationBuilder.DropColumn(
                "MenuPlanId",
                "Recipe");

            migrationBuilder.DropColumn(
                "NumbersOfMeals",
                "Recipe");

            migrationBuilder.DropColumn(
                "Amount",
                "Ingredient");

            migrationBuilder.DropColumn(
                "RecipeId",
                "Ingredient");

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

            migrationBuilder.AddColumn<int>(
                "DayOfWeek",
                "Recipe",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "MealDayTime",
                "Recipe",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "MenuPlanId",
                "Recipe",
                "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "NumbersOfMeals",
                "Recipe",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "Amount",
                "Ingredient",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "RecipeId",
                "Ingredient",
                "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Recipe_MenuPlanId",
                "Recipe",
                "MenuPlanId");

            migrationBuilder.CreateIndex(
                "IX_Ingredient_RecipeId",
                "Ingredient",
                "RecipeId");

            migrationBuilder.AddForeignKey(
                "FK_Ingredient_Recipe_RecipeId",
                "Ingredient",
                "RecipeId",
                "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Recipe_MenuPlan_MenuPlanId",
                "Recipe",
                "MenuPlanId",
                "MenuPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}