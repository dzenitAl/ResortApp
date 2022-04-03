using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.EF;
using Data.EFModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeminarskiRS1.Helper;
using SeminarskiRS1.ViewModels;

namespace SeminarskiRS1.Controllers
{
    [Autorizacija(true, true)]
    public class SpaCentarController : Controller
    {
        private MojDbContext _dbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly ILogger<WellnesController> _logger;
        public SpaCentarController(MojDbContext x, IWebHostEnvironment webhostEnvironment, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ILogger<WellnesController> logger)
        {
            _dbContext = x;
            WebHostEnvironment = webhostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [Autorizacija(true, true)]

        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return View();
        }
        public async Task<IActionResult> PrikazSpaCentraAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            List<SpaCentarPrikazVM.Row> centri = _dbContext.SpaCentar
                .Select(x => new SpaCentarPrikazVM.Row
                {
                    SpaCentarId = x.SpaCentarID,
                    OpisSpaCentra = x.Opis,
                    NazivSpaCentra = x.Naziv,
                    CijenaZakupljivanjaSpaCentra = x.CijenaZakupa,
                    PutanjaDoSlike = x.PutanjaDoSlike
                }).ToList();

            SpaCentarPrikazVM x = new SpaCentarPrikazVM();
            x.centri = centri;
            x.RolaID = user.RolaID;

            return View(x);
        }
        [Autorizacija(true, true)]
        public async Task<IActionResult> EvidentirajSpaCentarAsync(int SpaCentarID = 0)
        {
            var user = await _userManager.GetUserAsync(User);

            SpaCentarEvidentirajVM centar = new SpaCentarEvidentirajVM();

            if (SpaCentarID == 0)
            {
                centar = new SpaCentarEvidentirajVM();
            }
            else
            {
                centar = _dbContext.SpaCentar
                    .Where(s => s.SpaCentarID == SpaCentarID)
                    .Select(c => new SpaCentarEvidentirajVM
                    {
                        SpaCentarId = c.SpaCentarID,
                        NazivCentra = c.Naziv,
                        CijenaZakupljivanjaCentra = c.CijenaZakupa,
                        OpisCentra = c.Opis,
                        PutanjaDoSlike = c.PutanjaDoSlike

                    }).SingleOrDefault();
            }

            centar.SpaCentarId = SpaCentarID;
            centar.RolaID = user.RolaID;

            return View(centar);
        }

        [Autorizacija(false, true)]
        public IActionResult Snimi(SpaCentarEvidentirajVM x)
        {
            SpaCentar centri = new SpaCentar();
            x.PutanjaDoSlike = UploadFile(x);
            if (x.SpaCentarId == 0)
            {
                _dbContext.Add(centri);
            }
            else
            {
                centri = _dbContext.SpaCentar.Find(x.SpaCentarId);
            }
            centri.SpaCentarID = x.SpaCentarId;
            centri.Naziv = x.NazivCentra;
            centri.Opis = x.OpisCentra;
            centri.CijenaZakupa = x.CijenaZakupljivanjaCentra;
            if (!string.IsNullOrEmpty(x.PutanjaDoSlike))
                centri.PutanjaDoSlike = x.PutanjaDoSlike;
            _dbContext.SaveChanges();
            return Redirect("PrikazSpaCentra");
        }
        [Autorizacija(false, true)]
        public IActionResult ObrisiSpaCentar(int SpaCentarID)
        {

            SpaCentar pronadjen = _dbContext.SpaCentar.Find(SpaCentarID);
            foreach (var x in _dbContext.RezervacijaSpaCentar.Where(x => x.SpaCentarId == SpaCentarID))
            {
                _dbContext.RezervacijaSpaCentar.Remove(x);
            }
            
            _dbContext.Remove(pronadjen);
            _dbContext.SaveChanges();

            return Redirect("PrikazSpaCentra");
        }
        [Autorizacija(false, true)]
        private string UploadFile(SpaCentarEvidentirajVM x)
        {
            string fileName = null;
            if (x.CentarSlika != null)
            {
                foreach (IFormFile sala in x.CentarSlika)
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

                var x = new RezervacijaSpaCentar();
                x.RezervacijaID = rezervacija.RezervacijaID;
                x.SpaCentarId = m.ID;
                x.CijenaTretmana = m.CijenaNarudzbe;
                x.TerminRezervacije = m.dtmDate;
                x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                x.BrojTretmana = m.Kolicina;
                _dbContext.Add(x);
                _dbContext.SaveChanges();
            }
            else
            {
                var ima = _dbContext.RezervacijaSpaCentar.Where(r => r.RezervacijaID == postoji.RezervacijaID && r.SpaCentarId == m.ID).FirstOrDefault();

                if (ima == null)
                {
                    var rezervacija = _dbContext.Rezervacija.FirstOrDefault(a => a.KorisnikID == user.Id);
                    var x = new RezervacijaSpaCentar();
                    x.RezervacijaID = rezervacija.RezervacijaID;
                    x.SpaCentarId = m.ID;
                    x.CijenaTretmana = m.CijenaNarudzbe;
                    x.TerminRezervacije = m.dtmDate;
                    x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                    x.BrojTretmana = m.Kolicina;
                    _dbContext.Add(x);
                    _dbContext.SaveChanges();
                }
            }

            var spacentar = _dbContext.SpaCentar.Find(m.ID);

            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = spacentar.Naziv,
                //i ovdje nema tip??????????
                Opis = spacentar.Opis,
                PutanjaDoSlike = spacentar.PutanjaDoSlike
            };
            m.stavke = new List<RezervacijaPrikazVM.Rows>();
            m.stavke.Add(stavke);

            return Redirect("PrikazSpaCentra");
        }

        [Autorizacija(true, false)]
        public async Task<IActionResult> RezervacijaAsync(int SpaCentarId)
        {
            var user = await _userManager.GetUserAsync(User);

            var spacentar = _dbContext.SpaCentar.Find(SpaCentarId);

            var model = new RezervacijaPrikazVM()
            {
                ID = SpaCentarId,
                CijenaNarudzbe = spacentar.CijenaZakupa,

            };
            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = spacentar.Naziv,
                //Tip = wellnes.TipWellnes,
                Opis = spacentar.Opis,
                PutanjaDoSlike = spacentar.PutanjaDoSlike
            };

            model.stavke = new List<RezervacijaPrikazVM.Rows>();
            model.stavke.Add(stavke);
            model.Controller = "SpaCentar";

            return View("~/Views/Rezervacija/Prikaz.cshtml", model);
        }
    }
}
