using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.EF;
using Data.EFModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SeminarskiRS1.Helper;
using SeminarskiRS1.Repositories;
using SeminarskiRS1.ViewModels;

namespace SeminarskiRS1.Controllers
{
    public class SportskaAktivnostController : Controller
    {
        private MojDbContext _dbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly ILogger<WellnesController> _logger;
        private readonly IEmailService _emailService;

        public SportskaAktivnostController(MojDbContext x, IWebHostEnvironment webhostEnvironment, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ILogger<WellnesController> logger, IEmailService emailService)
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

        [Autorizacija(true, true)]
        public async Task<IActionResult> PrikazSportskaAktivnostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            List<SelectListItem> tipAktivnosti = _dbContext.SportskaAktivnostTip.Select(x => new SelectListItem
            {
                Value = x.SportskaAktivnostTipID.ToString(),
                Text = x.NazivTipaAktivnosti
            }).ToList();

            List<SportskaAktivnostPrikazVM.Row> sportskaAktivnost = _dbContext.SportskaAktivnost
                .Select(x => new SportskaAktivnostPrikazVM.Row
                {
                    SportskaAktivnostID = x.SportskaAktivnostID,
                    SportskaAktivnostiTipID = x.SportskeAktivnostiTipID,
                    OpisPrograma = x.OpisPrograma,
                    NazivAktivnosti = x.NazivAktivnosti,
                    CijenaAktivnosti = x.CijenaAktivnosti,
                    PutanjaDoSlike = _dbContext.SportskaAktivnostSlike.Where(i => i.SportskaAktivnostID == x.SportskaAktivnostID).FirstOrDefault().URL
                }).ToList();

            SportskaAktivnostPrikazVM x = new SportskaAktivnostPrikazVM();

            x.sportskaAktivnost = sportskaAktivnost;
            x.SportskaAktivnostTip = tipAktivnosti;
            x.RolaID = user.RolaID;

            return View(x);
        }

        [Autorizacija(true, true)]
        public async Task<IActionResult> EvidentirajSportskaAktivnostAsync(int SportskaAktivnostId = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            List<SelectListItem> tipAktivnosti = _dbContext.SportskaAktivnostTip.Select(x => new SelectListItem
            {
                Value = x.SportskaAktivnostTipID.ToString(),
                Text = x.NazivTipaAktivnosti
            }).ToList();

            SportskaAktivnostEvidentirajVM sportskaAktivnost = new SportskaAktivnostEvidentirajVM();
            if (SportskaAktivnostId == 0)
            {
                sportskaAktivnost = new SportskaAktivnostEvidentirajVM();
            }
            else
            {
                sportskaAktivnost = _dbContext.SportskaAktivnost
                    .Where(s => s.SportskaAktivnostID == SportskaAktivnostId)
                    .Select(x => new SportskaAktivnostEvidentirajVM
                    {
                        SportskaAktivnostID = x.SportskaAktivnostID,
                        SportskaAktivnostiTipID = x.SportskeAktivnostiTipID,
                        SportskaAktivnostTip2 = x.SportskaAktivnostTip.NazivTipaAktivnosti,
                        OpisPrograma = x.OpisPrograma,
                        NazivAktivnosti = x.NazivAktivnosti,
                        CijenaAktivnosti = x.CijenaAktivnosti
                    }).SingleOrDefault();

                sportskaAktivnost.PutanjaDoSlikaSportskeAktivnosti = _dbContext.SportskaAktivnostSlike.Where(s => s.SportskaAktivnostID == SportskaAktivnostId).Select(i => i.URL).ToList();
            }

            sportskaAktivnost.SportskaAktivnostID = SportskaAktivnostId;
            sportskaAktivnost.SportskaAktivnostTip = tipAktivnosti;
            sportskaAktivnost.RolaID = user.RolaID;

            return View(sportskaAktivnost);
        }

