using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class lastEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_NacinPlacanja_NacinPlacanjaID",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Racun_RacunID",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_StatusRezervacije_StatusRezervacijeID",
                table: "Rezervacija");

            migrationBuilder.DropTable(
                name: "NacinPlacanja");

            migrationBuilder.DropTable(
                name: "Racun");

            migrationBuilder.DropTable(
                name: "StatusRezervacije");

            migrationBuilder.DropTable(
                name: "KreditnaKartica");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_NacinPlacanjaID",
                table: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_RacunID",
                table: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_StatusRezervacijeID",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "NacinPlacanjaID",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "RacunID",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "StatusRezervacijeID",
                table: "Rezervacija");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NacinPlacanjaID",
                table: "Rezervacija",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RacunID",
                table: "Rezervacija",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusRezervacijeID",
                table: "Rezervacija",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KreditnaKartica",
                columns: table => new
                {
                    KreditnaKarticaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojKreditneKartice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GodinaIstekaKartice = table.Column<int>(type: "int", nullable: false),
                    KorisnikID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MjesecIstekaKartice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KreditnaKartica", x => x.KreditnaKarticaID);
                    table.ForeignKey(
                        name: "FK_KreditnaKartica_AspNetUsers_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NacinPlacanja",
                columns: table => new
                {
                    NacinPlacanjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NacinPlacanjaNaziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinPlacanja", x => x.NacinPlacanjaID);
                });

            migrationBuilder.CreateTable(
                name: "StatusRezervacije",
                columns: table => new
                {
                    StatusRezervacijeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusRezervacije", x => x.StatusRezervacijeID);
                });

            migrationBuilder.CreateTable(
                name: "Racun",
                columns: table => new
                {
                    RacunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IznosRacuna = table.Column<float>(type: "real", nullable: false),
                    KreditnaKarticaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racun", x => x.RacunID);
                    table.ForeignKey(
                        name: "FK_Racun_KreditnaKartica_KreditnaKarticaID",
                        column: x => x.KreditnaKarticaID,
                        principalTable: "KreditnaKartica",
                        principalColumn: "KreditnaKarticaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_NacinPlacanjaID",
                table: "Rezervacija",
                column: "NacinPlacanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_RacunID",
                table: "Rezervacija",
                column: "RacunID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_StatusRezervacijeID",
                table: "Rezervacija",
                column: "StatusRezervacijeID");

            migrationBuilder.CreateIndex(
                name: "IX_KreditnaKartica_KorisnikID",
                table: "KreditnaKartica",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_KreditnaKarticaID",
                table: "Racun",
                column: "KreditnaKarticaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_NacinPlacanja_NacinPlacanjaID",
                table: "Rezervacija",
                column: "NacinPlacanjaID",
                principalTable: "NacinPlacanja",
                principalColumn: "NacinPlacanjaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Racun_RacunID",
                table: "Rezervacija",
                column: "RacunID",
                principalTable: "Racun",
                principalColumn: "RacunID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_StatusRezervacije_StatusRezervacijeID",
                table: "Rezervacija",
                column: "StatusRezervacijeID",
                principalTable: "StatusRezervacije",
                principalColumn: "StatusRezervacijeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
