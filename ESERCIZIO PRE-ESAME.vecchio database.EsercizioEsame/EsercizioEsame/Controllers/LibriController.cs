using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EsercizioEsame.Models;

namespace EsercizioEsame.Controllers
{
    public class LibriController : Controller
    {
        private PrestitiBibliotecaEntities db = new PrestitiBibliotecaEntities();

        // GET: Libri
        public ActionResult Index(string SearchString, string DropSearch)
        {
            if(SearchString == null || SearchString ==  "")
            { return View(db.Libro.ToList()); }

            else
            {
                var Books = db.Libro.ToList();
                if(DropSearch == "AUTORE")
                {
                    Books = Books.Where(x => x.Autore == SearchString).ToList();
                }
                if (DropSearch == "TITOLO")
                {
                    Books = Books.Where(x => x.Titolo == SearchString).ToList();
                }
                if (DropSearch == "EDITORE")
                {
                    Books = Books.Where(x => x.Editore == SearchString).ToList();
                }

                return View(Books); 
            }
            
        }

        // GET: Libri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // GET: Libri/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Libri/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codice,Autore,Titolo,Editore,Anno,Luogo,Pagine,Classificazione,Collocazione,Copie")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Libro.Add(libro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(libro);
        }

        // GET: Libri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // POST: Libri/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codice,Autore,Titolo,Editore,Anno,Luogo,Pagine,Classificazione,Collocazione,Copie")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(libro);
        }

        // GET: Libri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // POST: Libri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Libro libro = db.Libro.Find(id);
            db.Libro.Remove(libro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
