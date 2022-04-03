using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class Soba
    {
        public int SobaId { get; set; }
        public string NazivSobe { get; set; }
        public string BrojSobe { get; set; }
        public int SobaTipID { get; set; }
        public SobaTip SobaTip { get; set; }
        public float Cijena { get; set; }
        public string OpisSobe { get; set; }
    }
}
