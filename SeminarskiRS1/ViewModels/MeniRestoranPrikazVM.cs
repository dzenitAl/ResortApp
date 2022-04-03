using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class MeniRestoranPrikazVM
    {
        public class Row
        {
            public int MeniRestoranID { get; set; }
            public string Restoran { get; set; }
            public int MeniRestoranTipID { get; set; }
            public List<SelectListItem> MeniRestoranTip { get; set; }
            public string OpisMenija { get; set; }
            public float CijenaMenija { get; set; }
            public List<IFormFile> MeniRestoranSlike { get; set; }
            public List<string> PutanjaDoSlikaMeniRestorana { get; set; }
            public string PutanjaDoSlike { get; set; }

        }
        public string pretraga { get; set; }
        public int MeniRestoranTipID { get; set; }
        public List<SelectListItem> MeniRestoranTip { get; set; }
        public List<Row> meniRestoran { get; set; }
        public int RolaID { get; set; }

    }
}
