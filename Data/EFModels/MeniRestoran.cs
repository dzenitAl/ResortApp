using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class MeniRestoran
    {
        public int MeniRestoranID { get; set; }
        public string Restoran { get; set; }
        public int MeniRestoranTipID { get; set; }
        public MeniRestoranTip MeniRestoranTip { get; set; }
        public string OpisMenija { get; set; }
        public float CijenaMenija { get; set; }
    }

}
