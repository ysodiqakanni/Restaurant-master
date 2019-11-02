using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addPriorityColumnToMealCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "MealContents",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MealContents",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "MealContents",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "MealContents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "MealContents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "MealCategories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MealContents");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MealContents");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "MealContents");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MealContents");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "MealCategories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MealContents",
                newName: "Content");
        }
    }
}
