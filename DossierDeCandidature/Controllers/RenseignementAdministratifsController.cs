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
                               
                db.Entry(renseignementAdministratif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Verification", "Enregistrement");

            }
            return View(renseignementAdministratif);
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
