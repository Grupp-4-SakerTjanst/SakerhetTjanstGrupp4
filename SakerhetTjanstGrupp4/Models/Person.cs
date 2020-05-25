using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakerhetTjanstGrupp4.Models
{
    public class Person
    {

        public int Id { get; set; }
        public string AnvandarNamn { get; set; }
        public string Losenord { get; set; }

        public int BehorighetsNiva { get; set; }
    }
}