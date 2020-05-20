using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SakerhetTjanstGrupp4;

namespace SakerhetTjanstGrupp4.Controllers
{
    public class LoginController : ApiController
    {
        private SakerhetDBModell db = new SakerhetDBModell();

        public IQueryable<Anvandare> GetAnvandares()
        {
            return db.Anvandares;
        }

        /*public void LoginValidation(string email, string losenord)
        {
            int anvandarRefId = 0;
            //TODO: jämför signatur med DB och hämta refId
            AuthenticationValidation(email, anvandarRefId);
        }
        public void AuthenticationValidation(string email, int anvandarRefId)
        {
            //TODO: hitta behorighet och skicka tillbaka till MVC
        }
        public void UppdaterasadAnvandare (string email, string losenord)
        {

        }
        public string UppdateraAnvandare (string email, string losenord)
        {
            
            return(email);
        }*/

       
        // POST: api/Anvandares
        [ResponseType(typeof(Anvandare))]
        public IHttpActionResult PostAnvandare(Anvandare anvandare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Anvandares.Add(anvandare);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = anvandare.Id }, anvandare);
        }



    }







}
