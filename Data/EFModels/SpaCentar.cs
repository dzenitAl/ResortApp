using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class SpaCentar
    {
        public int SpaCentarID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int CijenaZakupa { get; set; }
        public string? PutanjaDoSlike { get; set; }
    }
}
