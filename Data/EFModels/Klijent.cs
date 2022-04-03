using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.EFModels
{
    
    public class Klijent
    {
        [Key, ForeignKey("Korisnik")]
        public string ID { get; set; } //i PK i FK 
        public Rola Rola { get; set; }
        public int RolaID { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}
