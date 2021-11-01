using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using gesStockSdr.Models;
using Microsoft.AspNet.Identity;

namespace gesStockSdr.Controllers
{
    public class ProduitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produits
        public ActionResult Index()
        {
            var produits = db.Produits.Include(p => p.category).Include(p => p.fournisseur).Include(p => p.provenances);
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom");
            return View(produits.ToList());
        }
        public ActionResult searchParCateg(int ? categoryId)

        {
            if (categoryId == null)
            {
                var produits = db.Produits.Include(p => p.category).Include(p => p.fournisseur).Include(p => p.provenances);
                ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom");
                return View("Index", produits.ToList());
            }
            else
            {
 var produits = db.Produits.Where(a => a.categoryId== categoryId).Include(p => p.category).Include(p => p.fournisseur).Include(p => p.provenances);
                ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom");
                return View("Index", produits.ToList());
            }
           
            
        }

        // GET: Produits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Produits.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            return View(produits);
        }

        // GET: Produits/Create
        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom");
            ViewBag.fournisseurId = new SelectList(db.Fournisseurs, "Id", "nom");
            ViewBag.provenancesId = new SelectList(db.Provenances, "Id", "nom");
            return View();
        }

        // POST: Produits/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nom,num_article,quantite,date,categoryId,provenancesId,fournisseurId")] Produits produits)
        {
            if (ModelState.IsValid)
            {
                db.Produits.Add(produits);
              
                Historique histo=new Historique();
                histo.nom = produits.nom;
                histo.num_article = produits.num_article;
                histo.quantite = produits.quantite;
                histo.date = produits.date;
                histo.categoryId = produits.categoryId;
                histo.provenancesId = produits.provenancesId;
                histo.fournisseurId= produits.fournisseurId;
                histo.userId = User.Identity.GetUserId();
                db.Historiques.Add(histo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom", produits.categoryId);
            ViewBag.fournisseurId = new SelectList(db.Fournisseurs, "Id", "nom", produits.fournisseurId);
            ViewBag.provenancesId = new SelectList(db.Provenances, "Id", "nom", produits.provenancesId);
            return View(produits);
        }

        // GET: Produits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Produits.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom", produits.categoryId);
            ViewBag.fournisseurId = new SelectList(db.Fournisseurs, "Id", "nom", produits.fournisseurId);
            ViewBag.provenancesId = new SelectList(db.Provenances, "Id", "nom", produits.provenancesId);
            return View(produits);
        }

        // POST: Produits/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nom,num_article,quantite,date,categoryId,provenancesId,fournisseurId")] Produits produits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produits).State = EntityState.Modified;

                Historique histo = new Historique();
                histo.nom = produits.nom;
                histo.num_article = produits.num_article;
                histo.quantite = produits.quantite;
                histo.date = produits.date;
                histo.categoryId = produits.categoryId;
                histo.provenancesId = produits.provenancesId;
                histo.fournisseurId = produits.fournisseurId;
                histo.userId = User.Identity.GetUserId();
                db.Historiques.Add(histo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom", produits.categoryId);
            ViewBag.fournisseurId = new SelectList(db.Fournisseurs, "Id", "nom", produits.fournisseurId);
            ViewBag.provenancesId = new SelectList(db.Provenances, "Id", "nom", produits.provenancesId);
            return View(produits);
        }

        // GET: Produits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Produits.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            return View(produits);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produits produits = db.Produits.Find(id);
            db.Produits.Remove(produits);
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
