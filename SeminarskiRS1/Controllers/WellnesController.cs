using cloudscribe.Pagination.Models;
using Data.EF;
using Data.EFModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeminarskiRS1.Helper;
using SeminarskiRS1.Repositories;
using SeminarskiRS1.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SeminarskiRS1.Controllers
{
    [Autorizacija(true, true)]
    public class WellnesController : Controller
    {
        private MojDbContext _dbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly ILogger<WellnesController> _logger;
        private readonly IEmailService _emailService;

        public WellnesController(MojDbContext x, IWebHostEnvironment webhostEnvironment, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ILogger<WellnesController> logger, IEmailService emailService)
        {
            _dbContext = x;
            WebHostEnvironment = webhostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailService = emailService;
        }

        [Autorizacija(true, true)]
        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return View();
        }

        [Autorizacija(true,true)]

        public async Task<IActionResult> PrikazWellnesAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            List<WellnesPrikazVM.Row> wellnes = _dbContext.Wellnes
                .Select(x => new WellnesPrikazVM.Row
                {
                    WellnesId = x.WellnesId,
                    NazivWellnes = x.NazivWellnes,
                    TipWellnes = x.TipWellnes,
                    OpisPrograma = x.OpisPrograma,
                    CijenaTretmana= x.CijenaTretmana,
                    DatumVrijeme = x.DatumVrijeme,
                    PutanjaDoSlikeWellnes = x.PutanjaDoSlikeWellnes
                }).ToList();

            WellnesPrikazVM x = new WellnesPrikazVM();
            x.wellnes = wellnes;
            x.RolaID = user.RolaID;

            return View(x);
        }
      
        [Autorizacija(true, true)]
        public async Task<IActionResult> EvidentirajWellnesAsync(int WellnesId = 0)
        {
            var user = await _userManager.GetUserAsync(User);

            WellnesEvidentirajVM wellnes = new WellnesEvidentirajVM();
            
            if (WellnesId == 0)
            {
                wellnes = new WellnesEvidentirajVM();
            }
            else
            {
                wellnes = _dbContext.Wellnes
                    .Where(s => s.WellnesId == WellnesId)
                    .Select(x => new WellnesEvidentirajVM
                    {
                        WellnesId = x.WellnesId,
                        NazivWellnes = x.NazivWellnes,
                        TipWellnes = x.TipWellnes,
                        OpisPrograma = x.OpisPrograma,
                        CijenaTretmana = x.CijenaTretmana,
                        VremenskoTrajanjeTermina = x.VremenskoTrajanjeTermina,
                        DatumVrijeme = x.DatumVrijeme,
                        PutanjaDoSlikeWellnes = x.PutanjaDoSlikeWellnes
                    }).SingleOrDefault();
            }

            wellnes.WellnesId = WellnesId;
            wellnes.RolaID = user.RolaID;

            return View(wellnes);
        }

        [Autorizacija(false, true)]

        public IActionResult Snimi(WellnesEvidentirajVM x)
        {
            Wellnes wellnes = new Wellnes();

            x.PutanjaDoSlikeWellnes = UploadFile(x);

            if (x.WellnesId == 0)
            {
                _dbContext.Add(wellnes);
            }
            else
            {
                wellnes = _dbContext.Wellnes.Find(x.WellnesId);
            }
            wellnes.WellnesId = x.WellnesId;
            wellnes.NazivWellnes = x.NazivWellnes;
            wellnes.TipWellnes = x.TipWellnes;
            wellnes.OpisPrograma = x.OpisPrograma;
            wellnes.CijenaTretmana = x.CijenaTretmana;
            wellnes.VremenskoTrajanjeTermina = x.VremenskoTrajanjeTermina;
            wellnes.DatumVrijeme = x.DatumVrijeme;
            if (!string.IsNullOrEmpty(x.PutanjaDoSlikeWellnes))
                wellnes.PutanjaDoSlikeWellnes = x.PutanjaDoSlikeWellnes;

            _dbContext.SaveChanges();
            return Redirect("PrikazWellnes");
        }

        [Autorizacija(false, true)]
        public IActionResult ObrisiWellnes(int WellnesId)
        {

            Wellnes pronadjena = _dbContext.Wellnes.Find(WellnesId);
            foreach (var x in _dbContext.RezervacijaWellnes.Where(x => x.WellnesID == WellnesId))
            {
                _dbContext.RezervacijaWellnes.Remove(x);
            }

            _dbContext.Remove(pronadjena);
            _dbContext.SaveChanges();

            return Redirect("PrikazWellnes");
        }
        [Autorizacija(false, true)]
        private string UploadFile(WellnesEvidentirajVM x)
        {
            string fileName = null;
            if (x.SlikaWellnes != null)
            {
                foreach (IFormFile wellness in x.SlikaWellnes)
                {
                    string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Slike");
                    fileName = Guid.NewGuid().ToString() + "-" + wellness.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        wellness.CopyTo(fileStream);
                    }
                }
            }
            return fileName;
        }

        [Autorizacija(true, false)]
        public async Task<IActionResult> RezervacijaAsync(int WellnesId)
        {
            var user = await _userManager.GetUserAsync(User);

            var wellnes = _dbContext.Wellnes.Find(WellnesId);

            var model = new RezervacijaPrikazVM()
            {
                ID = WellnesId,
                CijenaNarudzbe = wellnes.CijenaTretmana,

            };
            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = wellnes.NazivWellnes,
                Tip = wellnes.TipWellnes,
                Opis = wellnes.OpisPrograma,
                PutanjaDoSlike = wellnes.PutanjaDoSlikeWellnes
            };

            model.stavke = new List<RezervacijaPrikazVM.Rows>();
            model.stavke.Add(stavke);
            model.Controller = "Wellnes";

            return View("~/Views/Rezervacija/Prikaz.cshtml", model);
        }

        [Autorizacija(true, false)]
        public async Task<IActionResult> SnimiRezervacijuAsync(RezervacijaPrikazVM m)
        {
            var user = await _userManager.GetUserAsync(User);

            var postoji = _dbContext.Rezervacija.FirstOrDefault(a=>a.KorisnikID==user.Id);
            
            if (postoji == null)
            {
                var rezervacija = new Rezervacija();
                rezervacija.KorisnikID = user.Id;
                _dbContext.Add(rezervacija);
                _dbContext.SaveChanges();

                var x = new RezervacijaWellnes();
                x.RezervacijaID = rezervacija.RezervacijaID;
                x.WellnesID = m.ID;
                x.CijenaTretmana = m.CijenaNarudzbe;
                x.TerminRezervacije = m.dtmDate;
                x.UkupnaCijena = m.CijenaNarudzbe*m.Kolicina;
                x.BrojTretmana = m.Kolicina;
                _dbContext.Add(x);
                _dbContext.SaveChanges();
            }
            else
            {
                var ima = _dbContext.RezervacijaWellnes.Where(r => r.RezervacijaID == postoji.RezervacijaID && r.WellnesID == m.ID).FirstOrDefault();

                if (ima == null)
                {
                    var rezervacija = _dbContext.Rezervacija.FirstOrDefault(a => a.KorisnikID == user.Id);
                    var x = new RezervacijaWellnes();
                    x.RezervacijaID = rezervacija.RezervacijaID;
                    x.WellnesID = m.ID;
                    x.CijenaTretmana = m.CijenaNarudzbe;
                    x.TerminRezervacije = m.dtmDate;
                    x.UkupnaCijena = m.CijenaNarudzbe*m.Kolicina;
                    x.BrojTretmana = m.Kolicina;
                    _dbContext.Add(x);
                    _dbContext.SaveChanges();
                }
            }

            var wellnes = _dbContext.Wellnes.Find(m.ID);

            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = wellnes.NazivWellnes,
                Tip = wellnes.TipWellnes,
                Opis = wellnes.OpisPrograma,
                PutanjaDoSlike = wellnes.PutanjaDoSlikeWellnes
            };
            m.stavke = new List<RezervacijaPrikazVM.Rows>();
            m.stavke.Add(stavke);

            await _emailService.SendEmailAsync(user.Email, "Rezervacija Wellnesa",
                "<h2>Uspješno ste poslali Vašu rezervaciju!</h2>" +
                $"<p>Poštovani " + user.Ime + " " + user.Prezime + ", <br />" + "<br />" +
                $"Kako bismo dovršili Vašu rezervaciju, potvrdite svoju e-mail adresu na linku</p>" + 
                "<button>Potvrdi prijavu</button>" + "<br />" +
                $"<p>Hvala Vam na Vašoj rezervaciji." + "<br />" +
                $"Srdačno Vas pozdravljamo!" + "<br />" + " <br />" +
                $"Vaš Resort tim</p>");

            return Redirect("PrikazWellnes");
        }

    }

}

