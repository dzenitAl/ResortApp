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
    [Autorizacija(true, true)]
    public class MeniRestoranController : Controller
    {
        private MojDbContext _dbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly ILogger<WellnesController> _logger;
        private readonly IEmailService _emailService;

        public MeniRestoranController(MojDbContext x, IWebHostEnvironment webhostEnvironment, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ILogger<WellnesController> logger, IEmailService emailService)
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
        public async Task<IActionResult> PrikazMeniRestoranAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            List<SelectListItem> meniRestoranTip = _dbContext.MeniRestoranTip.Select(x => new SelectListItem
            {
                Value = x.MeniRestoranTipID.ToString(),
                Text = x.NazivTipaMenija
            }).ToList();

            List<MeniRestoranPrikazVM.Row> meniRestoran = _dbContext.MeniRestoran
                .Select(x => new MeniRestoranPrikazVM.Row
                {
                    MeniRestoranID = x.MeniRestoranID,
                    Restoran = x.Restoran,
                    MeniRestoranTipID = x.MeniRestoranTipID,
                    OpisMenija= x.OpisMenija,
                    CijenaMenija= x.CijenaMenija,
                    PutanjaDoSlike = _dbContext.MeniRestoranSlike.Where(i => i.MeniRestoranID == x.MeniRestoranID).FirstOrDefault().URL
                }).ToList();


            MeniRestoranPrikazVM x = new MeniRestoranPrikazVM();

            x.meniRestoran = meniRestoran;
            x.MeniRestoranTip = meniRestoranTip;
            x.RolaID = user.RolaID;

            return View(x);
        }

        [Autorizacija(true, true)]
        public async Task<IActionResult> EvidentirajMeniRestoranAsync(int MeniRestoranID = 0)
        {
            var user = await _userManager.GetUserAsync(User);

            var meniRestoran = new MeniRestoranEvidentirajVM();
            List<SelectListItem> meniRestoranTip = _dbContext.MeniRestoranTip.Select(x => new SelectListItem
            {
                Value = x.MeniRestoranTipID.ToString(),
                Text = x.NazivTipaMenija
            }).ToList();

            if (MeniRestoranID == 0)
            {
                meniRestoran = new MeniRestoranEvidentirajVM();
            }
            else
            {
                meniRestoran = _dbContext.MeniRestoran
                    .Where(s => s.MeniRestoranID == MeniRestoranID)
                    .Select(x => new MeniRestoranEvidentirajVM
                    {
                        MeniRestoranID = x.MeniRestoranID,
                        Restoran = x.Restoran,
                        MeniRestoranTipID = x.MeniRestoranTipID,
                        MeniRestoranTip2 = x.MeniRestoranTip.NazivTipaMenija,
                        OpisMenija = x.OpisMenija,
                        CijenaMenija = x.CijenaMenija
                    }).SingleOrDefault();
                meniRestoran.PutanjaDoSlikaMeniRestorana = _dbContext.MeniRestoranSlike.Where(s => s.MeniRestoranID == MeniRestoranID).Select(i => i.URL).ToList();
            }

            meniRestoran.MeniRestoranID = MeniRestoranID;
            meniRestoran.MeniRestoranTip = meniRestoranTip;
            meniRestoran.RolaID = user.RolaID;

            return View(meniRestoran);
        }
        [Autorizacija(false, true)]
        public IActionResult Snimi(MeniRestoranEvidentirajVM x)
        {
            MeniRestoran meniRestoran = new MeniRestoran();

            x.PutanjaDoSlikaMeniRestorana = UploadFile(x);

            if (x.MeniRestoranID == 0)
            {
                _dbContext.Add(meniRestoran);
            }
            else
            {
                meniRestoran = _dbContext.MeniRestoran.Find(x.MeniRestoranID);
            }
            meniRestoran.MeniRestoranID = x.MeniRestoranID;
            meniRestoran.Restoran = x.Restoran;
            meniRestoran.MeniRestoranTipID = x.MeniRestoranTipID;
            meniRestoran.OpisMenija = x.OpisMenija;
            meniRestoran.CijenaMenija = x.CijenaMenija;

            _dbContext.SaveChanges();

            if (x.PutanjaDoSlikaMeniRestorana != null)
            {
                foreach (var i in x.PutanjaDoSlikaMeniRestorana)
                {
                    var meniRestoranSlike = new MeniRestoranSlike();

                    meniRestoranSlike.URL = i;
                    meniRestoranSlike.MeniRestoranID = meniRestoran.MeniRestoranID;

                    _dbContext.Add(meniRestoranSlike);
                    _dbContext.SaveChanges();
                }
            }
            return Redirect("PrikazMeniRestoran");
        }
        [Autorizacija(false, true)]
        public IActionResult ObrisiMeniRestoran(int MeniRestoranId)
        {
            MeniRestoran pronadjena = _dbContext.MeniRestoran.Find(MeniRestoranId);

            foreach (var x in _dbContext.MeniRestoranSlike.Where(x => x.MeniRestoranID == MeniRestoranId))
            {
                _dbContext.MeniRestoranSlike.Remove(x);
            }
            foreach (var x in _dbContext.RezervacijaMeniRestoran.Where(x => x.MeniRestoranID == MeniRestoranId))
            {
                _dbContext.RezervacijaMeniRestoran.Remove(x);
            }

            _dbContext.Remove(pronadjena);
            _dbContext.SaveChanges();

            return Redirect("PrikazMeniRestoran");
        }

        [Autorizacija(false, true)]
        private List<string> UploadFile(MeniRestoranEvidentirajVM x)
        {
            List<string> fileNameList = new List<string>();
            string fileName = null;
            if (x.MeniRestoranSlike != null && x.MeniRestoranSlike.Count > 0)
            {
                foreach (IFormFile slike in x.MeniRestoranSlike)
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
        public async Task<IActionResult> RezervacijaAsync(int MeniRestoranID)
        {
            var user = await _userManager.GetUserAsync(User);

            var meniRestoran = _dbContext.MeniRestoran.Find(MeniRestoranID);
            var meniRestoranTip = _dbContext.MeniRestoranTip.Find(meniRestoran.MeniRestoranTipID);
            var meniRestoranSlika = _dbContext.MeniRestoranSlike.Find(MeniRestoranID);

            var model = new RezervacijaPrikazVM()
            {
                ID = MeniRestoranID,
                CijenaNarudzbe = meniRestoran.CijenaMenija,

            };
            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = meniRestoran.Restoran + meniRestoranTip.NazivTipaMenija,
                Tip = meniRestoranTip.NazivTipaMenija,
                Opis = meniRestoran.OpisMenija,
                PutanjaDoSlike = meniRestoranSlika.URL
            };
            model.stavke = new List<RezervacijaPrikazVM.Rows>();
            model.stavke.Add(stavke);
            model.Controller = "MeniRestoran";

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

                var x = new RezervacijaMeniRestoran();
                x.RezervacijaID = rezervacija.RezervacijaID;
                x.MeniRestoranID = m.ID;
                x.CijenaTretmana = m.CijenaNarudzbe;
                x.TerminRezervacije = m.dtmDate;
                x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                x.BrojTretmana = m.Kolicina;
                _dbContext.Add(x);
                _dbContext.SaveChanges();
            }
            else
            {
                var ima = _dbContext.RezervacijaMeniRestoran.Where(r => r.RezervacijaID == postoji.RezervacijaID && r.MeniRestoranID == m.ID).FirstOrDefault();

                if (ima == null)
                {
                    var rezervacija = _dbContext.Rezervacija.FirstOrDefault(a => a.KorisnikID == user.Id);
                    var x = new RezervacijaMeniRestoran();
                    x.RezervacijaID = rezervacija.RezervacijaID;
                    x.MeniRestoranID = m.ID;
                    x.CijenaTretmana = m.CijenaNarudzbe;
                    x.TerminRezervacije = m.dtmDate;
                    x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                    x.BrojTretmana = m.Kolicina;
                    _dbContext.Add(x);
                    _dbContext.SaveChanges();
                }
            }

            var meniRestoran = _dbContext.MeniRestoran.Find(m.ID);
            var meniRestoranTip = _dbContext.MeniRestoranTip.Find(meniRestoran.MeniRestoranTipID);
            var meniRestoranSlika = _dbContext.MeniRestoranSlike.Find(m.ID);

            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = meniRestoran.Restoran + meniRestoranTip.NazivTipaMenija,
                Tip = meniRestoranTip.NazivTipaMenija,
                Opis = meniRestoran.OpisMenija,
                PutanjaDoSlike = meniRestoranSlika.URL
            };
            m.stavke = new List<RezervacijaPrikazVM.Rows>();
            m.stavke.Add(stavke);

            await _emailService.SendEmailAsync(user.Email, "Rezervacija Menija",
                "<h2>Uspješno ste poslali Vašu rezervaciju!</h2>" +
                $"<p>Poštovani " + user.Ime + " " + user.Prezime + ", <br />" + "<br />" +
                $"Kako bismo dovršili Vašu rezervaciju, potvrdite svoju e-mail adresu na linku</p>" +
                "<button>Potvrdi prijavu</button>" + "<br />" +
                $"<p>Hvala Vam na Vašoj rezervaciji." + "<br />" +
                $"Srdačno Vas pozdravljamo!" + "<br />" + " <br />" +
                $"Vaš Resort tim</p>");

            return Redirect("PrikazMeniRestoran");
        }

    }

}
