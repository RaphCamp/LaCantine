using Microsoft.EntityFrameworkCore.Migrations;

namespace LaCantine.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plats_Menu_Menuid",
                table: "Plats");

            migrationBuilder.DropIndex(
                name: "IX_Plats_Menuid",
                table: "Plats");

            migrationBuilder.DropColumn(
                name: "Menuid",
                table: "Plats");

            migrationBuilder.CreateTable(
                name: "MenuPlats",
                columns: table => new
                {
                    menusid = table.Column<int>(type: "int", nullable: false),
                    platsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPlats", x => new { x.menusid, x.platsid });
                    table.ForeignKey(
                        name: "FK_MenuPlats_Menu_menusid",
                        column: x => x.menusid,
                        principalTable: "Menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPlats_Plats_platsid",
                        column: x => x.platsid,
                        principalTable: "Plats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuPlats_platsid",
                table: "MenuPlats",
                column: "platsid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuPlats");

            migrationBuilder.AddColumn<int>(
                name: "Menuid",
                table: "Plats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plats_Menuid",
                table: "Plats",
                column: "Menuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Plats_Menu_Menuid",
                table: "Plats",
                column: "Menuid",
                principalTable: "Menu",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
