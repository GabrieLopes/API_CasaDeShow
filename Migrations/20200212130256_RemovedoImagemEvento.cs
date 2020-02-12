using Microsoft.EntityFrameworkCore.Migrations;

namespace CasaEventos.Migrations
{
    public partial class RemovedoImagemEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Casa_CasaId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Evento");

            migrationBuilder.AlterColumn<int>(
                name: "CasaId",
                table: "Evento",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Casa_CasaId",
                table: "Evento",
                column: "CasaId",
                principalTable: "Casa",
                principalColumn: "CasaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Casa_CasaId",
                table: "Evento");

            migrationBuilder.AlterColumn<int>(
                name: "CasaId",
                table: "Evento",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Evento",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Casa_CasaId",
                table: "Evento",
                column: "CasaId",
                principalTable: "Casa",
                principalColumn: "CasaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
