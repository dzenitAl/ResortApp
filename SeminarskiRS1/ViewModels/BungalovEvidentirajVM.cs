using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class BungalovEvidentirajVM
    {
        public int BungalovId { get; set; }
        public string NazivBungalova { get; set; }
        public string BrojBungalova { get; set; }
        public int BungalovTipID { get; set; }
        public List<SelectListItem> BungalovTipovi { get; set; }
        public float Cijena { get; set; }
        public string OpisBungalova { get; set; }
        public int BungalovSlikeId { get; set; }
        public List<IFormFile> BungalovSlike { get; set; }
        public List<string> PutanjaDoSlikaBungalova { get; set; }
        public string PutanjaDoSlike { get; set; }
        public int RolaID { get; set; }
        public string BungalovTip2 { get; set; }
    }
}
