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
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore;

namespace SeminarskiRS1.Controllers
{

    public class SobaController : Controller
    {

        private MojDbContext _dbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly ILogger<WellnesController> _logger;
        private readonly IEmailService _emailService;

        public SobaController(MojDbContext x, IWebHostEnvironment webhostEnvironment, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ILogger<WellnesController> logger, IEmailService emailService)
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
        public async Task<IActionResult> PrikazSobaAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            List<SelectListItem> sobaTipovi = _dbContext.SobaTip.Select(x => new SelectListItem
            {
                Value = x.SobaTipID.ToString(),
                Text = x.NazivTipaSobe
            }).ToList();

            List<SobaPrikazVM.Row> sobe = _dbContext.Soba
                .Select(x => new SobaPrikazVM.Row
                {
                    SobaId = x.SobaId,
                    BrojSobe = x.BrojSobe,
                    NazivSobe = x.NazivSobe,
                    SobaTipID = x.SobaTipID,
                    Cijena = x.Cijena,
                    OpisSobe = x.OpisSobe,
                    PutanjaDoSlike = _dbContext.SobaSlike.Where(i => i.SobaId == x.SobaId).FirstOrDefault().URL
                }).ToList();

            SobaPrikazVM x = new SobaPrikazVM();

            x.sobe = sobe;
            x.SobaTipovi = sobaTipovi;
            x.RolaID = user.RolaID;

            return View(x);
        }
        
        [Autorizacija(true, true)]
        public async Task<IActionResult> EvidentirajSobuAsync(int SobaId = 0)
        {
            var user = await _userManager.GetUserAsync(User);

            var soba = new SobaEvidentirajVM();

            List<SelectListItem> sobaTipovi = _dbContext.SobaTip
                .Select(x => new SelectListItem
                    {
                        Value = x.SobaTipID.ToString(),
                        Text = x.NazivTipaSobe
                    })
                .ToList();

            if (SobaId == 0)
            {
                soba = new SobaEvidentirajVM();
            }
            else
            {
                soba = _dbContext.Soba
                    .Where(s => s.SobaId == SobaId)
                    .Select(x => new SobaEvidentirajVM
                    {
                        SobaId = x.SobaId,
                        BrojSobe = x.BrojSobe,
                        NazivSobe = x.NazivSobe,
                        SobaTipID = x.SobaTipID,
                        SobaTip2 = x.SobaTip.NazivTipaSobe,
                        Cijena = x.Cijena,
                        OpisSobe = x.OpisSobe,
                    }).SingleOrDefault();

                soba.PutanjaDoSlikaSobe = _dbContext.SobaSlike.Where(s => s.SobaId == SobaId).Select(i => i.URL).ToList();
            }
            
            soba.SobaId = SobaId;
            soba.SobaTipovi = sobaTipovi;
            soba.RolaID = user.RolaID;

            return View(soba);
        }

        [Autorizacija(false, true)]
        public IActionResult Snimi(SobaEvidentirajVM x)
        {
            if (x == null)
            {
                _logger.LogError($"Room - Not found");
                return NotFound();
            }
            Soba soba = new Soba();

            x.PutanjaDoSlikaSobe = UploadFile(x);

            if (x.SobaId == 0)
            {
                _dbContext.Add(soba);
            }
            else
            {
                soba = _dbContext.Soba.Find(x.SobaId);
            }
            soba.SobaId = x.SobaId;
            soba.BrojSobe = x.BrojSobe;
            soba.NazivSobe = x.NazivSobe;
            soba.SobaTipID = x.SobaTipID;
            soba.Cijena = x.Cijena;
            soba.OpisSobe = x.OpisSobe;

            _dbContext.SaveChanges();

            if(x.PutanjaDoSlikaSobe != null)
            {
                foreach (var i in x.PutanjaDoSlikaSobe)
                {
                    var sobaSlike = new SobaSlike();

                    sobaSlike.URL = i;
                    sobaSlike.SobaId = soba.SobaId;

                    _dbContext.Add(sobaSlike);
                    _dbContext.SaveChanges();
                }
            }
            return Redirect("PrikazSoba");
        }

