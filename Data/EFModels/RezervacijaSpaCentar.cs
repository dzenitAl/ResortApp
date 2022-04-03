using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class RezervacijaSpaCentar
    {
        public int SpaCentarId { get; set; }
        public SpaCentar SpaCentar { get; set; }
        public int RezervacijaID { get; set; }
        public Rezervacija Rezervacija { get; set; }
        public DateTime TerminRezervacije { get; set; }
        public int BrojTretmana { get; set; }
        public float CijenaTretmana { get; set; }
        public float UkupnaCijena { get; set; }
    }
}
