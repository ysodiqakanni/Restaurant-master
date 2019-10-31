using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class linkRestaurantToArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Restaurants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AreaId",
                table: "Restaurants",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Areas_AreaId",
                table: "Restaurants",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Areas_AreaId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AreaId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Restaurants");
        }
    }
}
