using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class MeniRestoranSlike
    {
        public int MeniRestoranSlikeID { get; set; }
        public int MeniRestoranID { get; set; }
        public MeniRestoran MeniRestoran { get; set; }
        public string Naziv { get; set; }
        public string URL { get; set; }
    }
}