        [Autorizacija(false, true)]
        public IActionResult Snimi(SportskaAktivnostEvidentirajVM x)
        {
            SportskaAktivnost sportskaAktivnost = new SportskaAktivnost();

            x.PutanjaDoSlikaSportskeAktivnosti = UploadFile(x);

            if (x.SportskaAktivnostID == 0)
            {
                _dbContext.Add(sportskaAktivnost);
            }
            else
            {
                sportskaAktivnost = _dbContext.SportskaAktivnost.Find(x.SportskaAktivnostID);
            }
            sportskaAktivnost.SportskaAktivnostID = x.SportskaAktivnostID;
            sportskaAktivnost.SportskeAktivnostiTipID = x.SportskaAktivnostiTipID;
            sportskaAktivnost.OpisPrograma = x.OpisPrograma;
            sportskaAktivnost.NazivAktivnosti = x.NazivAktivnosti;
            sportskaAktivnost.CijenaAktivnosti = x.CijenaAktivnosti;
            
            _dbContext.SaveChanges();

            if (x.PutanjaDoSlikaSportskeAktivnosti != null)
            {
                foreach (var i in x.PutanjaDoSlikaSportskeAktivnosti)
                {
                    var sportskaAktivnostSlike = new SportskaAktivnostSlike();

                    sportskaAktivnostSlike.URL = i;
                    sportskaAktivnostSlike.SportskaAktivnostID = sportskaAktivnost.SportskaAktivnostID;

                    _dbContext.Add(sportskaAktivnostSlike);
                    _dbContext.SaveChanges();
                }
            }

            return Redirect("PrikazSportskaAktivnost");
        }

        [Autorizacija(false, true)]
        public IActionResult ObrisiSportskuAktivnost(int SportskaAktivnostId)
        {

            SportskaAktivnost pronadjena = _dbContext.SportskaAktivnost.Find(SportskaAktivnostId);
           
            foreach (var x in _dbContext.SportskaAktivnostSlike.Where(x => x.SportskaAktivnostID == SportskaAktivnostId))
            {
                _dbContext.SportskaAktivnostSlike.Remove(x);
            }
            foreach (var x in _dbContext.RezervacijaSportskaAktivnost.Where(x => x.SportskaAktivnostID == SportskaAktivnostId))
            {
                _dbContext.RezervacijaSportskaAktivnost.Remove(x);
            }

            _dbContext.Remove(pronadjena);
            _dbContext.SaveChanges();

            return Redirect("PrikazSportskaAktivnost");
        }

