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
    public class HistoriquesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Historiques
        public ActionResult Index()
        {
            var historiques = db.Historiques.Include(h => h.category).Include(h => h.fournisseur).Include(h => h.provenances);
            return View(historiques.ToList());
        }

        // GET: Historiques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historique historique = db.Historiques.Find(id);
            if (historique == null)
            {
                return HttpNotFound();
            }
            return View(historique);
        }

        // GET: Historiques/Create
        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom");
            ViewBag.fournisseurId = new SelectList(db.Fournisseurs, "Id", "nom");
            ViewBag.provenancesId = new SelectList(db.Provenances, "Id", "nom");
            return View();
        }

        // POST: Historiques/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nom,num_article,quantite,date,categoryId,provenancesId,fournisseurId,userId")] Historique historique)
        {
            if (ModelState.IsValid)
            {
                db.Historiques.Add(historique);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom", historique.categoryId);
            ViewBag.fournisseurId = new SelectList(db.Fournisseurs, "Id", "nom", historique.fournisseurId);
            ViewBag.provenancesId = new SelectList(db.Provenances, "Id", "nom", historique.provenancesId);
            return View(historique);
        }

        // GET: Historiques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historique historique = db.Historiques.Find(id);
            if (historique == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom", historique.categoryId);
            ViewBag.fournisseurId = new SelectList(db.Fournisseurs, "Id", "nom", historique.fournisseurId);
            ViewBag.provenancesId = new SelectList(db.Provenances, "Id", "nom", historique.provenancesId);
            return View(historique);
        }

        // POST: Historiques/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nom,num_article,quantite,date,categoryId,provenancesId,fournisseurId,userId")] Historique historique)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historique).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom", historique.categoryId);
            ViewBag.fournisseurId = new SelectList(db.Fournisseurs, "Id", "nom", historique.fournisseurId);
            ViewBag.provenancesId = new SelectList(db.Provenances, "Id", "nom", historique.provenancesId);
            return View(historique);
        }

        // GET: Historiques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historique historique = db.Historiques.Find(id);
            if (historique == null)
            {
                return HttpNotFound();
            }
            return View(historique);
        }

        // POST: Historiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Historique historique = db.Historiques.Find(id);
            db.Historiques.Remove(historique);
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
