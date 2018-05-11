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
                try
                {
                    RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                    candidatures.Experience = experience;
                    Session["administratif"] = candidatures;
                    return RedirectToAction("Create", "Langues");
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

            return View(experience);
        }

        // GET: Experiences/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            int? ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);
            if (Session["idRenseignement"] == null)
            {
                return HttpNotFound();
            }
            int Id = (int)Session["idRenseignement"];
            if (ID == null && ID != Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = await db.Experiences.FindAsync(ID);
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
            try
            {
                int Id = (int)Session["idRenseignement"];
                string NewID = Convert.ToBase64String(BitConverter.GetBytes(Id)).Replace("==", "");
                if (ModelState.IsValid)
                {
                    db.Entry(experience).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Verification", "Enregistrement", new { id = NewID });
                }
                return View(experience);
            }
            catch
            {
                return HttpNotFound();
            }
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
