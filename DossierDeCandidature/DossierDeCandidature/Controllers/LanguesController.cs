using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DossierDeCandidature.Models;

namespace DossierDeCandidature.Controllers
{
    public class LanguesController : Controller
    {
        private CandidatureContext db = new CandidatureContext();

        // GET: Langues
        public async Task<ActionResult> Index()
        {
            return View(await db.Langues.ToListAsync());
        }

        // GET: Langues/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Langues langues = await db.Langues.FindAsync(id);
            if (langues == null)
            {
                return HttpNotFound();
            }
            return View(langues);
        }

        // GET: Langues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Langues/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Langue,NiveauLangue,Autre,Niveau")] ICollection<Langues> langues)
        {
            if (ModelState.IsValid)
            {

                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Langues = langues;
                Session["administratif"] = candidatures;
                return RedirectToAction("Create", "Competences");
            }

            return View("Create");
        }

        // GET: Langues/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            RenseignementAdministratifsController rens = new RenseignementAdministratifsController();
            id = rens.Entier();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Langues langues = await db.Langues.FindAsync(id);
            if (langues == null)
            {
                return HttpNotFound();
            }
            return View(langues);
        }

        // POST: Langues/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Langue,NiveauLangue,Autre,Niveau")] ICollection<Langues> langues)
        {
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Langues = langues;
                Session["administratif"] = candidatures;
                return RedirectToAction("Edit", "Competences");
            }
            return View(langues);
        }

        // GET: Langues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Langues langues = await db.Langues.FindAsync(id);
            if (langues == null)
            {
                return HttpNotFound();
            }
            return View(langues);
        }

        // POST: Langues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Langues langues = await db.Langues.FindAsync(id);
            db.Langues.Remove(langues);
            await db.SaveChangesAsync();
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
