using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakerhetTjanstGrupp4.Models
{
    public class Anvandar
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Losenord { get; set; }

        public int Behorighetniva { get; set; }
    }
}