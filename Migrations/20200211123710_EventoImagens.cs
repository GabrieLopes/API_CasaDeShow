using Microsoft.EntityFrameworkCore.Migrations;

namespace CasaEventos.Migrations
{
    public partial class EventoImagens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusEvento",
                table: "Evento");

            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Evento",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Evento");

            migrationBuilder.AddColumn<bool>(
                name: "StatusEvento",
                table: "Evento",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
