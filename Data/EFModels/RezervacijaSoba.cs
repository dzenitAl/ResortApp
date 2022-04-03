using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class RezervacijaSoba
    {
        public int SobaID { get; set; }
        public Soba Soba { get; set; }
        public int RezervacijaID { get; set; }
        public Rezervacija Rezervacija { get; set; }

        public DateTime DatumRezervacije { get; set; }
        public int BrojNoci { get; set; }
        public float Cijena { get; set; }
        public float UkupnaCijena { get; set; }
    }
}
