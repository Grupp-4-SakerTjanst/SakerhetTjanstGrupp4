using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SakerhetTjanstGrupp4.Controllers
{
    public class LoginController : ApiController
    {
        public void LoginValidation(string email, string losenord)
        {
            int anvandarRefId = 0;
            //TODO: jämför signatur med DB och hämta refId
            AuthenticationValidation(email, anvandarRefId);
        }
        public void AuthenticationValidation(string email, int anvandarRefId)
        {
            //TODO: hitta behorighet och skicka tillbaka till MVC
        }
    }
   


}
