using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class BazenPrikazVM
    {
        public class Row
        {
            public int BazenId { get; set; }
            public string NazivBazena { get; set; }
            public float Cijena { get; set; }
            public string OpisBazena { get; set; }
            public List<IFormFile> BazenSlika { get; set; }
            public List<string> PutanjaDoSlikaBazena { get; set; }
            public string PutanjaDoSlike { get; set; }
        }
        public List<Row> bazeni { get; set; }
        public int RolaID { get; set; }

    }
}
