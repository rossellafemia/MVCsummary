using GamMostre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamMostre.Controllers
{
    public class MostreController : Controller
    {
        private MostreContext db = new MostreContext();
        // GET: Mostre
        public ActionResult Index()
        {
            //elenco delle prime 100 mostre
            var mostre = db.Mostre.Take(100).ToList();
            return View(mostre);
        }

        public ActionResult Mostra(int id)
        {
            var mostra = db.Mostre.Find(id);
            if (mostra == null)
                return HttpNotFound();
            return View(mostra);
        }
    }
}