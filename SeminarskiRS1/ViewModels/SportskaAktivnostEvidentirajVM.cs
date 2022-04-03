using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class SportskaAktivnostEvidentirajVM
    {
        public int SportskaAktivnostID { get; set; }
        public int SportskaAktivnostiTipID { get; set; }
        public List<SelectListItem> SportskaAktivnostTip { get; set; }
        public string SportskaAktivnostTip2 { get; set; }
        public string OpisPrograma { get; set; }
        public string NazivAktivnosti { get; set; }
        public int CijenaAktivnosti { get; set; }
        public int SportskaAktivnostSlikeId { get; set; }
        public List<IFormFile> SportskaAktivnostSlika { get; set; }
        public List<string> PutanjaDoSlikaSportskeAktivnosti { get; set; }
        public string PutanjaDoSlike { get; set; }
        public int RolaID { get; set; }
    }
}
