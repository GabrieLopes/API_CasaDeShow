using Microsoft.EntityFrameworkCore.Migrations;

namespace CasaEventos.Migrations
{
    public partial class EventoImagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GeneroNome",
                table: "Genero",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Evento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Evento");

            migrationBuilder.AlterColumn<string>(
                name: "GeneroNome",
                table: "Genero",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
