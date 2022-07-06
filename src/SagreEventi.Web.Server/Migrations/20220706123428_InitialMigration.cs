using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SagreEventi.Web.Server.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventi",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    NomeEvento = table.Column<string>(type: "TEXT", nullable: true),
                    DescrizioneEvento = table.Column<string>(type: "TEXT", nullable: true),
                    CittaEvento = table.Column<string>(type: "TEXT", nullable: true),
                    DataOraEvento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EventoConcluso = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataOraUltimaModifica = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventi", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventi");
        }
    }
}
