using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class SobaSlike
    {
        public int SobaSlikeId { get; set; }
        public int SobaId { get; set; }
        public Soba Soba { get; set; }
        public string Naziv { get; set; }
        public string URL { get; set; }
    }
}
