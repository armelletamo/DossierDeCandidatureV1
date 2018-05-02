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
    public class LanguesController : Controller
    {
        private CandidatureContext db = new CandidatureContext();


        // GET: Langues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Langues/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Langue,NiveauLangue")] ICollection<Langues> langues)
        {
            ICollection<Langues> languesNotNull = new List<Langues>();
            if (ModelState.IsValid)
            {
                foreach (var item in langues)
                {
                    if (item.Langue != null)
                        languesNotNull.Add(item);
                }
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                if (candidatures == null)
                {
                    //Session inexistante ou Expiré, redirigé vers la page d'accueil
                    return new HttpNotFoundResult();

                }
                candidatures.Langues = languesNotNull;
                Session["administratif"] = candidatures;
                return RedirectToAction("Create", "Competences");
            }

            return View("Create");
        }

        // GET: Langues/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            List<Langues> lang = new List<Langues>();
            int ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);
            int Id = (int)Session["idRenseignement"];
            if (ID != Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Session["idRenseignement"] = id;
            try
            {

                var renseignementAdministratif = await db.Langues
                    .Where(x => x.RenseignementAdministratif.Id == ID)
                         .Include("RenseignementAdministratif")
                         .ToListAsync();
                foreach (var c in renseignementAdministratif)
                {
                    lang.Add(c);
                }


                if (lang == null)
                {
                    return HttpNotFound();
                }
                return View(lang);
            }
            catch (Exception )
            {
                return HttpNotFound();
            }
        }

        // POST: Langues/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Langue,NiveauLangue")] ICollection<Langues> langues)
        {
            int Id = (int)Session["idRenseignement"];
            string NewID = Convert.ToBase64String(BitConverter.GetBytes(Id)).Replace("==", "");
            if (ModelState.IsValid)
            {
                foreach (var item in langues)
                {
                    if (item.Langue != null)
                    {
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                
                return RedirectToAction("Verification", "Enregistrement", new { id = NewID });
            }
            return View(langues);
        }

        // GET: Langues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Langues langues = await db.Langues.FindAsync(id);
            if (langues == null)
            {
                return HttpNotFound();
            }
            return View(langues);
        }

        // POST: Langues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Langues langues = await db.Langues.FindAsync(id);
            if (langues == null)
            {
                return HttpNotFound();
            }
            db.Langues.Remove(langues);
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

        // GET: Langues/AjouterLangue
        public ActionResult AjouterLangue(int? id)
        {
            Session["idRenseignement"] = id;
            return View();
        }

        // POST: Langues/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjouterLangue([Bind(Include = "Id,Langue,NiveauLangue")] ICollection<Langues> langues)
        {
            var idRenseignement = (int)Session["idRenseignement"];
            if (ModelState.IsValid)
            {
                try
                {
                    var renseignementAdministratif = db.RenseignementsAdministratifs
                    .Where(x => x.Id == idRenseignement)
                      .Include(x => x.Langues)
                         .FirstOrDefault();

                    foreach (var item in langues)
                    {
                        if (item.Langue != null)
                        {
                            db.Langues.Add(item);
                            db.SaveChanges();
                            renseignementAdministratif.Langues.Add(item);
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                catch
                {
                    return new HttpNotFoundResult();
                }
                return RedirectToAction("Verification", "Enregistrement");
            }
            return View("AjouterLangue");
        }
    }
}
