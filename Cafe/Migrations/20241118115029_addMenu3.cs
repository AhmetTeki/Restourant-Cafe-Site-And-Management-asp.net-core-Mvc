using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe.Migrations
{
    public partial class addMenu3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Özel",
                table: "Menu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Özel",
                table: "Menu",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
