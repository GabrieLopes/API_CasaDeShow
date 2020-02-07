using Microsoft.EntityFrameworkCore.Migrations;

namespace CasaEventos.Migrations
{
    public partial class EventoGenero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Casa_CasaId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "GeneroEvento",
                table: "Evento");

            migrationBuilder.AlterColumn<int>(
                name: "CasaId",
                table: "Evento",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Casa_CasaId",
                table: "Evento",
                column: "CasaId",
                principalTable: "Casa",
                principalColumn: "CasaId",
                onDelete: ReferentialAction.Restrict);
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
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeneroEvento",
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
