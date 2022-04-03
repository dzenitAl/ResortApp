using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class SportskaAktivnostSlike
    {
        public int SportskaAktivnostSlikeId { get; set; }
        public int SportskaAktivnostID { get; set; }
        public SportskaAktivnost SportskaAktivnost { get; set; }
        public string Naziv { get; set; }
        public string URL { get; set; }
    }
}
