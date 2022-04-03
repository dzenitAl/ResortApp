using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EFModels
{
    public class Korisnik : IdentityUser
    {
        public Admin Admin { get; set; }
        public Klijent Klijent { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Rola Rola { get; set; }
        public int RolaID { get; set; }
        public int brojNotifikacija { get; set; }

    }

}
