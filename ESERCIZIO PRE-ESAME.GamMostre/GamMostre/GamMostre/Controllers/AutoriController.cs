using GamMostre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamMostre.Controllers
{
    public class AutoriController : Controller
    {
        private MostreContext db = new MostreContext();
        // GET: Autori
        public ActionResult Index()
        {
            return View(db.Autori.OrderBy(x => x.Nominativo).ToList());
        }

        public ActionResult ElencoMostre(int id)
        {
            ViewBag.Autore = db.Autori.Find(id).Nominativo;
            var mostre = db.Mostre.Where(x => x.Autore.Id==id).ToList();
            return View(mostre);
        }
    }
}