using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CasaEventos.Migrations
{
    public partial class EventoFotoAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FotoEvento",
                table: "Evento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoEvento",
                table: "Evento");
        }
    }
}
