using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class RezervacijaPrikazVM
    {
        public class Rows
        {
            public int StavkaID { get; set; }
            public string Naziv { get; set; }
            public string Opis { get; set; }
            public string Tip { get; set; }
            public int Kolicina { get; set; }
            public float Cijena { get; set; }
            public float UkupnaCijena { get; set; }
            public string VremenskoTrajanjeTermina { get; set; }
            public DateTime TerminRezervacije { get; set; }
            public IFormFile Slika { get; set; }
            public string PutanjaDoSlike { get; set; }
        }
        public List<Rows> stavke { get; set; }
        public string KorisnikID { get; set; }
        public int RezervacijaID { get; set; }
        public float CijenaNarudzbe { get; set; }
        public float UkupnaCijena { get; set; }
        public int ID { get; set; }
        public int Kolicina { get; set; }
        public DateTime dtmDate { get; set; }
        public string Controller { get; set; }
    }
}

