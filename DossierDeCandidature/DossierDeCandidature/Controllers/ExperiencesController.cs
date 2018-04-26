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
    public class ExperiencesController : Controller
    {
        private CandidatureContext db = new CandidatureContext();

        // GET: Experiences
        public async Task<ActionResult> Index()
        {
            return View(await db.Experiences.ToListAsync());
        }

        // GET: Experiences/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = await db.Experiences.FindAsync(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // GET: Experiences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Experiences/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Formation,Diplome,Ecole,Annee")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Experience = experience;
                Session["administratif"] = candidatures;
                return RedirectToAction("Create", "Langues");
            }

            return View(experience);
        }

        // GET: Experiences/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            RenseignementAdministratifsController rens = new RenseignementAdministratifsController();
            id = rens.Entier();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = await db.Experiences.FindAsync(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Formation,Diplome,Ecole,Annee")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Experience = experience;
                Session["administratif"] = candidatures;
                return RedirectToAction("Edit", "Langues");
            }
            return View(experience);
        }

        // GET: Experiences/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = await db.Experiences.FindAsync(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Experience experience = await db.Experiences.FindAsync(id);
            db.Experiences.Remove(experience);
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
