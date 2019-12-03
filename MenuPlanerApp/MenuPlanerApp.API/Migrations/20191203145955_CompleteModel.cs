using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanerApp.API.Migrations
{
    public partial class CompleteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_MenuPlan_MenuPlanId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_MenuPlanId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_RecipeId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "MealDayTime",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "MenuPlanId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "NumbersOfMeals",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Ingredient");

            migrationBuilder.CreateTable(
                name: "IngredientWithAmount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    RecipeId = table.Column<int>(nullable: true)
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
                        name: "FK_IngredientWithAmount_Recipe_RecipeId",
                        column: x => x.RecipeId,
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
                    MenuPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeWithAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeWithAmount_MenuPlan_MenuPlanId",
                        column: x => x.MenuPlanId,
                        principalTable: "MenuPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeWithAmount_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientWithAmount_IngredientId",
                table: "IngredientWithAmount",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientWithAmount_RecipeId",
                table: "IngredientWithAmount",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeWithAmount_MenuPlanId",
                table: "RecipeWithAmount",
                column: "MenuPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeWithAmount_RecipeId",
                table: "RecipeWithAmount",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientWithAmount");

            migrationBuilder.DropTable(
                name: "RecipeWithAmount");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Recipe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealDayTime",
                table: "Recipe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MenuPlanId",
                table: "Recipe",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumbersOfMeals",
                table: "Recipe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Ingredient",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_MenuPlanId",
                table: "Recipe",
                column: "MenuPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_RecipeId",
                table: "Ingredient",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_MenuPlan_MenuPlanId",
                table: "Recipe",
                column: "MenuPlanId",
                principalTable: "MenuPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
