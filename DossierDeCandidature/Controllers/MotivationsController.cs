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
                if(candidatures==null)
                    return HttpNotFound();
                candidatures.Motivation = motivation;
                Session["administratif"] = candidatures;
                return RedirectToAction("Enregistrement", "Enregistrement");

            }

            return View(motivation);
        }

        // GET: Motivations/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: Motivations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExperisIt,MotifRecherche,Objectif")] Motivation motivation)
        {
            var idRenseignement = (int)Session["idRenseignement"];
            if (ModelState.IsValid)
            {
                db.Entry(motivation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Verification", "Enregistrement");
            }
            return View(motivation);
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
