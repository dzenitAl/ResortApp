using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class BazenEvidentirajVM
    {
        public int BazenId { get; set; }
        public string OpisBazena { get; set; }
        public string NazivBazena { get; set; }
        public float CijenaIznajmljivanjaBazena { get; set; }
        public string PutanjaDoSlike { get; set; }
        public string KorisnikID { get; set; }
        public List<IFormFile> BazenSlika { get; set; }
        public int RolaID { get; set; }

    }
}
