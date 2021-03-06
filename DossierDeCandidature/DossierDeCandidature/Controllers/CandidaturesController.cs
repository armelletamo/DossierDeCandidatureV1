﻿using System;
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
    public class CandidaturesController : Controller
    {
        private CandidatureContext db = new CandidatureContext();

        // GET: Candidatures
        public async Task<ActionResult> Index()
        {
            return View(await db.Candidatures.ToListAsync());
        }

        // GET: Candidatures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidature candidature = await db.Candidatures.FindAsync(id);
            if (candidature == null)
            {
                return HttpNotFound();
            }
            return View(candidature);
        }

        // GET: Candidatures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidatures/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Origine,Autre,statutActuel,PosteActuel,PosteSouhaite,Remuneration,RemunerationVoulu,Experience,Disponibilite,mobilité,Precision")] Candidature candidature)
        {
            if (ModelState.IsValid)
            {
                
                if (!candidature.mobilité)
                {
                    candidature.Precision = "";
                }
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Candidature = candidature;
                Session["administratif"] = candidatures;
                return RedirectToAction("Create", "Experiences");
            }

            return View(candidature);
        }

        // GET: Candidatures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            RenseignementAdministratifsController rens = new RenseignementAdministratifsController();
            id = rens.Entier();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidature candidature = await db.Candidatures.FindAsync(id);
            if (candidature == null)
            {
                return HttpNotFound();
            }
            return View(candidature);
        }

        // POST: Candidatures/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Edit([Bind(Include = "Id,Origine,Autre,statutActuel,PosteActuel,PosteSouhaite,Remuneration,RemunerationVoulu,Experience,Disponibilite,mobilité,Precision")] Candidature candidature)
        {
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.Candidature = candidature;
                Session["administratif"] = candidatures;
                return RedirectToAction("Edit", "Experiences");
            }
            return View(candidature);
        }

        // GET: Candidatures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidature candidature = await db.Candidatures.FindAsync(id);
            if (candidature == null)
            {
                return HttpNotFound();
            }
            return View(candidature);
        }

        // POST: Candidatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Candidature candidature = await db.Candidatures.FindAsync(id);
            db.Candidatures.Remove(candidature);
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
