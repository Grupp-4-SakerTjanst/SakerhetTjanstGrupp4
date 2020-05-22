using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SakerhetTjanstGrupp4.Controllers
{
    public class PersonalController : Controller
    {
        private PersonalModel db = new PersonalModel();
        // GET: Personal
        public ActionResult Index()
        {
            return View();
        }

        [Route("SkapaPersonal")]
        [HttpPost]
        public object SkapaPersonal(Personal NyPersonal)
        { //Måste kolla om vi ska använda IFS för att separera olika behorighetsnivar
            Personal SkapadAnvandare = new Personal();
            SkapadAnvandare.AnvandarNamn = NyPersonal.AnvandarNamn;
            SkapadAnvandare.Losenord = NyPersonal.Losenord;
            SkapadAnvandare.BehorighetsNiva = 2;

            db.Personal.Add(SkapadAnvandare);
            db.SaveChanges();

            //kalla på deras tjänst
            return (NyPersonal.Id);

        }
    }
}