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
    public class CommandesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Commandes
        public ActionResult Index()
        {
            var commandes = db.Commandes.Include(c => c.service);
            return View(commandes.ToList());
        }
        public ActionResult searchByDate(DateTime ?date)
        {
            if (date==null)
            {
                var commandes = db.Commandes.Include(c => c.service);
                return View("Index",commandes.ToList());
              
            }
            else
            {
               
                var commandes = db.Commandes.Where(a => a.date==date).Include(c => c.service);
                return View("Index", commandes.ToList());
            }
           
        }

        // GET: Commandes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commandes.Find(id);
            ViewData["panier"]  =db.Paniers.Where(s => s.CommandeId == id).ToList();
            if (commande == null)
            {
                return HttpNotFound();
               
            }
            return View(commande);
        }

        // GET: Commandes/Create
        public ActionResult Create()
        {   
            ViewBag.serviceId = new SelectList(db.Services, "Id", "nom");
            var produits = db.Produits.Include(p => p.category).Include(p => p.fournisseur).Include(p => p.provenances);
            return View(produits.ToList());
        
        }

        // POST: Commandes/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int serviceId,DateTime date)
        {  
            Commande commande = new Commande();
            if (ModelState.IsValid)
            {
              

                commande.serviceId = serviceId;
                commande.date = date;

                commande.Set(ComdAjo);
                db.Commandes.Add(commande);
                Console.WriteLine("ddddddddddddddddd");
                db.SaveChanges();
                var produits = db.Produits.Include(p => p.category).Include(p => p.fournisseur).Include(p => p.provenances);
                ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom");
                return View("choisirPro",produits.ToList());
              
            }

            ViewBag.serviceId = new SelectList(db.Services, "Id", "nom", commande.serviceId);
            return View(commande);
        }

        // GET: Commandes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            ViewBag.serviceId = new SelectList(db.Services, "Id", "nom", commande.serviceId);
            return View(commande);
        }

        // POST: Commandes/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,status,serviceId,date")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commande).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.serviceId = new SelectList(db.Services, "Id", "nom", commande.serviceId);
            return View(commande);
        }

        // GET: Commandes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // POST: Commandes/Delete/5
        [HttpPost]
       
        public ActionResult DeleteConfirmed(int id)
        {
            Commande commande = db.Commandes.Find(id);
            db.Commandes.Remove(commande);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteLastCommande()
        {
            int id= db.Commandes.AsEnumerable().Last().Id;

            Commande commande = db.Commandes.Find(id);
            db.Commandes.Remove(commande);
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
        Dictionary<Produits, int> ComdAjo = new Dictionary<Produits, int>();
        [HttpPost]
    
        public void ajoCmd(int idPro,int qty)
        {
            Console.WriteLine(idPro);
            Produits produits = db.Produits.Find(idPro);
            ComdAjo.Add(produits, qty);
           


        }
        [HttpPost]
     
        public ActionResult valider(  int idCom)
        {
            var pro = db.Paniers.Where(s => s.CommandeId == idCom).ToList();
           foreach(var p  in pro)
            {
              
               int newQty = p.produits.quantite- p.quantite;
                Produits produits = db.Produits.Find(p.produits.Id);
                produits.quantite = newQty;
                 db.Entry(produits).State = EntityState.Modified;
                db.SaveChanges();
            }
            Commande cmd = db.Commandes.Find(idCom);
            cmd.status = true;
            db.Entry(cmd).State = EntityState.Modified;
            db.SaveChanges();
          
            return RedirectToAction("Details", new { id = idCom });

        }
        public ActionResult searchParCateg(int? categoryId)

        {
            if (categoryId == null)
            {
                var produits = db.Produits.Include(p => p.category).Include(p => p.fournisseur).Include(p => p.provenances);
                ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom");
                return View("choisirPro", produits.ToList());
            }
            else
            {
                var produits = db.Produits.Where(a => a.categoryId == categoryId).Include(p => p.category).Include(p => p.fournisseur).Include(p => p.provenances);
                ViewBag.categoryId = new SelectList(db.Categories, "Id", "nom");
                return View("choisirPro", produits.ToList());
            }


        }
    }

}