        [Autorizacija(false, true)]
        private List<string> UploadFile(SportskaAktivnostEvidentirajVM x)
        {
            List<string> fileNameList = new List<string>();
            string fileName = null;
            if (x.SportskaAktivnostSlika != null && x.SportskaAktivnostSlika.Count > 0)
            {
                foreach (IFormFile slike in x.SportskaAktivnostSlika)
                {
                    string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Slike");
                    fileName = Guid.NewGuid().ToString() + "-" + slike.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        slike.CopyTo(fileStream);
                    }
                    fileNameList.Add(fileName);
                }
            }
            return fileNameList;
        }

        [Autorizacija(true, false)]
        public async Task<IActionResult> RezervacijaAsync(int SportskaAktivnostID)
        {
            var user = await _userManager.GetUserAsync(User);

            var sportskaAktivnost = _dbContext.SportskaAktivnost.Find(SportskaAktivnostID);
            var sportskaAktivnostTip = _dbContext.SportskaAktivnostTip.Find(sportskaAktivnost.SportskeAktivnostiTipID);
            var sportskaAktivnostSlike = _dbContext.SportskaAktivnostSlike.FirstOrDefault(a => a.SportskaAktivnostID == SportskaAktivnostID);

            var model = new RezervacijaPrikazVM()
            {
                ID = SportskaAktivnostID,
                CijenaNarudzbe = sportskaAktivnost.CijenaAktivnosti
            };
            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = sportskaAktivnost.NazivAktivnosti,
                Tip = sportskaAktivnostTip.NazivTipaAktivnosti,
                Opis = sportskaAktivnost.OpisPrograma,
                PutanjaDoSlike = sportskaAktivnostSlike.URL
            };
            model.stavke = new List<RezervacijaPrikazVM.Rows>();
            model.stavke.Add(stavke);
            model.Controller = "SportskaAktivnost";

            return View("~/Views/Rezervacija/Prikaz.cshtml", model);
        }

        [Autorizacija(true, false)]
        public async Task<IActionResult> SnimiRezervacijuAsync(RezervacijaPrikazVM m)
        {
            var user = await _userManager.GetUserAsync(User);

            var postoji = _dbContext.Rezervacija.FirstOrDefault(a => a.KorisnikID == user.Id);

            if (postoji == null)
            {
                var rezervacija = new Rezervacija();
                rezervacija.KorisnikID = user.Id;
                _dbContext.Add(rezervacija);
                _dbContext.SaveChanges();

                var x = new RezervacijaSportskaAktivnost();
                x.RezervacijaID = rezervacija.RezervacijaID;
                x.SportskaAktivnostID = m.ID;
                x.CijenaTermina = m.CijenaNarudzbe;
                x.TerminRezervacije = m.dtmDate;
                x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                x.BrojTermina = m.Kolicina;
                _dbContext.Add(x);
                _dbContext.SaveChanges();
            }
            else
            {
                var ima = _dbContext.RezervacijaSportskaAktivnost.Where(r => r.RezervacijaID == postoji.RezervacijaID && r.SportskaAktivnostID == m.ID).FirstOrDefault();

                if (ima == null)
                {
                    var rezervacija = _dbContext.Rezervacija.FirstOrDefault(a => a.KorisnikID == user.Id);
                    var x = new RezervacijaSportskaAktivnost();
                    x.RezervacijaID = rezervacija.RezervacijaID;
                    x.SportskaAktivnostID = m.ID;
                    x.CijenaTermina = m.CijenaNarudzbe;
                    x.TerminRezervacije = m.dtmDate;
                    x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                    x.BrojTermina = m.Kolicina;
                    _dbContext.Add(x);
                    _dbContext.SaveChanges();
                }
            }

            var sportskaAktivnost = _dbContext.SportskaAktivnost.Find(m.ID);
            var sportskaAktivnostTip = _dbContext.SportskaAktivnostTip.Find(sportskaAktivnost.SportskeAktivnostiTipID);
            var sportskaAktivnostSlike = _dbContext.SportskaAktivnostSlike.FirstOrDefault(a => a.SportskaAktivnostID == m.ID);

            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = sportskaAktivnost.NazivAktivnosti,
                Tip = sportskaAktivnostTip.NazivTipaAktivnosti,
                Opis = sportskaAktivnost.OpisPrograma,
                PutanjaDoSlike = sportskaAktivnostSlike.URL
            };
            m.stavke = new List<RezervacijaPrikazVM.Rows>();
            m.stavke.Add(stavke);

            await _emailService.SendEmailAsync(user.Email, "Rezervacija Sportske aktivnosti",
                "<h2>Uspješno ste poslali Vašu rezervaciju!</h2>" + 
                $"<p>Poštovani " + user.Ime + " " + user.Prezime + ", <br />" + "<br />" +
                $"Kako bismo dovršili Vašu rezervaciju, potvrdite svoju e-mail adresu na linku</p>" +
                "<button>Potvrdi prijavu</button>" + "<br />" +
                $"<p>Hvala Vam na Vašoj rezervaciji." + "<br />" +
                $"Srdačno Vas pozdravljamo!" + "<br />" + " <br />" +
                $"Vaš Resort tim</p>");

            return Redirect("PrikazSportskaAktivnost");
        }

    }

}
