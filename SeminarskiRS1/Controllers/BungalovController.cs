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
using SeminarskiRS1.ViewModels;


namespace SeminarskiRS1.Controllers
{
    [Autorizacija(true, true)]

    public class BungalovController : Controller
    {
        private MojDbContext _dbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly ILogger<WellnesController> _logger;

        public BungalovController(MojDbContext x, IWebHostEnvironment webhostEnvironment, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ILogger<WellnesController> logger)
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

        [Autorizacija(true, true)]
        public async Task<IActionResult> PrikazBungalovaAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            List<SelectListItem> bungaloviTipovi = _dbContext.BungalovTip.Select(x => new SelectListItem
            {
                Value = x.BungalovTipID.ToString(),
                Text = x.NazivTipaBungalova
            }).ToList();

            List<BungalovPrikazVM.Row> bungalovi = _dbContext.Bungalov
                .Select(x => new BungalovPrikazVM.Row
                {
                    BungalovId = x.BungalovId,
                    BrojBungalova = x.BrojBungalova,
                    NazivBungalova = x.NazivBungalova,
                    BungalovTipID = x.BungalovTipID,
                    Cijena = x.Cijena,
                    OpisBungalova = x.OpisBungalova,
                    PutanjaDoSlike = _dbContext.BungalovSlike.Where(i => i.BungalovId == x.BungalovId).FirstOrDefault().URL
                }).ToList();

            BungalovPrikazVM x = new BungalovPrikazVM();

            x.bungalovi = bungalovi;
            x.BungalovTipovi = bungaloviTipovi;
            x.RolaID = user.RolaID;

            return View(x);
        }

        [Autorizacija(true, true)]
        public async Task<IActionResult> EvidentirajBungalovAsync(int BungalovId = 0)
        {
            var user = await _userManager.GetUserAsync(User);

            var bungalov = new BungalovEvidentirajVM();

            List<SelectListItem> bungalovTipovi = _dbContext.BungalovTip
                .Select(x => new SelectListItem
                {
                    Value = x.BungalovTipID.ToString(),
                    Text = x.NazivTipaBungalova
                })
                .ToList();

            if (BungalovId == 0)
            {
                bungalov = new BungalovEvidentirajVM();
            }
            else
            {
                bungalov = _dbContext.Bungalov
                    .Where(s => s.BungalovId == BungalovId)
                    .Select(x => new BungalovEvidentirajVM
                    {
                        BungalovId = x.BungalovId,
                        BrojBungalova = x.BrojBungalova,
                        NazivBungalova = x.NazivBungalova,
                        BungalovTipID = x.BungalovTipID,
                        BungalovTip2=x.BungalovTip.NazivTipaBungalova,
                        Cijena = x.Cijena,
                        OpisBungalova = x.OpisBungalova,
                    }).SingleOrDefault();

                bungalov.PutanjaDoSlikaBungalova = _dbContext.BungalovSlike.Where(s => s.BungalovId == BungalovId).Select(i => i.URL).ToList();
            }

            bungalov.BungalovId = BungalovId;
            bungalov.BungalovTipovi = bungalovTipovi;
            bungalov.RolaID = user.RolaID;
            return View(bungalov);
        }

        [Autorizacija(false, true)]
        public IActionResult Snimi(BungalovEvidentirajVM x)
        {
            Bungalov bungalov = new Bungalov();

            x.PutanjaDoSlikaBungalova = UploadFile(x);

            if (x.BungalovId == 0)
            {
                _dbContext.Add(bungalov);
            }
            else
            {
                bungalov = _dbContext.Bungalov.Find(x.BungalovId);
            }
            bungalov.BungalovId = x.BungalovId;
            bungalov.BrojBungalova = x.BrojBungalova;
            bungalov.NazivBungalova = x.NazivBungalova;
            bungalov.BungalovTipID = x.BungalovTipID;
            bungalov.Cijena = x.Cijena;
            bungalov.OpisBungalova = x.OpisBungalova;

            _dbContext.SaveChanges();

            if (x.PutanjaDoSlikaBungalova != null)
            {
                foreach (var i in x.PutanjaDoSlikaBungalova)
                {
                    var bungalovSlika = new BungalovSlike();

                    bungalovSlika.URL = i;
                    bungalovSlika.BungalovId = bungalov.BungalovId;

                    _dbContext.Add(bungalovSlika);
                    _dbContext.SaveChanges();
                }
            }
            return Redirect("PrikazBungalova");
        }

