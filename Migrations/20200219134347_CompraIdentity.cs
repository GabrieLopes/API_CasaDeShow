using Microsoft.EntityFrameworkCore.Migrations;

namespace CasaEventos.Migrations
{
    public partial class CompraIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Compra",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdentityUserId",
                table: "Compra",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_AspNetUsers_IdentityUserId",
                table: "Compra",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_AspNetUsers_IdentityUserId",
                table: "Compra");

            migrationBuilder.DropIndex(
                name: "IX_Compra_IdentityUserId",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Compra");
        }
    }
}
