﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using SakerhetTjanstGrupp4.Models;
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
        private AnvandarModel db = new AnvandarModel();

        [HttpGet]
        public IHttpActionResult Home()
        {
            Personal SkapadAnvandare = new Personal();

            SkapadAnvandare.AnvandarNamn = "GanimBicoliTEST4";
            SkapadAnvandare.Losenord = "123";
            SkapadAnvandare.BehorighetsNiva = 2;

            Login("Ganim@hotmail.com", "123");
            

            return Ok();


        }
        // GET: api/Anvandares
        //public IQueryable<Anvandare> GetAnvandares()
        //{
        //    return db.Anvandares;
        //}

        // GET: api/Anvandares/5
        [ResponseType(typeof(Anvandare))]
        public IHttpActionResult GetAnvandare(int id)
        {
            Anvandare anvandare = db.Anvandare.Find(id);
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

        //// POST: api/Anvandares
        //[ResponseType(typeof(Anvandare))]
        //public IHttpActionResult PostAnvandare(Anvandare anvandare)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Anvandares.Add(anvandare);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = anvandare.Id }, anvandare);
        //}

        // DELETE: api/Anvandares/5
        [ResponseType(typeof(Anvandare))]
        public IHttpActionResult DeleteAnvandare(int id)
        {
            Anvandare anvandare = db.Anvandare.Find(id);
            if (anvandare == null)
            {
                return NotFound();
            }

            db.Anvandare.Remove(anvandare);
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
            return db.Anvandare.Count(e => e.Id == id) > 0;
        }




        //Vår Kod
        [Route("Login")]
        public IHttpActionResult Login(string email, string los)
        {
            //List<Anvandar> test = new List<Anvandar>();
            Anvandare test = new Anvandare();

            try
            {
                string emailCheck = email.ToString();
                string losenordCheck = los.ToString();
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
                test = db.Anvandare.Where(x => x.Email == email && x.Losenord == los).FirstOrDefault();
                                   //.OrderBy(x => x.Id)
                                   //.Select(x => new Anvandar   //Använder egen model.
                                   //{
                                   //    Behorighetniva = x.Behorighetniva,
                                   //    Id = x.Id
                                   //}).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(test);
        }





        [HttpPost]
        [Route("SkapaAnvandare")]
        public IHttpActionResult SkapaNyAnvändare(Anvandare NyAnvandare)
        {
            Anvandare SkapadAnvandare = new Anvandare();
            bool sammaKonto = false;
            foreach (var item in db.Anvandare.ToList())
            {
                if (item.Email == NyAnvandare.Email)
                {
                    sammaKonto = true;
                    break;
                }
            }

            try
            {
                SkapadAnvandare.Email = NyAnvandare.Email;
                SkapadAnvandare.Losenord = NyAnvandare.Losenord;
                SkapadAnvandare.Behorighetniva = 1;
            }
            catch (NullReferenceException)
            {
                //retunera att ett av värderna var null.
                throw;
            }
            if (sammaKonto == false)
            {
                db.Anvandare.Add(SkapadAnvandare);
                db.SaveChanges();
                return Ok(SkapadAnvandare.Id);

                //return Ok(db.Anvandare.Where(x => x.Email == NyAnvandare.Email
                //                 && x.Losenord == NyAnvandare.Losenord)
                //                 .Select(s => s.Id).FirstOrDefault());

            }
            //Returnera detta
            //kalla på deras tjänst och skicka ID. Ska vi inte bara retunera? blir detta kallat så skickar detta tillbaka. vi ska inte 
            //skicka detta till ett specifik tjänst, eller??


            return Ok();

        }
    }



}