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
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SeminarskiRS1.Helper;
using SeminarskiRS1.SignalR;
using SeminarskiRS1.ViewModels;

namespace SeminarskiRS1.Controllers
{
    [Autorizacija(true, true)]
    public class BazenController : Controller
    {
        private MojDbContext _dbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly ILogger<WellnesController> _logger;
        private IHubContext<MyHub> _hubContext;

        public BazenController(MojDbContext x, IHubContext<MyHub> hubContext, IWebHostEnvironment webhostEnvironment, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ILogger<WellnesController> logger)
        {
            _dbContext = x;
            WebHostEnvironment = webhostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _hubContext = hubContext;
        }
        public IActionResult ResetCountKorisnik(string Email)
        {
            var id = _dbContext.Users.Where(i => i.Email == Email).FirstOrDefault().Id;
            var korisnik = _dbContext.Users.Find(id);
            korisnik.brojNotifikacija = 0;

            _dbContext.Update(korisnik);
            _dbContext.SaveChanges();

            return RedirectToAction("/Bazen/PrikazBazena");
        }
        [Autorizacija(true, true)]
        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return View();
        }
        [Autorizacija(true, true)]
        public async Task<IActionResult> PrikazBazenaAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            List<BazenPrikazVM.Row> bazeni = _dbContext.Bazen
                .Select(x => new BazenPrikazVM.Row
                {
                    BazenId = x.BazenID,
                    OpisBazena = x.Opis,
                    NazivBazena = x.NazivBazena,
                    Cijena = x.Cijena,
                    PutanjaDoSlike = x.PutanjaDoSlikeSale
                }).ToList();

            BazenPrikazVM x = new BazenPrikazVM();
            x.bazeni = bazeni;
            x.RolaID = user.RolaID;

            return View(x);
        }
        [Autorizacija(true, true)]
        public async Task<IActionResult> EvidentirajBazenAsync(int BazenID = 0)
        {
            var user = await _userManager.GetUserAsync(User);

            BazenEvidentirajVM bazen = new BazenEvidentirajVM();

            if (BazenID == 0)
            {
                bazen = new BazenEvidentirajVM();
            }
            else
            {
                bazen = _dbContext.Bazen
                    .Where(s => s.BazenID == BazenID)
                    .Select(c => new BazenEvidentirajVM
                    {
                        BazenId = c.BazenID,
                        NazivBazena = c.NazivBazena,
                        CijenaIznajmljivanjaBazena = c.Cijena,
                        OpisBazena = c.Opis,
                        PutanjaDoSlike = c.PutanjaDoSlikeSale

                    }).SingleOrDefault();
            }

            bazen.BazenId = BazenID;
            bazen.RolaID = user.RolaID;

            return View(bazen);
        }
        [Autorizacija(false, true)]
        public IActionResult Snimi(BazenEvidentirajVM x)
        {

            Bazen bazeni = new Bazen();
            x.PutanjaDoSlike = UploadFile(x);
            if (x.BazenId == 0)
            {
                _dbContext.Add(bazeni);

                var korisnici = _dbContext.Users.ToList();
                _hubContext.Clients.All.SendAsync("prijemPoruke", "dodanaNovost");

                foreach (var i in korisnici)
                {
                    i.brojNotifikacija = ++i.brojNotifikacija;
                    _dbContext.Users.Update(i);
                }
            }
            else
            {
                bazeni = _dbContext.Bazen.Find(x.BazenId);
            }
            bazeni.BazenID = x.BazenId;
            bazeni.NazivBazena = x.NazivBazena;
            bazeni.Opis = x.OpisBazena;
            bazeni.Cijena = x.CijenaIznajmljivanjaBazena;
            if (!string.IsNullOrEmpty(x.PutanjaDoSlike))
                bazeni.PutanjaDoSlikeSale = x.PutanjaDoSlike;
            _dbContext.SaveChanges();
            return Redirect("PrikazBazena");
        }
        [Autorizacija(false, true)]
        public IActionResult ObrisiBazen(int BazenID)
        {

            Bazen pronadjen = _dbContext.Bazen.Find(BazenID);
            foreach (var x in _dbContext.RezervacijaBazen.Where(x => x.BazenId == BazenID))
            {
                _dbContext.RezervacijaBazen.Remove(x);
            }
            _dbContext.Remove(pronadjen);
            _dbContext.SaveChanges();

            return Redirect("PrikazBazena");
        }
        [Autorizacija(false, true)]
        private string UploadFile(BazenEvidentirajVM x)
        {
            string fileName = null;
            if (x.BazenSlika != null)
            {
                foreach (IFormFile sala in x.BazenSlika)
                {
                    string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Slike");
                    fileName = Guid.NewGuid().ToString() + "-" + sala.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        sala.CopyTo(fileStream);
                    }
                }
            }
            return fileName;
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

                var x = new RezervacijaBazen();
                x.RezervacijaID = rezervacija.RezervacijaID;
                x.BazenId = m.ID;
                x.Cijena = m.CijenaNarudzbe;
                x.TerminRezervacije = m.dtmDate;
                x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                x.BrojLjudi = m.Kolicina;
                _dbContext.Add(x);
                _dbContext.SaveChanges();
            }
            else
            {
                var ima = _dbContext.RezervacijaBazen.Where(r => r.RezervacijaID == postoji.RezervacijaID && r.BazenId == m.ID).FirstOrDefault();

                if (ima == null)
                {
                    var rezervacija = _dbContext.Rezervacija.FirstOrDefault(a => a.KorisnikID == user.Id);
                    var x = new RezervacijaBazen();
                    x.RezervacijaID = rezervacija.RezervacijaID;
                    x.BazenId = m.ID;
                    x.Cijena = m.CijenaNarudzbe;
                    x.TerminRezervacije = m.dtmDate;
                    x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                    x.BrojLjudi = m.Kolicina;
                    _dbContext.Add(x);
                    _dbContext.SaveChanges();
                }
            }

            var bazen = _dbContext.Bazen.Find(m.ID);

            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = bazen.NazivBazena,
                Opis = bazen.Opis,
                //ovdje nema tipa sta tu da se stavi????????????????
                PutanjaDoSlike = bazen.PutanjaDoSlikeSale
            };
            m.stavke = new List<RezervacijaPrikazVM.Rows>();
            m.stavke.Add(stavke);

            return Redirect("PrikazBazena");
        }

        [Autorizacija(true, false)]
        public async Task<IActionResult> RezervacijaAsync(int BazenId)
        {
            var user = await _userManager.GetUserAsync(User);

            var bazen = _dbContext.Bazen.Find(BazenId);

            var model = new RezervacijaPrikazVM()
            {
                ID = BazenId,
                CijenaNarudzbe = bazen.Cijena,

            };
            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = bazen.NazivBazena,
                //Tip = wellnes.TipWellnes,
                Opis = bazen.Opis,
                PutanjaDoSlike = bazen.PutanjaDoSlikeSale
            };

            model.stavke = new List<RezervacijaPrikazVM.Rows>();
            model.stavke.Add(stavke);
            model.Controller = "Bazen";

            return View("~/Views/Rezervacija/Prikaz.cshtml", model);
        }
    }
}
