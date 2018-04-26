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
    public class MotivationsController : Controller
    {
        private CandidatureContext db = new CandidatureContext();

        // GET: Motivations
        public async Task<ActionResult> Index()
        {
            return View(await db.Motivations.ToListAsync());
        }

        // GET: Motivations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motivation motivation = await db.Motivations.FindAsync(id);
            if (motivation == null)
            {
                return HttpNotFound();
            }
            return View(motivation);
        }

        // GET: Motivations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Motivations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExperisIt,MotifRecherche,Objectif")] Motivation motivation)
        {
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Motivation = motivation;
                Session["administratif"] = candidatures;
                return RedirectToAction("Enregistrement", "Enregistrement");
                
            }

            return View(motivation);
        }

        // GET: Motivations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            RenseignementAdministratifsController rens = new RenseignementAdministratifsController();
            id = rens.Entier();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motivation motivation = await db.Motivations.FindAsync(id);
            if (motivation == null)
            {
                return HttpNotFound();
            }
            return View(motivation);
        }

        // POST: Motivations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExperisIt,MotifRecherche,Objectif")] Motivation motivation)
        {
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Motivation = motivation;
                Session["administratif"] = candidatures;
                return RedirectToAction("ModifierEnregistrement", "Enregistrement");
            }
            return View(motivation);
        }

        // GET: Motivations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motivation motivation = await db.Motivations.FindAsync(id);
            if (motivation == null)
            {
                return HttpNotFound();
            }
            return View(motivation);
        }

        // POST: Motivations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Motivation motivation = await db.Motivations.FindAsync(id);
            db.Motivations.Remove(motivation);
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
