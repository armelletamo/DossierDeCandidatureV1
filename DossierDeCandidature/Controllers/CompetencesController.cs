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
                try
                {
                    RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                    if (candidatures == null)
                        return HttpNotFound();
                    candidatures.Competences = competenceNotNull;
                    Session["administratif"] = candidatures;
                    return RedirectToAction("Create", "References");
                }
                catch
                {
                    if (Session["administratif"] == null)
                    {
                        ViewBag.messageExpirationSession = "La session a expiré veuillez resaisir vos informations";
                        return View("~/Views/Home/Index.cshtml");
                    }
                    return HttpNotFound();
                }
            }

            return View(competences);
        }
        // GET: Competences/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            List<Competences> comp = new List<Competences>();

            int ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);
            int Id = (int)Session["idRenseignement"];
            if (ID != Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var renseignementAdministratif = await db.Competences
                    .Where(x => x.RenseignementAdministratif.Id == ID)
                         .Include("RenseignementAdministratif")
                         .ToListAsync();
                foreach (var c in renseignementAdministratif)
                {
                    comp.Add(c);
                }
            }
            catch (Exception)
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

            string NewID = Convert.ToBase64String(BitConverter.GetBytes(idRenseignement)).Replace("==", "");
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
                return RedirectToAction("Verification", "Enregistrement", new { id = NewID });
            }
            return View(comp);
        }



        // GET: Competences/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            int ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);

            Competences competences = await db.Competences.FindAsync(ID);
            if (competences == null)
            {
                return HttpNotFound();
            }
            return View(competences);
        }

        // POST: Competences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            int ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);
            int idRenseignement = (int)Session["idRenseignement"];
            Competences competences = await db.Competences.FindAsync(ID);
            if (competences == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Competences.Remove(competences);
            await db.SaveChangesAsync();
            string NewID = Convert.ToBase64String(BitConverter.GetBytes(idRenseignement)).Replace("==", "");
            return RedirectToAction("Verification", "Enregistrement", new { id = NewID });
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
        public ActionResult Ajouter(string id)
        {
            int ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);
            int Id = (int)Session["idRenseignement"];
            if (ID != Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
            string NewID = Convert.ToBase64String(BitConverter.GetBytes(idRenseignement)).Replace("==", "");
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
                    return RedirectToAction("Verification", "Enregistrement", new { id = NewID });
                }
                catch (Exception)
                {
                    return HttpNotFound();
                }

            }
            return View("Ajouter");
        }
    }
}
