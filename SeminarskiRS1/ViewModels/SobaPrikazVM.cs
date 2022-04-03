using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class SobaPrikazVM
    {
        public class Row
        {
            public int SobaId { get; set; }
            public string NazivSobe { get; set; }
            public string BrojSobe { get; set; }
            public int SobaTipID { get; set; }
            public List<SelectListItem> SobaTipovi { get; set; }
            public float Cijena { get; set; }
            public string OpisSobe { get; set; }
            public List<IFormFile> SobaSlike { get; set; }
            public List<string> PutanjaDoSlikaSobe { get; set; }
            public string PutanjaDoSlike { get; set; }
        }
        public string pretraga { get; set; }
        public int SobaTipID { get; set; }
        public List<SelectListItem> SobaTipovi { get; set; }
        public List<Row> sobe { get; set; }
        public int RolaID { get; set; }

    }
}
