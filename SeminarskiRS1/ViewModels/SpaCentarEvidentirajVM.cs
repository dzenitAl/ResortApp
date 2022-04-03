using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class SpaCentarEvidentirajVM
    {
        public int SpaCentarId { get; set; }
        public string OpisCentra { get; set; }
        public string NazivCentra { get; set; }
        public int CijenaZakupljivanjaCentra { get; set; }
        public string PutanjaDoSlike { get; set; }
        public string KorisnikID { get; set; }
        public List<IFormFile> CentarSlika { get; set; }
        public int RolaID { get; set; }

    }
}
