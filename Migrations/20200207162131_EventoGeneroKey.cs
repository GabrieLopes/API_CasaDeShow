using Microsoft.EntityFrameworkCore.Migrations;

namespace CasaEventos.Migrations
{
    public partial class EventoGeneroKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Evento",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evento_GeneroId",
                table: "Evento",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Genero_GeneroId",
                table: "Evento",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "GeneroId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Genero_GeneroId",
                table: "Evento");

            migrationBuilder.DropIndex(
                name: "IX_Evento_GeneroId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Evento");
        }
    }
}
