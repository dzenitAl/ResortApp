using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class RezervacijaSportskaAktivnost
    {
        public int SportskaAktivnostID { get; set; }
        public SportskaAktivnost SportskaAktivnost { get; set; }
        public int RezervacijaID { get; set; }
        public Rezervacija Rezervacija { get; set; }

        public DateTime TerminRezervacije { get; set; }
        public int BrojTermina { get; set; }
        public float CijenaTermina { get; set; }
        public float UkupnaCijena { get; set; }
    }
}
