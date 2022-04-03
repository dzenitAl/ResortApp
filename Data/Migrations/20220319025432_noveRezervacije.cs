using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class noveRezervacije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bazen",
                columns: table => new
                {
                    BazenID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PutanjaDoSlikeSale = table.Column<string>(nullable: true),
                    NazivBazena = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    Cijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bazen", x => x.BazenID);
                });

            migrationBuilder.CreateTable(
                name: "BungalovTip",
                columns: table => new
                {
                    BungalovTipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivTipaBungalova = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BungalovTip", x => x.BungalovTipID);
                });

            migrationBuilder.CreateTable(
                name: "MeniRestoranTip",
                columns: table => new
                {
                    MeniRestoranTipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivTipaMenija = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeniRestoranTip", x => x.MeniRestoranTipID);
                });

            migrationBuilder.CreateTable(
                name: "NacinPlacanja",
                columns: table => new
                {
                    NacinPlacanjaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NacinPlacanjaNaziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinPlacanja", x => x.NacinPlacanjaID);
                });

            migrationBuilder.CreateTable(
                name: "Rola",
                columns: table => new
                {
                    RolaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rola", x => x.RolaID);
                });

            migrationBuilder.CreateTable(
                name: "SalaTip",
                columns: table => new
                {
                    SalaTipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivTipaSale = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaTip", x => x.SalaTipID);
                });

            migrationBuilder.CreateTable(
                name: "SobaTip",
                columns: table => new
                {
                    SobaTipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivTipaSobe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SobaTip", x => x.SobaTipID);
                });

            migrationBuilder.CreateTable(
                name: "SpaCentar",
                columns: table => new
                {
                    SpaCentarID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    CijenaZakupa = table.Column<int>(nullable: false),
                    PutanjaDoSlike = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaCentar", x => x.SpaCentarID);
                });

            migrationBuilder.CreateTable(
                name: "SportskaAktivnostTip",
                columns: table => new
                {
                    SportskaAktivnostTipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivTipaAktivnosti = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportskaAktivnostTip", x => x.SportskaAktivnostTipID);
                });

            migrationBuilder.CreateTable(
                name: "StatusRezervacije",
                columns: table => new
                {
                    StatusRezervacijeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusRezervacije", x => x.StatusRezervacijeID);
                });

            migrationBuilder.CreateTable(
                name: "Wellnes",
                columns: table => new
                {
                    WellnesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivWellnes = table.Column<string>(nullable: true),
                    TipWellnes = table.Column<string>(nullable: true),
                    OpisPrograma = table.Column<string>(nullable: true),
                    DatumVrijeme = table.Column<DateTime>(nullable: false),
                    VremenskoTrajanjeTermina = table.Column<string>(nullable: true),
                    CijenaTretmana = table.Column<float>(nullable: false),
                    PutanjaDoSlikeWellnes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wellnes", x => x.WellnesId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Bungalov",
                columns: table => new
                {
                    BungalovId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivBungalova = table.Column<string>(nullable: true),
                    BrojBungalova = table.Column<string>(nullable: true),
                    BungalovTipID = table.Column<int>(nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    OpisBungalova = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bungalov", x => x.BungalovId);
                    table.ForeignKey(
                        name: "FK_Bungalov_BungalovTip_BungalovTipID",
                        column: x => x.BungalovTipID,
                        principalTable: "BungalovTip",
                        principalColumn: "BungalovTipID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeniRestoran",
                columns: table => new
                {
                    MeniRestoranID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Restoran = table.Column<string>(nullable: true),
                    MeniRestoranTipID = table.Column<int>(nullable: false),
                    OpisMenija = table.Column<string>(nullable: true),
                    CijenaMenija = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeniRestoran", x => x.MeniRestoranID);
                    table.ForeignKey(
                        name: "FK_MeniRestoran_MeniRestoranTip_MeniRestoranTipID",
                        column: x => x.MeniRestoranTipID,
                        principalTable: "MeniRestoranTip",
                        principalColumn: "MeniRestoranTipID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    RolaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Rola_RolaID",
                        column: x => x.RolaID,
                        principalTable: "Rola",
                        principalColumn: "RolaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    SalaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KapacitetSale = table.Column<int>(nullable: false),
                    OpisSale = table.Column<string>(nullable: true),
                    NazivSale = table.Column<string>(nullable: true),
                    CijenaIznajmljivanjaSale = table.Column<float>(nullable: false),
                    PutanjaDoSlikeSale = table.Column<string>(nullable: true),
                    SalaTipID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.SalaID);
                    table.ForeignKey(
                        name: "FK_Sala_SalaTip_SalaTipID",
                        column: x => x.SalaTipID,
                        principalTable: "SalaTip",
                        principalColumn: "SalaTipID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Soba",
                columns: table => new
                {
                    SobaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivSobe = table.Column<string>(nullable: true),
                    BrojSobe = table.Column<string>(nullable: true),
                    SobaTipID = table.Column<int>(nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    OpisSobe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soba", x => x.SobaId);
                    table.ForeignKey(
                        name: "FK_Soba_SobaTip_SobaTipID",
                        column: x => x.SobaTipID,
                        principalTable: "SobaTip",
                        principalColumn: "SobaTipID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SportskaAktivnost",
                columns: table => new
                {
                    SportskaAktivnostID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportskeAktivnostiTipID = table.Column<int>(nullable: false),
                    SportskaAktivnostTipID = table.Column<int>(nullable: true),
                    OpisPrograma = table.Column<string>(nullable: true),
                    NazivAktivnosti = table.Column<string>(nullable: true),
                    CijenaAktivnosti = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportskaAktivnost", x => x.SportskaAktivnostID);
                    table.ForeignKey(
                        name: "FK_SportskaAktivnost_SportskaAktivnostTip_SportskaAktivnostTipID",
                        column: x => x.SportskaAktivnostTipID,
                        principalTable: "SportskaAktivnostTip",
                        principalColumn: "SportskaAktivnostTipID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BungalovSlike",
                columns: table => new
                {
                    BungalovSlikeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BungalovId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BungalovSlike", x => x.BungalovSlikeId);
                    table.ForeignKey(
                        name: "FK_BungalovSlike_Bungalov_BungalovId",
                        column: x => x.BungalovId,
                        principalTable: "Bungalov",
                        principalColumn: "BungalovId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeniRestoranSlike",
                columns: table => new
                {
                    MeniRestoranSlikeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeniRestoranID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeniRestoranSlike", x => x.MeniRestoranSlikeID);
                    table.ForeignKey(
                        name: "FK_MeniRestoranSlike_MeniRestoran_MeniRestoranID",
                        column: x => x.MeniRestoranID,
                        principalTable: "MeniRestoran",
                        principalColumn: "MeniRestoranID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    RolaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Admin_AspNetUsers_ID",
                        column: x => x.ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admin_Rola_RolaID",
                        column: x => x.RolaID,
                        principalTable: "Rola",
                        principalColumn: "RolaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Klijent",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    RolaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Klijent_AspNetUsers_ID",
                        column: x => x.ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Klijent_Rola_RolaID",
                        column: x => x.RolaID,
                        principalTable: "Rola",
                        principalColumn: "RolaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KreditnaKartica",
                columns: table => new
                {
                    KreditnaKarticaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojKreditneKartice = table.Column<string>(nullable: true),
                    MjesecIstekaKartice = table.Column<int>(nullable: false),
                    GodinaIstekaKartice = table.Column<int>(nullable: false),
                    CVC = table.Column<string>(nullable: true),
                    KorisnikID = table.Column<string>(nullable: true)
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
                name: "SobaSlike",
                columns: table => new
                {
                    SobaSlikeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SobaId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SobaSlike", x => x.SobaSlikeId);
                    table.ForeignKey(
                        name: "FK_SobaSlike_Soba_SobaId",
                        column: x => x.SobaId,
                        principalTable: "Soba",
                        principalColumn: "SobaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SportskaAktivnostSlike",
                columns: table => new
                {
                    SportskaAktivnostSlikeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportskaAktivnostID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportskaAktivnostSlike", x => x.SportskaAktivnostSlikeId);
                    table.ForeignKey(
                        name: "FK_SportskaAktivnostSlike_SportskaAktivnost_SportskaAktivnostID",
                        column: x => x.SportskaAktivnostID,
                        principalTable: "SportskaAktivnost",
                        principalColumn: "SportskaAktivnostID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Racun",
                columns: table => new
                {
                    RacunID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IznosRacuna = table.Column<float>(nullable: false),
                    KreditnaKarticaID = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    RezervacijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumRezervacije = table.Column<DateTime>(nullable: false),
                    KorisnikID = table.Column<string>(nullable: true),
                    RacunID = table.Column<int>(nullable: true),
                    NacinPlacanjaID = table.Column<int>(nullable: true),
                    StatusRezervacijeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.RezervacijaID);
                    table.ForeignKey(
                        name: "FK_Rezervacija_AspNetUsers_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacija_NacinPlacanja_NacinPlacanjaID",
                        column: x => x.NacinPlacanjaID,
                        principalTable: "NacinPlacanja",
                        principalColumn: "NacinPlacanjaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Racun_RacunID",
                        column: x => x.RacunID,
                        principalTable: "Racun",
                        principalColumn: "RacunID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacija_StatusRezervacije_StatusRezervacijeID",
                        column: x => x.StatusRezervacijeID,
                        principalTable: "StatusRezervacije",
                        principalColumn: "StatusRezervacijeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaMeniRestoran",
                columns: table => new
                {
                    MeniRestoranID = table.Column<int>(nullable: false),
                    RezervacijaID = table.Column<int>(nullable: false),
                    TerminRezervacije = table.Column<DateTime>(nullable: false),
                    BrojTretmana = table.Column<int>(nullable: false),
                    CijenaTretmana = table.Column<float>(nullable: false),
                    UkupnaCijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaMeniRestoran", x => new { x.MeniRestoranID, x.RezervacijaID });
                    table.ForeignKey(
                        name: "FK_RezervacijaMeniRestoran_MeniRestoran_MeniRestoranID",
                        column: x => x.MeniRestoranID,
                        principalTable: "MeniRestoran",
                        principalColumn: "MeniRestoranID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaMeniRestoran_Rezervacija_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacija",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaSoba",
                columns: table => new
                {
                    SobaID = table.Column<int>(nullable: false),
                    RezervacijaID = table.Column<int>(nullable: false),
                    DatumRezervacije = table.Column<DateTime>(nullable: false),
                    BrojNoci = table.Column<int>(nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    UkupnaCijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaSoba", x => new { x.SobaID, x.RezervacijaID });
                    table.ForeignKey(
                        name: "FK_RezervacijaSoba_Rezervacija_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacija",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaSoba_Soba_SobaID",
                        column: x => x.SobaID,
                        principalTable: "Soba",
                        principalColumn: "SobaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaSportskaAktivnost",
                columns: table => new
                {
                    SportskaAktivnostID = table.Column<int>(nullable: false),
                    RezervacijaID = table.Column<int>(nullable: false),
                    TerminRezervacije = table.Column<DateTime>(nullable: false),
                    BrojTermina = table.Column<int>(nullable: false),
                    CijenaTermina = table.Column<float>(nullable: false),
                    UkupnaCijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaSportskaAktivnost", x => new { x.SportskaAktivnostID, x.RezervacijaID });
                    table.ForeignKey(
                        name: "FK_RezervacijaSportskaAktivnost_Rezervacija_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacija",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaSportskaAktivnost_SportskaAktivnost_SportskaAktivnostID",
                        column: x => x.SportskaAktivnostID,
                        principalTable: "SportskaAktivnost",
                        principalColumn: "SportskaAktivnostID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaWellnes",
                columns: table => new
                {
                    WellnesID = table.Column<int>(nullable: false),
                    RezervacijaID = table.Column<int>(nullable: false),
                    TerminRezervacije = table.Column<DateTime>(nullable: false),
                    BrojTretmana = table.Column<int>(nullable: false),
                    CijenaTretmana = table.Column<float>(nullable: false),
                    UkupnaCijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaWellnes", x => new { x.WellnesID, x.RezervacijaID });
                    table.ForeignKey(
                        name: "FK_RezervacijaWellnes_Rezervacija_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacija",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaWellnes_Wellnes_WellnesID",
                        column: x => x.WellnesID,
                        principalTable: "Wellnes",
                        principalColumn: "WellnesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_RolaID",
                table: "Admin",
                column: "RolaID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RolaID",
                table: "AspNetUsers",
                column: "RolaID");

            migrationBuilder.CreateIndex(
                name: "IX_Bungalov_BungalovTipID",
                table: "Bungalov",
                column: "BungalovTipID");

            migrationBuilder.CreateIndex(
                name: "IX_BungalovSlike_BungalovId",
                table: "BungalovSlike",
                column: "BungalovId");

            migrationBuilder.CreateIndex(
                name: "IX_Klijent_RolaID",
                table: "Klijent",
                column: "RolaID");

            migrationBuilder.CreateIndex(
                name: "IX_KreditnaKartica_KorisnikID",
                table: "KreditnaKartica",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_MeniRestoran_MeniRestoranTipID",
                table: "MeniRestoran",
                column: "MeniRestoranTipID");

            migrationBuilder.CreateIndex(
                name: "IX_MeniRestoranSlike_MeniRestoranID",
                table: "MeniRestoranSlike",
                column: "MeniRestoranID");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_KreditnaKarticaID",
                table: "Racun",
                column: "KreditnaKarticaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_KorisnikID",
                table: "Rezervacija",
                column: "KorisnikID");

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
                name: "IX_RezervacijaMeniRestoran_RezervacijaID",
                table: "RezervacijaMeniRestoran",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaSoba_RezervacijaID",
                table: "RezervacijaSoba",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaSportskaAktivnost_RezervacijaID",
                table: "RezervacijaSportskaAktivnost",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaWellnes_RezervacijaID",
                table: "RezervacijaWellnes",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_SalaTipID",
                table: "Sala",
                column: "SalaTipID");

            migrationBuilder.CreateIndex(
                name: "IX_Soba_SobaTipID",
                table: "Soba",
                column: "SobaTipID");

            migrationBuilder.CreateIndex(
                name: "IX_SobaSlike_SobaId",
                table: "SobaSlike",
                column: "SobaId");

            migrationBuilder.CreateIndex(
                name: "IX_SportskaAktivnost_SportskaAktivnostTipID",
                table: "SportskaAktivnost",
                column: "SportskaAktivnostTipID");

            migrationBuilder.CreateIndex(
                name: "IX_SportskaAktivnostSlike_SportskaAktivnostID",
                table: "SportskaAktivnostSlike",
                column: "SportskaAktivnostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bazen");

            migrationBuilder.DropTable(
                name: "BungalovSlike");

            migrationBuilder.DropTable(
                name: "Klijent");

            migrationBuilder.DropTable(
                name: "MeniRestoranSlike");

            migrationBuilder.DropTable(
                name: "RezervacijaMeniRestoran");

            migrationBuilder.DropTable(
                name: "RezervacijaSoba");

            migrationBuilder.DropTable(
                name: "RezervacijaSportskaAktivnost");

            migrationBuilder.DropTable(
                name: "RezervacijaWellnes");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "SobaSlike");

            migrationBuilder.DropTable(
                name: "SpaCentar");

            migrationBuilder.DropTable(
                name: "SportskaAktivnostSlike");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Bungalov");

            migrationBuilder.DropTable(
                name: "MeniRestoran");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Wellnes");

            migrationBuilder.DropTable(
                name: "SalaTip");

            migrationBuilder.DropTable(
                name: "Soba");

            migrationBuilder.DropTable(
                name: "SportskaAktivnost");

            migrationBuilder.DropTable(
                name: "BungalovTip");

            migrationBuilder.DropTable(
                name: "MeniRestoranTip");

            migrationBuilder.DropTable(
                name: "NacinPlacanja");

            migrationBuilder.DropTable(
                name: "Racun");

            migrationBuilder.DropTable(
                name: "StatusRezervacije");

            migrationBuilder.DropTable(
                name: "SobaTip");

            migrationBuilder.DropTable(
                name: "SportskaAktivnostTip");

            migrationBuilder.DropTable(
                name: "KreditnaKartica");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Rola");
        }
    }
}