        [Autorizacija(false, true)]
        public IActionResult ObrisiBungalov(int BungalovId)
        {

            Bungalov pronadjena = _dbContext.Bungalov.Find(BungalovId);
            foreach (var x in _dbContext.RezervacijaBungalov.Where(x => x.BungalovId == BungalovId))
            {
                _dbContext.RezervacijaBungalov.Remove(x);
            }
            foreach (var x in _dbContext.BungalovSlike.Where(x => x.BungalovId == BungalovId))
            {
                _dbContext.BungalovSlike.Remove(x);
            }
            _dbContext.Remove(pronadjena);
            _dbContext.SaveChanges();

            return Redirect("PrikazBungalova");
        }

        [Autorizacija(false, true)]
        private List<string> UploadFile(BungalovEvidentirajVM x)
        {
            List<string> fileNameList = new List<string>();
            string fileName = null;
            if (x.BungalovSlike != null && x.BungalovSlike.Count > 0)
            {
                foreach (IFormFile slike in x.BungalovSlike)
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

                var x = new RezervacijaBungalov();
                x.RezervacijaID = rezervacija.RezervacijaID;
                x.BungalovId = m.ID;
                x.CijenaPoDanu = m.CijenaNarudzbe;
                x.TerminRezervacije = m.dtmDate;
                x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                x.BrojDana = m.Kolicina;
                _dbContext.Add(x);
                _dbContext.SaveChanges();
            }
            else
            {
                var ima = _dbContext.RezervacijaBungalov.Where(r => r.RezervacijaID == postoji.RezervacijaID && r.BungalovId == m.ID).FirstOrDefault();

                if (ima == null)
                {
                    var rezervacija = _dbContext.Rezervacija.FirstOrDefault(a => a.KorisnikID == user.Id);
                    var x = new RezervacijaBungalov();
                    x.RezervacijaID = rezervacija.RezervacijaID;
                    x.BungalovId = m.ID;
                    x.CijenaPoDanu = m.CijenaNarudzbe;
                    x.TerminRezervacije = m.dtmDate;
                    x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                    x.BrojDana = m.Kolicina;
                    _dbContext.Add(x);
                    _dbContext.SaveChanges();
                }
            }

            var bungalov = _dbContext.Bungalov.Find(m.ID);
            var bungalovTip = _dbContext.BungalovTip.Find(bungalov.BungalovTipID);
            var bungalovSlike = _dbContext.BungalovSlike.Find(m.ID);

            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = bungalov.NazivBungalova,
                Tip = bungalovTip.NazivTipaBungalova,
                Opis = bungalov.OpisBungalova,
                PutanjaDoSlike = bungalovSlike.URL
            };
            m.stavke = new List<RezervacijaPrikazVM.Rows>();
            m.stavke.Add(stavke);

            return Redirect("PrikazBungalova");
        }

        [Autorizacija(true, false)]
        public async Task<IActionResult> RezervacijaAsync(int BungalovId)
        {
            var user = await _userManager.GetUserAsync(User);

            var bungalov = _dbContext.Bungalov.Find(BungalovId);
            var bungalovTip = _dbContext.BungalovTip.Find(bungalov.BungalovTipID);
            var bungalovSlike = _dbContext.BungalovSlike.Find(BungalovId);

            var model = new RezervacijaPrikazVM()
            {
                ID = BungalovId,
                CijenaNarudzbe = bungalov.Cijena,

            };
            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = bungalov.NazivBungalova,
                Tip = bungalovTip.NazivTipaBungalova,
                Opis = bungalov.OpisBungalova,
                PutanjaDoSlike = bungalovSlike.URL
            };

            model.stavke = new List<RezervacijaPrikazVM.Rows>();
            model.stavke.Add(stavke);
            model.Controller = "Bungalov";

            return View("~/Views/Rezervacija/Prikaz.cshtml", model);
        }
    }
}
