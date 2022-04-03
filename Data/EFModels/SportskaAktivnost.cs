using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class SportskaAktivnost
    {
        public int SportskaAktivnostID { get; set; }
        public int SportskeAktivnostiTipID { get; set; }
        public SportskaAktivnostTip SportskaAktivnostTip { get; set; }
        public string OpisPrograma { get; set; }
        public string NazivAktivnosti { get; set; }
        public int CijenaAktivnosti { get; set; }
    }
}
