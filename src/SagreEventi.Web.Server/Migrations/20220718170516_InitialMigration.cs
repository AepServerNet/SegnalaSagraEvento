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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CittaEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInizioEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFineEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescrizioneEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventoConcluso = table.Column<bool>(type: "bit", nullable: false),
                    DataOraUltimaModifica = table.Column<DateTime>(type: "datetime2", nullable: false)
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
