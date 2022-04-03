using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class SalaEvidentirajVM
    {
        public int SalaID { get; set; }
        public int KapacitetSale { get; set; }
        public string OpisSale { get; set; }
        public string NazivSale { get; set; }
        public float CijenaIznajmljivanjaSale { get; set; }
    
        public string PutanjaDoSlike { get; set; }
        public string KorisnikID { get; set; }
        public int SalaTipID { get; set; }
        public List<SelectListItem> SalaTipovi { get; set; }
        public List<IFormFile> SalaSlika { get; set; }
        public int RolaID { get; set; }
        public string SalaTip2 { get; set; }
    }
}
