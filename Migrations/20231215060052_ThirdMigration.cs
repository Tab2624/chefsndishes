using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chefsndishes.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DishID",
                table: "Dishes",
                newName: "DishId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DishId",
                table: "Dishes",
                newName: "DishID");
        }
    }
}
