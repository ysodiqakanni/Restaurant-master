using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class createAreaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealCategories_MealCategoryId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealTypes_MealTypeId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Restaurants_RestaurantId",
                table: "Meals");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Meals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MealTypeId",
                table: "Meals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MealCategoryId",
                table: "Meals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealCategories_MealCategoryId",
                table: "Meals",
                column: "MealCategoryId",
                principalTable: "MealCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealTypes_MealTypeId",
                table: "Meals",
                column: "MealTypeId",
                principalTable: "MealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Restaurants_RestaurantId",
                table: "Meals",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealCategories_MealCategoryId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealTypes_MealTypeId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Restaurants_RestaurantId",
                table: "Meals");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Meals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MealTypeId",
                table: "Meals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MealCategoryId",
                table: "Meals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealCategories_MealCategoryId",
                table: "Meals",
                column: "MealCategoryId",
                principalTable: "MealCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealTypes_MealTypeId",
                table: "Meals",
                column: "MealTypeId",
                principalTable: "MealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Restaurants_RestaurantId",
                table: "Meals",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
