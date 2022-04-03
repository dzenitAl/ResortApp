using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiRS1.ViewModels
{
    public class SpaCentarPrikazVM
    {
        public class Row
        {
            public int SpaCentarId { get; set; }
            public string OpisSpaCentra { get; set; }
            public string NazivSpaCentra { get; set; }
            public float CijenaZakupljivanjaSpaCentra { get; set; }
            public string PutanjaDoSlike { get; set; }
            public List<IFormFile> SpaCentarSlika { get; set; }
        }
        public List<Row> centri { get; set; }
        public int RolaID { get; set; }

    }
}
