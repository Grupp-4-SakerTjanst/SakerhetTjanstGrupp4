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
        public IHttpActionResult Login(PersonalAnv persAnv)
        {
            PersonalAnv PersInf = new PersonalAnv();

            try
            {
                string emailCheck = persAnv.Anvandarnamn.ToString();
                string losenordCheck = persAnv.Losenord.ToString();
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
                PersInf = db.PersonalAnvs.Where(x => x.Anvandarnamn == persAnv.Anvandarnamn && x.Losenord == persAnv.Losenord).FirstOrDefault();
                //.OrderBy(x => x.Id)
                //.Select(x => new Person   //Använder egen model.
                //{
                //    BehorighetsNiva = x.Behorighetsniva,
                //    Id = x.Id
                //}).ToList();
            }
            catch (Exception)
            {

                throw;
            }


            return Ok(PersInf);
        }


        [Route("SkapaPersonal")]
        [HttpPost]
        public object SkapaPersonal(PersonalAnv NyPersonal)
        {
            PersonalAnv SkapadAnvandare = new PersonalAnv();
            try
            {
                SkapadAnvandare.Anvandarnamn = NyPersonal.Anvandarnamn;
                SkapadAnvandare.Losenord = NyPersonal.Losenord;
            }
            catch (NullReferenceException)
            {
                //retunera att ett av värderna var null.
                throw;
            }

            //Göra en lätt check mot XSS / SQL-injecktion.
            //Göra en gemensam metod mot detta för AnvandaresControll och denna controllern?

            var h = db.PersonalAnvs.ToList();
            bool sammaKonto = false;
            foreach (var item in db.PersonalAnvs.ToList())
            {
                if (item.Anvandarnamn == NyPersonal.Anvandarnamn)
                {
                    sammaKonto = true;
                    break;
                }
            }
            
            if (sammaKonto == false)
            {
                db.PersonalAnvs.Add(SkapadAnvandare);
                db.SaveChanges();

                NyPersonal.Id = SkapadAnvandare.Id;

                using (var client = new HttpClient()) //TODO skicka hela objektet till Personal och spara informationen i listan.
                {
                    client.BaseAddress = new Uri("http://localhost:56539/");
                    var response = client.PostAsJsonAsync("SkapaPersonal", NyPersonal).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var AnvSvar = response.Content.ReadAsStringAsync().Result;

                       Console.Write("Success");

                    }
                    else
                        Console.Write("Error");
                }

                return (db.PersonalAnvs.Where(x => x.Anvandarnamn == NyPersonal.Anvandarnamn
                                  && x.Losenord == NyPersonal.Losenord)
                                  .Select(s => s.Id).FirstOrDefault());
               
            }
            //Returnera detta
            //kalla på deras tjänst och skicka ID. Ska vi inte bara retunera? blir detta kallat så skickar detta tillbaka. vi ska inte 
            //skicka detta till ett specifik tjänst, eller??

            return Ok();

        }
    }
}