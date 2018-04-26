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
    public class RenseignementAdministratifsController : Controller
    {
        private CandidatureContext db = new CandidatureContext();

        // GET: RenseignementAdministratifs
        public async Task<ActionResult> Index()
        {
            return View(await db.RenseignementsAdministratifs.ToListAsync());
        }

        // GET: RenseignementAdministratifs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RenseignementAdministratif renseignementAdministratif = await db.RenseignementsAdministratifs.FindAsync(id);
            if (renseignementAdministratif == null)
            {
                return HttpNotFound();
            }
            return View(renseignementAdministratif);
        }

        // GET: RenseignementAdministratifs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RenseignementAdministratifs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom,NomJeuneFille,Adresse,CodePostal,Ville,indicatif,Telephone,Email,Secu,DateNaiss,LieuNaiss,AutorisationTravail,DateExpiration,PermisConduire,Vehicule,Handicap,AmenagementPoste")] RenseignementAdministratif renseignementAdministratif)
        {
            if (ModelState.IsValid)
            {
                //j'ouvre une session et je met le formulaire le temps que le candidat finisse de remplir le formulaire 
                Session.Timeout = 40;
                Session["administratif"] = renseignementAdministratif;
                return RedirectToAction("Create", "Candidatures");
            }

            return View(renseignementAdministratif);
        }

        // GET: RenseignementAdministratifs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RenseignementAdministratif renseignementAdministratif = await db.RenseignementsAdministratifs.FindAsync(id);
            if (renseignementAdministratif == null)
            {
                return HttpNotFound();
            }
            return View(renseignementAdministratif);
        }

        // POST: RenseignementAdministratifs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,NomJeuneFille,Adresse,CodePostal,Ville,indicatif,Telephone,Email,Secu,DateNaiss,LieuNaiss,AutorisationTravail,DateExpiration,PermisConduire,Vehicule,Handicap,AmenagementPoste")] RenseignementAdministratif renseignementAdministratif)
        {
            if (ModelState.IsValid)
            {
                //ici on le candidat peut modifier les informations saisies 
                Session["administratif"] = renseignementAdministratif;
                return RedirectToAction("Edit", "Candidatures");

            }
            return View(renseignementAdministratif);
        }

        // GET: RenseignementAdministratifs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RenseignementAdministratif renseignementAdministratif = await db.RenseignementsAdministratifs.FindAsync(id);
            if (renseignementAdministratif == null)
            {
                return HttpNotFound();
            }
            return View(renseignementAdministratif);
        }

        // POST: RenseignementAdministratifs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RenseignementAdministratif renseignementAdministratif = await db.RenseignementsAdministratifs.FindAsync(id);
            db.RenseignementsAdministratifs.Remove(renseignementAdministratif);
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
        public int? Entier()
        {
            var renseignement = (RenseignementAdministratif)Session["administratif"];
            int? id = db.RenseignementsAdministratifs
              .Where(x => x.Secu == renseignement.Secu)
              .Select(x => x.Id).FirstOrDefault();
            return id;
        }
    }
}
