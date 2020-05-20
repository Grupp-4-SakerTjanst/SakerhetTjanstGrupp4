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
        [Route("SkapaAnvandare")]
        [HttpPost]

        public object SkapaAnvandare (Anvandare NyAnvandare)
        {
            Anvandare SkapadAnvandare = new Anvandare();
            SkapadAnvandare.Email = NyAnvandare.Email;
            SkapadAnvandare.Losenord = NyAnvandare.Losenord;
            NyAnvandare.Behorighet = 1;

            db.Anvandares.Add(SkapadAnvandare);
            db.SaveChanges();

            //kalla på deras tjänst
            return (NyAnvandare.Id);     
           
        }
      
        [HttpPost]
        public object LoginValidation(Anvandare InLogg)
        {
            if (InLogg.Email == null || InLogg.Losenord == null)
            {
                ModelState.AddModelError("", "Du måste fylla i");
                return Redirect();
            }

            bool ValidUser = false;
            ValidUser = CheckUser(InLogg.Email, InLogg.Losenord) ;

            if (ValidUser == true)
            {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(InLogg.Email, false);
            }
            ModelState.AddModelError("", "Inloggning ej godkänd");
            return Redirect();

        }

        private bool CheckUser(string email, string losord)
        {
            var user = from rader in db.Anvandares
                       where rader.Email == email
                       && rader.Losenord == losord
                       select rader;
            if (user.Count() == 1)
            {
                
                return true;
            }
            else
            {             
                return false;               
            }
        }
        public Anvandare BehorighetMetod()
        {
            List<Anvandare> BehorigAnvandare = new List<Anvandare>();
            Anvandare TempDBAnvandare = new Anvandare();
            var behorig = from rader in db.Anvandares
                          where rader.Id == TempDBAnvandare.Id
                          select rader;
            foreach (var item in BehorigAnvandare)
            {
                item.Behorighet 
            }
            
            return

        }

        // GET: api/LoggaIn
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LoggaIn/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LoggaIn
        public void Post([FromBody]string value)
        {
        }
      
        // PUT: api/LoggaIn/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LoggaIn/5
        public void Delete(int id)
        {
        }
    }
}
