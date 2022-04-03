using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class novetabele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RezervacijaBazen",
                columns: table => new
                {
                    BazenId = table.Column<int>(nullable: false),
                    RezervacijaID = table.Column<int>(nullable: false),
                    TerminRezervacije = table.Column<DateTime>(nullable: false),
                    BrojLjudi = table.Column<int>(nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    UkupnaCijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaBazen", x => new { x.BazenId, x.RezervacijaID });
                    table.ForeignKey(
                        name: "FK_RezervacijaBazen_Bazen_BazenId",
                        column: x => x.BazenId,
                        principalTable: "Bazen",
                        principalColumn: "BazenID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaBazen_Rezervacija_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacija",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaBungalov",
                columns: table => new
                {
                    BungalovId = table.Column<int>(nullable: false),
                    RezervacijaID = table.Column<int>(nullable: false),
                    TerminRezervacije = table.Column<DateTime>(nullable: false),
                    BrojDana = table.Column<int>(nullable: false),
                    CijenaPoDanu = table.Column<float>(nullable: false),
                    UkupnaCijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaBungalov", x => new { x.BungalovId, x.RezervacijaID });
                    table.ForeignKey(
                        name: "FK_RezervacijaBungalov_Bungalov_BungalovId",
                        column: x => x.BungalovId,
                        principalTable: "Bungalov",
                        principalColumn: "BungalovId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaBungalov_Rezervacija_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacija",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaSala",
                columns: table => new
                {
                    SalaId = table.Column<int>(nullable: false),
                    RezervacijaID = table.Column<int>(nullable: false),
                    TerminRezervacije = table.Column<DateTime>(nullable: false),
                    BrojDana = table.Column<int>(nullable: false),
                    CijenaPoDanu = table.Column<float>(nullable: false),
                    UkupnaCijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaSala", x => new { x.SalaId, x.RezervacijaID });
                    table.ForeignKey(
                        name: "FK_RezervacijaSala_Rezervacija_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacija",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaSala_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "SalaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaSpaCentar",
                columns: table => new
                {
                    SpaCentarId = table.Column<int>(nullable: false),
                    RezervacijaID = table.Column<int>(nullable: false),
                    TerminRezervacije = table.Column<DateTime>(nullable: false),
                    BrojTretmana = table.Column<int>(nullable: false),
                    CijenaTretmana = table.Column<float>(nullable: false),
                    UkupnaCijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaSpaCentar", x => new { x.SpaCentarId, x.RezervacijaID });
                    table.ForeignKey(
                        name: "FK_RezervacijaSpaCentar_Rezervacija_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacija",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaSpaCentar_SpaCentar_SpaCentarId",
                        column: x => x.SpaCentarId,
                        principalTable: "SpaCentar",
                        principalColumn: "SpaCentarID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaBazen_RezervacijaID",
                table: "RezervacijaBazen",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaBungalov_RezervacijaID",
                table: "RezervacijaBungalov",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaSala_RezervacijaID",
                table: "RezervacijaSala",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaSpaCentar_RezervacijaID",
                table: "RezervacijaSpaCentar",
                column: "RezervacijaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RezervacijaBazen");

            migrationBuilder.DropTable(
                name: "RezervacijaBungalov");

            migrationBuilder.DropTable(
                name: "RezervacijaSala");

            migrationBuilder.DropTable(
                name: "RezervacijaSpaCentar");
        }
    }
}
