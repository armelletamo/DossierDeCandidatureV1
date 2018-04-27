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
            ICollection<Competences> competenceNotNull = new List<Competences>();
            if (ModelState.IsValid)
            {
                foreach (var item in competences)
                {
                    if (item.Competence != null)
                        competenceNotNull.Add(item);
                }
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Competences = competenceNotNull;
                Session["administratif"] = candidatures;
                return RedirectToAction("Create", "References");
            }

            return View(competences);
        }
        // GET: Competences/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            List<Competences> comp = new List<Competences>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var renseignementAdministratif = await db.Competences
                    .Where(x => x.RenseignementAdministratif.Id == id)
                         .Include("RenseignementAdministratif")
                         .ToListAsync();
                foreach (var c in renseignementAdministratif)
                {
                    comp.Add(c);
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }

            return View(comp);
        }

        // POST: Competences/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Competence,NiveauCompetence")] ICollection<Competences> comp)
        {
            var idRenseignement = (int)Session["idRenseignement"];
            if (ModelState.IsValid)
            {
                foreach (var item in comp)
                {
                    if (item.Competence != null)
                    {
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Verification", "Enregistrement");
            }
            return View(comp);
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
            int? idRenseignement = (int)Session["idRenseignement"];
            Competences competences = await db.Competences.FindAsync(id);
            if(competences==null || idRenseignement==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Competences.Remove(competences);
            await db.SaveChangesAsync();
            return RedirectToAction("Verification", "Enregistrement");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: Competances/AjouterCompetences
        public ActionResult Ajouter(int? id)
        {
            Session["idRenseignement"] = id;
            return View("Ajouter");
        }

        // POST: Competances/Ajouter
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ajouter([Bind(Include = "Id,Competence,NiveauCompetence")] ICollection<Competences> comp)
        {
            var idRenseignement = (int)Session["idRenseignement"];
            if (ModelState.IsValid)
            {
                try
                {

                    var renseignementAdministratif = db.RenseignementsAdministratifs
                    .Where(x => x.Id == idRenseignement)
                      .Include(x => x.Competences)
                         .FirstOrDefault();
                    foreach (var item in comp)
                    {
                        if (item.Competence != null)
                        {
                            db.Competences.Add(item);
                            db.SaveChanges();
                            renseignementAdministratif.Competences.Add(item);
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Verification", "Enregistrement");
                }
                catch(Exception ex)
                {
                    return HttpNotFound();
                }

            }
            return View("Ajouter");
        }
    }
}
