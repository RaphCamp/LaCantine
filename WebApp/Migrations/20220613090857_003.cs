using Microsoft.EntityFrameworkCore.Migrations;

namespace LaCantine.Migrations
{
    public partial class _003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Commandes_CommandesID",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_Plats_Commandes_CommandesID",
                table: "Plats");

            migrationBuilder.DropIndex(
                name: "IX_Plats_CommandesID",
                table: "Plats");

            migrationBuilder.DropIndex(
                name: "IX_Menu_CommandesID",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "CommandesID",
                table: "Plats");

            migrationBuilder.DropColumn(
                name: "CommandesID",
                table: "Menu");

            migrationBuilder.CreateTable(
                name: "CommandesMenu",
                columns: table => new
                {
                    LesMenusid = table.Column<int>(type: "int", nullable: false),
                    commandesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandesMenu", x => new { x.LesMenusid, x.commandesID });
                    table.ForeignKey(
                        name: "FK_CommandesMenu_Commandes_commandesID",
                        column: x => x.commandesID,
                        principalTable: "Commandes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandesMenu_Menu_LesMenusid",
                        column: x => x.LesMenusid,
                        principalTable: "Menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandesPlats",
                columns: table => new
                {
                    LesPlatsid = table.Column<int>(type: "int", nullable: false),
                    commandesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandesPlats", x => new { x.LesPlatsid, x.commandesID });
                    table.ForeignKey(
                        name: "FK_CommandesPlats_Commandes_commandesID",
                        column: x => x.commandesID,
                        principalTable: "Commandes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandesPlats_Plats_LesPlatsid",
                        column: x => x.LesPlatsid,
                        principalTable: "Plats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandesMenu_commandesID",
                table: "CommandesMenu",
                column: "commandesID");

            migrationBuilder.CreateIndex(
                name: "IX_CommandesPlats_commandesID",
                table: "CommandesPlats",
                column: "commandesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandesMenu");

            migrationBuilder.DropTable(
                name: "CommandesPlats");

            migrationBuilder.AddColumn<int>(
                name: "CommandesID",
                table: "Plats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommandesID",
                table: "Menu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plats_CommandesID",
                table: "Plats",
                column: "CommandesID");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_CommandesID",
                table: "Menu",
                column: "CommandesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Commandes_CommandesID",
                table: "Menu",
                column: "CommandesID",
                principalTable: "Commandes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plats_Commandes_CommandesID",
                table: "Plats",
                column: "CommandesID",
                principalTable: "Commandes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
