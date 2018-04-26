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
    public class CompetencesController : Controller
    {
        private CandidatureContext db = new CandidatureContext();

        // GET: Competences
        public async Task<ActionResult> Index()
        {
            return View(await db.Competences.ToListAsync());
        }

        // GET: Competences/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competences competences = await db.Competences.FindAsync(id);
            if (competences == null)
            {
                return HttpNotFound();
            }
            return View(competences);
        }

        // GET: Competences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Competences/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Competence,NiveauCompetence")] ICollection<Competences> competences)
        {
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Competences = competences;
                Session["administratif"] = candidatures;
                return RedirectToAction("Create", "References");
            }

            return View(competences);
        }
        // GET: Competences/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            RenseignementAdministratifsController rens = new RenseignementAdministratifsController();
            id = rens.Entier();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competences competences = await db.Competences.FindAsync(id);
            if (competences == null)
            {
                return HttpNotFound();
            }
            return View(competences);
        }

        // POST: Competences/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Competence,NiveauCompetence")] ICollection<Competences> competences)
        {
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Competences = competences;
                Session["administratif"] = candidatures;
                return RedirectToAction("Edit", "References");
            }
            return View(competences);
        }

        // GET: Competences/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competences competences = await db.Competences.FindAsync(id);
            if (competences == null)
            {
                return HttpNotFound();
            }
            return View(competences);
        }

        // POST: Competences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Competences competences = await db.Competences.FindAsync(id);
            db.Competences.Remove(competences);
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
