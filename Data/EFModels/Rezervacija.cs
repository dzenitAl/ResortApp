using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class Rezervacija
    {
        public int RezervacijaID { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public string KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }

    }
}
