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

        // GET: References
        public async Task<ActionResult> Index()
        {
            return View(await db.References.ToListAsync());
        }

        // GET: References/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            References references = await db.References.FindAsync(id);
            if (references == null)
            {
                return HttpNotFound();
            }
            return View(references);
        }

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
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.References = references;
                Session["administratif"] = candidatures;
                return RedirectToAction("Create", "Motivations");
            }

            return View(references);
        }

        // GET: References/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            RenseignementAdministratifsController rens = new RenseignementAdministratifsController();
            id = rens.Entier();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            References references = await db.References.FindAsync(id);
            if (references == null)
            {
                return HttpNotFound();
            }
            return View(references);
        }

        // POST: References/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomPrenom,Fonction,Societe,TelMail")] ICollection<References> references)
        {
            if (ModelState.IsValid)
            {
                RenseignementAdministratif candidatures = (RenseignementAdministratif)Session["administratif"];
                candidatures.References = references;
                Session["administratif"] = candidatures;
                return RedirectToAction("Edit", "Motivations");
            }
            return View(references);
        }

        // GET: References/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            References references = await db.References.FindAsync(id);
            if (references == null)
            {
                return HttpNotFound();
            }
            return View(references);
        }

        // POST: References/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            References references = await db.References.FindAsync(id);
            db.References.Remove(references);
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
