using Data.EFModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.EF
{
    public class MojDbContext : IdentityDbContext<Korisnik>
    {
        public MojDbContext(DbContextOptions<MojDbContext> options)
               : base(options)
        {
        }
        public DbSet<Rola> Rola  { get; set; }
        public DbSet<Korisnik> Korisnik  { get; set; }
        public DbSet<Klijent> Klijent  { get; set; }
        public DbSet<Admin> Admin  { get; set; }
        public DbSet<Sala> Sala  { get; set; }
        public DbSet<Soba> Soba  { get; set; }
        public DbSet<SobaSlike> SobaSlike  { get; set; }
        public DbSet<SobaTip> SobaTip  { get; set; }
        public DbSet<SportskaAktivnost> SportskaAktivnost  { get; set; }
        public DbSet<SportskaAktivnostTip> SportskaAktivnostTip  { get; set; }
        public DbSet<SportskaAktivnostSlike> SportskaAktivnostSlike  { get; set; }
        public DbSet<Wellnes> Wellnes { get; set; }
        public DbSet<MeniRestoran> MeniRestoran { get; set; }
        public DbSet<MeniRestoranTip> MeniRestoranTip { get; set; }
        public DbSet<MeniRestoranSlike> MeniRestoranSlike { get; set; }
        public DbSet<SalaTip> SalaTip { get; set; }
        public DbSet<Bazen> Bazen { get; set; }
        public DbSet<SpaCentar> SpaCentar { get; set; }
        public DbSet<Bungalov> Bungalov { get; set; }
        public DbSet<BungalovTip> BungalovTip { get; set; }
        public DbSet<BungalovSlike> BungalovSlike { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<RezervacijaWellnes> RezervacijaWellnes { get; set; }
        public DbSet<RezervacijaMeniRestoran> RezervacijaMeniRestoran { get; set; }
        public DbSet<RezervacijaSoba> RezervacijaSoba { get; set; }
        public DbSet<RezervacijaSportskaAktivnost> RezervacijaSportskaAktivnost { get; set; }
        public DbSet<RezervacijaSala> RezervacijaSala { get; set; }
        public DbSet<RezervacijaBungalov> RezervacijaBungalov { get; set; }
        public DbSet<RezervacijaBazen> RezervacijaBazen { get; set; }
        public DbSet<RezervacijaSpaCentar> RezervacijaSpaCentar { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
             .SelectMany(t => t.GetForeignKeys())
             .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Korisnik>()
            .HasOne<Klijent>(s => s.Klijent)
            .WithOne(ad => ad.Korisnik)
            .HasForeignKey<Klijent>(ad => ad.ID);

            modelBuilder.Entity<Korisnik>()
            .HasOne<Admin>(s => s.Admin)
            .WithOne(ad => ad.Korisnik)
            .HasForeignKey<Admin>(ad => ad.ID);

            modelBuilder.Entity<RezervacijaWellnes>()
                .HasKey(pp => new { pp.WellnesID, pp.RezervacijaID });
            modelBuilder.Entity<RezervacijaSoba>()
                .HasKey(pp => new { pp.SobaID, pp.RezervacijaID });
            modelBuilder.Entity<RezervacijaSportskaAktivnost>()
                .HasKey(pp => new { pp.SportskaAktivnostID, pp.RezervacijaID });
            modelBuilder.Entity<RezervacijaMeniRestoran>()
                .HasKey(pp => new { pp.MeniRestoranID, pp.RezervacijaID });
            modelBuilder.Entity<RezervacijaBazen>()
                .HasKey(pp => new { pp.BazenId, pp.RezervacijaID });
            modelBuilder.Entity<RezervacijaBungalov>()
                .HasKey(pp => new { pp.BungalovId, pp.RezervacijaID });
            modelBuilder.Entity<RezervacijaSala>()
                .HasKey(pp => new { pp.SalaId, pp.RezervacijaID });
            modelBuilder.Entity<RezervacijaSpaCentar>()
                .HasKey(pp => new { pp.SpaCentarId, pp.RezervacijaID });

        }
    }


}
