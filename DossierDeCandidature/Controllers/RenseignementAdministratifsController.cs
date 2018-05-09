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
        public ActionResult Create([Bind(Include = "Id,DateDeCreation,Nom,Prenom,NomJeuneFille,Adresse,CodePostal,Ville,indicatif,Telephone,Email,Secu,DateNaiss,LieuNaiss,AutorisationTravail,DateExpiration,PermisConduire,Vehicule,Handicap,AmenagementPoste")] RenseignementAdministratif renseignementAdministratif)
        {
            if (ModelState.IsValid)
            {
                //j'ouvre une session et je met le formulaire le temps que le candidat finisse de remplir le formulaire 
                Session.Timeout = 40;
                Session["administratif"] = renseignementAdministratif;
                return RedirectToAction("Create", "Candidatures");
            }
            ViewBag.indicatifValue = renseignementAdministratif.indicatif;
            return View(renseignementAdministratif);
        }

        // GET: RenseignementAdministratifs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            int? ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);
            int Id = (int)Session["idRenseignement"];
            if (ID != Id || ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RenseignementAdministratif renseignementAdministratif = await db.RenseignementsAdministratifs.FindAsync(ID);
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
        public ActionResult Edit([Bind(Include = "Id,DateDeCreation,Nom,Prenom,NomJeuneFille,Adresse,CodePostal,Ville,indicatif,Telephone,Email,Secu,DateNaiss,LieuNaiss,AutorisationTravail,DateExpiration,PermisConduire,Vehicule,Handicap,AmenagementPoste")] RenseignementAdministratif renseignementAdministratif)
        {
            string NewID = Convert.ToBase64String(BitConverter.GetBytes(renseignementAdministratif.Id)).Replace("==", "");
            if (ModelState.IsValid)
            {

                db.Entry(renseignementAdministratif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Verification", "Enregistrement", new { id = NewID });

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