        [Autorizacija(false, true)]
        public IActionResult ObrisiSobu(int SobaId)
        {

            Soba pronadjena = _dbContext.Soba.Find(SobaId);

            foreach (var x in _dbContext.SobaSlike.Where(x => x.SobaId == SobaId))
            {
                _dbContext.SobaSlike.Remove(x);
            }
            foreach (var x in _dbContext.RezervacijaSoba.Where(x => x.SobaID == SobaId))
            {
                _dbContext.RezervacijaSoba.Remove(x);
            }

            _dbContext.Remove(pronadjena);
            _dbContext.SaveChanges();

            _logger.LogInformation($"Soba obrisana");

            return Redirect("PrikazSoba");
        }

        [Autorizacija(false, true)]
        private List<string> UploadFile(SobaEvidentirajVM x)
        {
            List<string> fileNameList = new List<string>();
            string fileName = null;
            if (x.SobaSlike != null && x.SobaSlike.Count > 0)
            {
                foreach (IFormFile slike in x.SobaSlike)
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
        public async Task<IActionResult> RezervacijaAsync(int SobaId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var soba = _dbContext.Soba.Find(SobaId);
                var sobaTip = _dbContext.SobaTip.Find(soba.SobaTipID);
                var sobaSlike = _dbContext.SobaSlike.FirstOrDefault(a => a.SobaId == SobaId);

                var model = new RezervacijaPrikazVM()
                {
                    ID = SobaId,
                    CijenaNarudzbe = soba.Cijena
                };
                var stavke = new RezervacijaPrikazVM.Rows()
                {
                    Naziv = soba.NazivSobe,
                    Tip = sobaTip.NazivTipaSobe,
                    Opis = soba.OpisSobe,
                    PutanjaDoSlike = sobaSlike.URL
                };
                model.stavke = new List<RezervacijaPrikazVM.Rows>();
                model.stavke.Add(stavke);
                model.Controller = "Soba";

                return View("~/Views/Rezervacija/Prikaz.cshtml", model);
            }
            catch (Exception e)
            {
                throw e;
            }
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

                var x = new RezervacijaSoba();
                x.RezervacijaID = rezervacija.RezervacijaID;
                x.SobaID = m.ID;
                x.Cijena = m.CijenaNarudzbe;
                x.DatumRezervacije = m.dtmDate;
                x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                x.BrojNoci = m.Kolicina;
                _dbContext.Add(x);
                _dbContext.SaveChanges();
            }
            else
            {
                var ima = _dbContext.RezervacijaSoba.Where(r => r.RezervacijaID == postoji.RezervacijaID && r.SobaID == m.ID).FirstOrDefault();

                if (ima == null)
                {
                    var rezervacija = _dbContext.Rezervacija.FirstOrDefault(a => a.KorisnikID == user.Id);
                    var x = new RezervacijaSoba();
                    x.RezervacijaID = rezervacija.RezervacijaID;
                    x.SobaID = m.ID;
                    x.Cijena = m.CijenaNarudzbe;
                    x.DatumRezervacije = m.dtmDate;
                    x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                    x.BrojNoci = m.Kolicina;
                    _dbContext.Add(x);
                    _dbContext.SaveChanges();
                }
            }

            var soba = _dbContext.Soba.Find(m.ID);
            var sobaTip = _dbContext.SobaTip.Find(soba.SobaTipID);
            var sobaSlike = _dbContext.SobaSlike.FirstOrDefault(a=>a.SobaId == m.ID);

            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = soba.NazivSobe,
                Tip = sobaTip.NazivTipaSobe,
                Opis = soba.OpisSobe,
                PutanjaDoSlike = sobaSlike.URL
            };
            m.stavke = new List<RezervacijaPrikazVM.Rows>();
            m.stavke.Add(stavke);

            await _emailService.SendEmailAsync(user.Email, "Rezervacija Sobe",
                "<h2>Uspješno ste poslali Vašu rezervaciju!</h2>" +
                $"<p>Poštovani " + user.Ime + " " + user.Prezime + ", <br />" + "<br />" +
                $"Kako bismo dovršili Vašu rezervaciju, potvrdite svoju e-mail adresu na linku</p>" +
                "<button>Potvrdi prijavu</button>" + "<br />" +
                $"<p>Hvala Vam na Vašoj rezervaciji." + "<br />" +
                $"Srdačno Vas pozdravljamo!" + "<br />" + " <br />" +
                $"Vaš Resort tim</p>");

            return Redirect("PrikazSoba");
        }

    }

}

