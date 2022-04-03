using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class Wellnes
    {
        public int WellnesId { get; set; }
        public string NazivWellnes { get; set; }
        public string TipWellnes { get; set; }
        public string OpisPrograma { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public string VremenskoTrajanjeTermina { get; set; }
        public float CijenaTretmana { get; set; }
        public string? PutanjaDoSlikeWellnes { get; set; }
    }
}
