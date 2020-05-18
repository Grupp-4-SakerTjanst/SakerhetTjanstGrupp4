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
    public class AnvandaresController : ApiController
    {
        private SakerhetDBModell db = new SakerhetDBModell();

        // GET: api/Anvandares
        public IQueryable<Anvandare> GetAnvandares()
        {
            return db.Anvandares;
        }

        // GET: api/Anvandares/5
        [ResponseType(typeof(Anvandare))]
        public IHttpActionResult GetAnvandare(int id)
        {
            Anvandare anvandare = db.Anvandares.Find(id);
            if (anvandare == null)
            {
                return NotFound();
            }

            return Ok(anvandare);
        }

        // PUT: api/Anvandares/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnvandare(int id, Anvandare anvandare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != anvandare.Id)
            {
                return BadRequest();
            }

            db.Entry(anvandare).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnvandareExists(id))
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

        // DELETE: api/Anvandares/5
        [ResponseType(typeof(Anvandare))]
        public IHttpActionResult DeleteAnvandare(int id)
        {
            Anvandare anvandare = db.Anvandares.Find(id);
            if (anvandare == null)
            {
                return NotFound();
            }

            db.Anvandares.Remove(anvandare);
            db.SaveChanges();

            return Ok(anvandare);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnvandareExists(int id)
        {
            return db.Anvandares.Count(e => e.Id == id) > 0;
        }
    }
}