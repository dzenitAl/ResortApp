using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class RezervacijaBazen
    {
        public int BazenId { get; set; }
        public Bazen Bazen { get; set; }
        public int RezervacijaID { get; set; }
        public Rezervacija Rezervacija { get; set; }
        public DateTime TerminRezervacije { get; set; }
        public int BrojLjudi { get; set; }
        public float Cijena { get; set; }
        public float UkupnaCijena { get; set; }
    }
}
