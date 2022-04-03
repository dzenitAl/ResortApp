using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class BungalovSlike
    {
        public int BungalovSlikeId { get; set; }
        public int BungalovId { get; set; }
        public Bungalov Bungalov { get; set; }
        public string Naziv { get; set; }
        public string URL { get; set; }
    }
}
