using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class Bazen
    {
        public int BazenID { get; set; }
        public string? PutanjaDoSlikeSale { get; set; }
        public string NazivBazena { get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
    }
}
