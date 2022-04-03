using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class Bungalov
    {
        public int BungalovId { get; set; }
        public string NazivBungalova { get; set; }
        public string BrojBungalova { get; set; }
        public int BungalovTipID { get; set; }
        public BungalovTip BungalovTip { get; set; }
        public float Cijena { get; set; }
        public string OpisBungalova { get; set; }
    }
}
