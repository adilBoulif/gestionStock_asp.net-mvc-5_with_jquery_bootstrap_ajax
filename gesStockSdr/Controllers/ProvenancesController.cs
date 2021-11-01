using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using gesStockSdr.Models;

namespace gesStockSdr.Controllers
{
    public class ProvenancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Provenances
        public ActionResult Index()
        {
            return View(db.Provenances.ToList());
        }

        // GET: Provenances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provenances provenances = db.Provenances.Find(id);
            if (provenances == null)
            {
                return HttpNotFound();
            }
            return View(provenances);
        }

        // GET: Provenances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provenances/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nom")] Provenances provenances)
        {
            if (ModelState.IsValid)
            {
                db.Provenances.Add(provenances);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(provenances);
        }

        // GET: Provenances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provenances provenances = db.Provenances.Find(id);
            if (provenances == null)
            {
                return HttpNotFound();
            }
            return View(provenances);
        }

        // POST: Provenances/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nom")] Provenances provenances)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provenances).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provenances);
        }

        // GET: Provenances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provenances provenances = db.Provenances.Find(id);
            if (provenances == null)
            {
                return HttpNotFound();
            }
            return View(provenances);
        }

        // POST: Provenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provenances provenances = db.Provenances.Find(id);
            db.Provenances.Remove(provenances);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Search(string nom)
        {
            return View("index", db.Provenances.Where(a => a.nom.Equals(nom)).ToList());
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
