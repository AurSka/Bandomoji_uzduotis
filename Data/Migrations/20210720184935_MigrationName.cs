using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bandomoji_uzduotis.Data.Migrations
{
    public partial class MigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmas",
                columns: table => new
                {
                    Pavadinimas = table.Column<string>(nullable: false),
                    IsleidimoData = table.Column<DateTime>(nullable: false),
                    FilmoZanras = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmas", x => x.Pavadinimas);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filmas");
        }
    }
}
