using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
   public  class Sala
    {
        public int SalaID { get; set; }
        public int KapacitetSale { get; set; }
        public string OpisSale { get; set; }
        public string NazivSale { get; set; }
        public float CijenaIznajmljivanjaSale { get; set; }
        public string? PutanjaDoSlikeSale { get; set; }
        public int SalaTipID { get; set; }
        public SalaTip SalaTip { get; set; }
    }
}
