using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using SakerhetTjanstGrupp4.Models;
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
            PersonalAnv SkapadAnvandare = new PersonalAnv();

            SkapadAnvandare.AnvandarNamn = "GanimBicoliTEST4";
            SkapadAnvandare.Losenord = "123";
            SkapadAnvandare.BehorighetsNiva = 2;

            return Ok();


        }

        // GET: api/Personals
        //public IQueryable<Personal> GetPersonal()
        //{
        //    return db.Personal;
        //}

        // GET: api/Personals/5
        [ResponseType(typeof(PersonalAnv))]
        public IHttpActionResult GetPersonal(int id)
        {
            PersonalAnv personal = db.PersonalAnvs.Find(id);
            if (personal == null)
            {
                return NotFound();
            }

            return Ok(personal);
        }

        // PUT: api/Personals/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutPersonal(int id, Personal personal)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != personal.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(personal).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PersonalExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //POST: api/Personals
        [Route("CreatePersonal")]
        [HttpPost]
        //[ResponseType(typeof(Personal))]
        public IHttpActionResult PostPersonal(PersonalAnv personal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonalAnvs.Add(personal);

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
        [ResponseType(typeof(PersonalAnv))]
        public IHttpActionResult DeletePersonal(int id)
        {
            PersonalAnv personal = db.PersonalAnvs.Find(id);
            if (personal == null)
            {
                return NotFound();
            }

            db.PersonalAnvs.Remove(personal);
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
            return db.PersonalAnvs.Count(e => e.Id == id) > 0;
        }


        //Våra egna metoder
        //Våra egna metoder


        [Route("Login")]
        [HttpPost]
        public IHttpActionResult Login(PersonalAnv anvadar)
        {
            List<Person> test = new List<Person>();

            try
            {
                string emailCheck = anvadar.AnvandarNamn.ToString();
                string losenordCheck = anvadar.Losenord.ToString();
            }
            catch (ArgumentNullException e)
            {

                throw;
            }
            catch (FormatException e)
            {

                throw;
            }
            catch (Exception e)
            {

                throw;
            }


            try
            {
                test = db.PersonalAnvs.Where(x => x.AnvandarNamn == anvadar.AnvandarNamn)
                                   .OrderBy(x => x.Id)
                                   .Select(x => new Person   //Använder egen model.
                                   {
                                       BehorighetsNiva = x.BehorighetsNiva,
                                       Id = x.Id
                                   }).ToList();
            }
            catch (Exception)
            {

                throw;
            }


            return Ok(db.PersonalAnvs.Where(x => x.AnvandarNamn == anvadar.AnvandarNamn)
                                   .OrderBy(x => x.Id)
                                   .Select(x => new Person   //Använder egen model.
                                   {
                                       BehorighetsNiva = x.BehorighetsNiva,
                                       Id = x.Id
                                   }).ToList());
        }


        [Route("SkapaPersonal")]
        [HttpPost]
        public object SkapaPersonal(PersonalAnv NyPersonal)
        {
            PersonalAnv SkapadAnvandare = new PersonalAnv();
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

            //Göra en lätt check mot XSS / SQL-injecktion.
            //Göra en gemensam metod mot detta för AnvandaresControll och denna controllern?
           
            bool sammaKonto = false;
            foreach (var item in db.PersonalAnvs.ToList())
            {
                if (item.AnvandarNamn == NyPersonal.AnvandarNamn)
                {
                    sammaKonto = true;
                    break;
                }
            }
            
            if (sammaKonto == false)
            {
                db.PersonalAnvs.Add(SkapadAnvandare);
                db.SaveChanges();
                return (db.PersonalAnvs.Where(x => x.AnvandarNamn == NyPersonal.AnvandarNamn
                                  && x.Losenord == NyPersonal.Losenord)
                                  .Select(s => s.Id).ToList());
               
            }
            //Returnera detta
            //kalla på deras tjänst och skicka ID. Ska vi inte bara retunera? blir detta kallat så skickar detta tillbaka. vi ska inte 
            //skicka detta till ett specifik tjänst, eller??

            return Ok();

        }
    }
}