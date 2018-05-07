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
    public class ReferencesController : Controller
    {
        private CandidatureContext db = new CandidatureContext();

        // GET: References/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: References/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomPrenom,Fonction,Societe,TelMail")] ICollection<References> references)
        {
            ICollection<References> referencesNotNull = new List<References>();
            if (ModelState.IsValid)
            {
                try
                {

                    RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                    foreach (var item in references)
                    {

                        if (!(item.NomPrenom == null && item.Societe == null && item.Fonction == null && item.TelMail == null))
                            referencesNotNull.Add(item);
                    }
                    candidatures.References = referencesNotNull;
                    Session["administratif"] = candidatures;
                    return RedirectToAction("Create", "Motivations");
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

            return View(references);
        }

        // GET: References/Edit/5
        public async Task<ActionResult> Edit(string id)
        {

            List<References> refe = new List<References>();
            int ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);
            int Id = (int)Session["idRenseignement"];
            if (Session["idRenseignement"] == null)
            {
                ViewBag.messageExpirationSession = "La session a expiré veuillez resaisir vos informations";
                return View("~/Views/Home/Index.cshtml");
            }
            if (ID != Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var compet = await db.References
                 .Where(x => x.RenseignementAdministratif.Id == ID)
                      .Include("RenseignementAdministratif")
                      .ToListAsync();
            if (compet == null)
            {
                return HttpNotFound();
            }
            foreach (var v in compet)
            {
                refe.Add(v);
            }

            if (refe == null)
            {
                return HttpNotFound();
            }
            return View(refe);
        }

        // POST: References/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomPrenom,Fonction,Societe,TelMail")] ICollection<References> references)
        {
            ICollection<References> referencesNotNull = new List<References>();
            var idRenseignement = (int)Session["idRenseignement"];
            if (Session["idRenseignement"] == null)
            {
                ViewBag.messageExpirationSession = "La session a expiré veuillez resaisir vos informations";
                return View("~/Views/Home/Index.cshtml");
            }

            string NewID = Convert.ToBase64String(BitConverter.GetBytes(idRenseignement)).Replace("==", "");
            References reference = null;
            foreach (var item in references)
            {

                if (!(item.NomPrenom == null && item.Societe == null && item.Fonction == null && item.TelMail == null))
                {
                    referencesNotNull.Add(item);
                }
                else
                {
                    reference = db.References.Find(item.Id);
                    if (reference == null)
                    {
                        return HttpNotFound();
                    }
                    db.References.Remove(reference);
                    db.SaveChanges();
                }
            }
            if (ModelState.IsValid)
            {
                foreach (var item in referencesNotNull)
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Verification", "Enregistrement", new { id = NewID });
            }
            return View(references);
        }

        // GET: References/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            int ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);

            References references = await db.References.FindAsync(ID);
            if (references == null)
            {
                return HttpNotFound();
            }
            return View(references);
        }

        // POST: References/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            int ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);
            int idRenseignement = (int)Session["idRenseignement"];

            References references = await db.References.FindAsync(ID);
            if (references == null)
            {
                return HttpNotFound();
            }
            db.References.Remove(references);
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
        // GET: References/Ajouter
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

        // POST: References/Ajouter
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ajouter([Bind(Include = "Id,NomPrenom,Fonction,Societe,TelMail")] ICollection<References> references)
        {
            var idRenseignement = (int)Session["idRenseignement"];
            string NewID = Convert.ToBase64String(BitConverter.GetBytes(idRenseignement)).Replace("==", "");

            if (ModelState.IsValid)
            {
                try
                {

                    var renseignementAdministratif = db.RenseignementsAdministratifs
                    .Where(x => x.Id == idRenseignement)
                      .Include(x => x.References)
                         .FirstOrDefault();
                    foreach (var item in references)
                    {
                        if (!(item.NomPrenom == null && item.Societe == null && item.Fonction == null && item.TelMail == null))
                        {
                            db.References.Add(item);
                            db.SaveChanges();
                            renseignementAdministratif.References.Add(item);
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                catch
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Verification", "Enregistrement", new { id = NewID });
            }
            return View("Ajouter");
        }
    }
}
