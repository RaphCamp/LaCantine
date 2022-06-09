using Microsoft.EntityFrameworkCore.Migrations;

namespace LaCantine.Migrations
{
    public partial class Plat2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Allergenes_Plats_PlatsID",
                table: "Produits_Allergenes");

            migrationBuilder.DropIndex(
                name: "IX_Produits_Allergenes_PlatsID",
                table: "Produits_Allergenes");

            migrationBuilder.DropColumn(
                name: "PlatsID",
                table: "Produits_Allergenes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlatsID",
                table: "Produits_Allergenes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produits_Allergenes_PlatsID",
                table: "Produits_Allergenes",
                column: "PlatsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Allergenes_Plats_PlatsID",
                table: "Produits_Allergenes",
                column: "PlatsID",
                principalTable: "Plats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
