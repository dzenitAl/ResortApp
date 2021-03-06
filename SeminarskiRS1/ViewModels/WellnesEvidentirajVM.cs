using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class WellnesEvidentirajVM
    {
        public int WellnesId { get; set; }
        public string NazivWellnes { get; set; }
        public string TipWellnes { get; set; }
        public string OpisPrograma { get; set; }
        public string VremenskoTrajanjeTermina { get; set; }
        public float CijenaTretmana { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public List<IFormFile> SlikaWellnes { get; set; }
        public string PutanjaDoSlikeWellnes { get; set; }
        public int RolaID { get; set; }

    }
}
