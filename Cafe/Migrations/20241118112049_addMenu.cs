using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe.Migrations
{
    public partial class addMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Özel = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CatagoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Catagory_CatagoryId",
                        column: x => x.CatagoryId,
                        principalTable: "Catagory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_CatagoryId",
                table: "Menu",
                column: "CatagoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
