using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RestaurantCategory_Priority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "RestaurantCategories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "RestaurantCategories");
        }
    }
}
