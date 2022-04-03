using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class RezervacijaMeniRestoran
    {
        public int MeniRestoranID { get; set; }
        public MeniRestoran MeniRestoran { get; set; }
        public int RezervacijaID { get; set; }
        public Rezervacija Rezervacija { get; set; }

        public DateTime TerminRezervacije { get; set; }
        public int BrojTretmana { get; set; }
        public float CijenaTretmana { get; set; }
        public float UkupnaCijena { get; set; }
    }
}
