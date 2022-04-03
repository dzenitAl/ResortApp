using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class RezervacijaSala
    {
        public int SalaId { get; set; }
        public Sala Sala { get; set; }
        public int RezervacijaID { get; set; }
        public Rezervacija Rezervacija { get; set; }
        public DateTime TerminRezervacije { get; set; }
        public int BrojDana { get; set; }
        public float CijenaPoDanu { get; set; }
        public float UkupnaCijena { get; set; }
    }
}
