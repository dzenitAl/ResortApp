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
    public class SalaController : Controller
    {
        private MojDbContext _dbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly ILogger<WellnesController> _logger;
        public SalaController(MojDbContext x, IWebHostEnvironment webhostEnvironment, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ILogger<WellnesController> logger)
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
        public async Task<IActionResult> PrikazSalaAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            List<SelectListItem> salaTipovi = _dbContext.SalaTip.Select(x => new SelectListItem
            {
                Value = x.SalaTipID.ToString(),
                Text = x.NazivTipaSale
            }).ToList();

            List<SalaPrikazVM.Row> sale = _dbContext.Sala
                .Select(x => new SalaPrikazVM.Row
                {
                    SalaID = x.SalaID,
                    KapacitetSale = x.KapacitetSale,
                    OpisSale = x.OpisSale,
                    NazivSale = x.NazivSale,
                    SalaTipID = x.SalaTipID,
                    CijenaIznajmljivanjaSale = x.CijenaIznajmljivanjaSale,
                    PutanjaDoSlike=x.PutanjaDoSlikeSale
                }).ToList();
            
            SalaPrikazVM x = new SalaPrikazVM();
            x.sale = sale;
            x.SalaTipovi = salaTipovi;
            x.RolaID = user.RolaID;

            return View(x);
        }
        [Autorizacija(true, true)]
        public async Task<IActionResult> EvidentirajSaluAsync( int SalaID = 0)
        {
            var user = await _userManager.GetUserAsync(User);

            List<SelectListItem> salaTipovi = _dbContext.SalaTip.Select(x => new SelectListItem
            {
                Value = x.SalaTipID.ToString(),
                Text = x.NazivTipaSale
            }).ToList();

            SalaEvidentirajVM sala = new SalaEvidentirajVM();

            if (SalaID == 0)
            {
                sala = new SalaEvidentirajVM();
            }
            else
            {
                sala = _dbContext.Sala
                    .Where(s => s.SalaID == SalaID)
                    .Select(c => new SalaEvidentirajVM
                    {
                        SalaID = c.SalaID,
                        NazivSale = c.NazivSale,
                        KapacitetSale = c.KapacitetSale,
                        SalaTipID = c.SalaTipID,
                        SalaTip2=c.SalaTip.NazivTipaSale,
                        CijenaIznajmljivanjaSale = c.CijenaIznajmljivanjaSale,
                        OpisSale = c.OpisSale,
                        PutanjaDoSlike=c.PutanjaDoSlikeSale

                    }).SingleOrDefault();
            }

            sala.SalaID = SalaID;
            sala.SalaTipovi = salaTipovi;
            sala.RolaID = user.RolaID;


            return View(sala);
        }
        [Autorizacija(false, true)]
        public IActionResult Snimi(SalaEvidentirajVM x)
        {

            Sala sale = new Sala();
            x.PutanjaDoSlike = UploadFile(x);
            if (x.SalaID == 0)
            {
                _dbContext.Add(sale);
            }
            else
            {
                sale = _dbContext.Sala.Find(x.SalaID);
            }
            sale.SalaID = x.SalaID;
            sale.NazivSale = x.NazivSale;
            sale.OpisSale = x.OpisSale;
            sale.KapacitetSale = x.KapacitetSale;
            sale.CijenaIznajmljivanjaSale = x.CijenaIznajmljivanjaSale;
            sale.SalaTipID = x.SalaTipID;
            if (!string.IsNullOrEmpty(x.PutanjaDoSlike))
                sale.PutanjaDoSlikeSale = x.PutanjaDoSlike;
            _dbContext.SaveChanges();
            return Redirect("PrikazSala");
        }
        [Autorizacija(false, true)]
        public IActionResult ObrisiSalu( int SalaID)
        {

            Sala pronadjen = _dbContext.Sala.Find(SalaID);
            foreach (var x in _dbContext.RezervacijaSala.Where(x => x.SalaId == SalaID))
            {
                _dbContext.RezervacijaSala.Remove(x);
            }
            _dbContext.Remove(pronadjen);
            _dbContext.SaveChanges();

            return Redirect("PrikazSala");
        }
        [Autorizacija(false, true)]
        private string UploadFile(SalaEvidentirajVM x)
        {
            string fileName = null;
            if (x.SalaSlika != null)
            {
                foreach (IFormFile sala in x.SalaSlika)
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

                var x = new RezervacijaSala();
                x.RezervacijaID = rezervacija.RezervacijaID;
                x.SalaId = m.ID;
                x.CijenaPoDanu = m.CijenaNarudzbe;
                x.TerminRezervacije = m.dtmDate;
                x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                x.BrojDana = m.Kolicina;
                _dbContext.Add(x);
                _dbContext.SaveChanges();
            }
            else
            {
                var ima = _dbContext.RezervacijaSala.Where(r => r.RezervacijaID == postoji.RezervacijaID && r.SalaId == m.ID).FirstOrDefault();

                if (ima == null)
                {
                    var rezervacija = _dbContext.Rezervacija.FirstOrDefault(a => a.KorisnikID == user.Id);
                    var x = new RezervacijaSala();
                    x.RezervacijaID = rezervacija.RezervacijaID;
                    x.SalaId = m.ID;
                    x.CijenaPoDanu = m.CijenaNarudzbe;
                    x.TerminRezervacije = m.dtmDate;
                    x.UkupnaCijena = m.CijenaNarudzbe * m.Kolicina;
                    x.BrojDana = m.Kolicina;
                    _dbContext.Add(x);
                    _dbContext.SaveChanges();
                }
            }

            var sala = _dbContext.Sala.Find(m.ID);
            var salaTip = _dbContext.SalaTip.Find(sala.SalaTipID);

            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = sala.NazivSale,
                Tip = salaTip.NazivTipaSale,
                Opis = sala.OpisSale,
                PutanjaDoSlike = sala.PutanjaDoSlikeSale
            };
            m.stavke = new List<RezervacijaPrikazVM.Rows>();
            m.stavke.Add(stavke);

            return Redirect("PrikazSala");
        }

        [Autorizacija(true, false)]
        public async Task<IActionResult> RezervacijaAsync(int SalaId)
        {
            var user = await _userManager.GetUserAsync(User);

            var sala = _dbContext.Sala.Find(SalaId);
            var salaTip = _dbContext.SalaTip.Find(sala.SalaTipID);

            var model = new RezervacijaPrikazVM()
            {
                ID = SalaId,
                CijenaNarudzbe = sala.CijenaIznajmljivanjaSale,

            };
            var stavke = new RezervacijaPrikazVM.Rows()
            {
                Naziv = sala.NazivSale,
                Tip = salaTip.NazivTipaSale,
                Opis = sala.OpisSale,
                PutanjaDoSlike = sala.PutanjaDoSlikeSale
            };

            model.stavke = new List<RezervacijaPrikazVM.Rows>();
            model.stavke.Add(stavke);
            model.Controller = "Sala";

            return View("~/Views/Rezervacija/Prikaz.cshtml", model);
        }
    }
}
