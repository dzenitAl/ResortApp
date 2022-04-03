using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class BungalovPrikazVM
    {
        public class Row
        {
            public int BungalovId { get; set; }
            public string NazivBungalova { get; set; }
            public string BrojBungalova { get; set; }
            public int BungalovTipID { get; set; }
            public List<SelectListItem> BungalovTipovi { get; set; }
            public float Cijena { get; set; }
            public string OpisBungalova { get; set; }
            public List<IFormFile> BungalovSlike { get; set; }
            public List<string> PutanjaDoSlikaSobe { get; set; }
            public string PutanjaDoSlike { get; set; }
        }
        public int BungalovTipID { get; set; }
        public List<SelectListItem> BungalovTipovi { get; set; }
        public List<Row> bungalovi { get; set; }
        public int RolaID { get; set; }

    }
}
