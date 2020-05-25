using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SakerhetTjanstGrupp4.Controllers
{
    public class LoggaInController : ApiController
    {
        private SakerhetDBModell db = new SakerhetDBModell();
       
        [Route("LoginValidation")]
        [HttpPost]
        public object LoginValidation(Anvandare InLogg)
        {
            if (InLogg.Email == null || InLogg.Losenord == null)
            {
                ModelState.AddModelError("", "Du måste fylla i");

                return NotFound();

            }

            Anvandare AnvInfo = CheckUser(InLogg.Email, InLogg.Losenord);

            if (AnvInfo.Email == null)
            {
                
                ModelState.AddModelError("", "Inloggning ej godkänd");
                return NotFound();
            }
            else
            {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(InLogg.Email, false);
            }
            
            return NotFound();

        }

        private Anvandare CheckUser(string email, string losord)
        {
            var tempDB = db.Anvandares.ToList();
            foreach (var item in tempDB)
            {
                Anvandare TempDBAnvandare = new Anvandare();
                if (email == item.Email && losord == item.Losenord)
                {
                    TempDBAnvandare.Behorighetniva = item.Behorighetniva;
                    TempDBAnvandare.Id = item.Id;
                    return (TempDBAnvandare);
                    
                }
            }
            return null;
            /* var user = from rader in db.Anvandares
                        where rader.Email == email
                        && rader.Losenord == losord
                        select rader;

     */

        }
        /* public Anvandare BehorighetMetod(int id, int behorighet)
         {
             List<Anvandare> BehorigAnvandare = new List<Anvandare>();
             Anvandare TempDBAnvandare = new Anvandare();

             foreach (var item in BehorigAnvandare)
             {
                 var behorig = from rader in db.Anvandares
                               where rader.Id == TempDBAnvandare.Id
                               select rader.Behorighet;
             }

             return

         }*/

    

    }
}
