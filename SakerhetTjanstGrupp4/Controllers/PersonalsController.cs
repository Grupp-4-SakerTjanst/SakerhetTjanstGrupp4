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
    public class PersonalsController : ApiController
    {
        private PersonalModel db = new PersonalModel();

        [HttpGet]
        public IHttpActionResult Home()
        {
            Personal SkapadAnvandare = new Personal();

            SkapadAnvandare.AnvandarNamn = "GanimBicoli";
            SkapadAnvandare.Losenord = "123";
            SkapadAnvandare.BehorighetsNiva = 2;

            SkapaPersonal(SkapadAnvandare);

            return Ok();


        }

        // GET: api/Personals
        //public IQueryable<Personal> GetPersonal()
        //{
        //    return db.Personal;
        //}

        // GET: api/Personals/5
        [ResponseType(typeof(Personal))]
        public IHttpActionResult GetPersonal(int id)
        {
            Personal personal = db.Personal.Find(id);
            if (personal == null)
            {
                return NotFound();
            }

            return Ok(personal);
        }

        // PUT: api/Personals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonal(int id, Personal personal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personal.Id)
            {
                return BadRequest();
            }

            db.Entry(personal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Personals
        [ResponseType(typeof(Personal))]
        public IHttpActionResult PostPersonal(Personal personal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Personal.Add(personal);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonalExists(personal.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = personal.Id }, personal);
        }

        // DELETE: api/Personals/5
        [ResponseType(typeof(Personal))]
        public IHttpActionResult DeletePersonal(int id)
        {
            Personal personal = db.Personal.Find(id);
            if (personal == null)
            {
                return NotFound();
            }

            db.Personal.Remove(personal);
            db.SaveChanges();

            return Ok(personal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonalExists(int id)
        {
            return db.Personal.Count(e => e.Id == id) > 0;
        }


        //Våra egna metoder
        //Våra egna metoder


        [Route("SkapaPersonal")]
        [HttpPost]
        public object SkapaPersonal(Personal NyPersonal)
        {
            Personal SkapadAnvandare = new Personal();
            try
            {
                SkapadAnvandare.AnvandarNamn = NyPersonal.AnvandarNamn;
                SkapadAnvandare.Losenord = NyPersonal.Losenord;
                SkapadAnvandare.BehorighetsNiva = 2;
            }
            catch (NullReferenceException)
            {
                //retunera att ett av värderna var null.
                throw;
            }

           
            bool sammaKonto = false;
            foreach (var item in db.Personal.ToList())
            {
                if (item.AnvandarNamn == NyPersonal.AnvandarNamn)
                {
                    sammaKonto = true;
                    break;
                }
            }
            
            if (sammaKonto == false)
            {
                db.Personal.Add(SkapadAnvandare);
                db.SaveChanges();
            }
            //Returnera detta
            //kalla på deras tjänst och skicka ID. Ska vi inte bara retunera? blir detta kallat så skickar detta tillbaka. vi ska inte 
            //skicka detta till ett specifik tjänst, eller??
            var h = db.Personal.Where(x => x.AnvandarNamn == NyPersonal.AnvandarNamn
                                  && x.Losenord == NyPersonal.Losenord)
                                  .Select(s => s.Id).ToList();
            var gg = 0;
            return Ok();

        }
    }
}