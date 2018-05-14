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
    public class AdministrateursController : Controller
    {
        private CandidatureContext db = new CandidatureContext();

        // GET: Administrateurs        
        public async Task<ActionResult> Index()
        {
            List<RenseignementAdministratif> rens = new List<RenseignementAdministratif>();
            var date = DateTime.Today;
            List<RenseignementAdministratif> liste = null;
            try
            {
                liste = await db.RenseignementsAdministratifs.Where(x => x.DateDeCreation.Year == (date.Year-1)).ToListAsync();
                //liste = await db.RenseignementsAdministratifs.ToListAsync();
            }
            catch (Exception)
            {

            }
            if (liste.Count == 0)
            {
                return View("NoCandidate");
            }

            else
            {
                foreach (var item in liste)
                {
                    var date1 = liste.Select(x => x.DateDeCreation).FirstOrDefault();
                    if ((date - date1).TotalDays >= 365)
                    {
                        rens.Add(item);
                    }
                }
                return View(rens);
            }
        }



        // GET: Administrateurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Identifiant,MotDePasse")] Administrateur administrateur)
        {
            var idf = db.Administrateur.Select(x => x.Identifiant).First();
            var mdp = db.Administrateur.Select(x => x.MotDePasse).First();
            if (ModelState.IsValid)
            {
                if (administrateur.Identifiant == idf && administrateur.MotDePasse == mdp)
                {                   
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Identifiant ou Mot De Passe Incorrect";
                    return View("Create");
                }

            }

            return View(administrateur);
        }

        // GET: Administrateurs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrateur administrateur = await db.Administrateur.FindAsync(id);
            if (administrateur == null)
            {
                return HttpNotFound();
            }
            return View(administrateur);
        }

        // POST: Administrateurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Identifiant,MotDePasse")] Administrateur administrateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrateur).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(administrateur);
        }

        // GET: Administrateurs/Delete/5       
        public async Task<ActionResult> Delete(FormCollection formCollection)
        {
            string[] ids = formCollection["Id"].Split(new char[] { ',' });
            foreach(string id in ids)
            {
                int Id = int.Parse(id);
                var utilisateur = await db.RenseignementsAdministratifs
                                .Include(x => x.Candidature)
                                .Include(x => x.Experience)
                                .Include(x => x.Langues)
                                .Include(x => x.Competences)
                                .Include(x => x.References)
                                .Include(x => x.Motivation)
                                .SingleAsync(a => a.Id == Id);

                for (int i = 0; i < utilisateur.References.Count; i++)
                {
                    db.References.Remove(utilisateur.References.ElementAt(i));
                }
                for (int i = 0; i < utilisateur.Competences.Count; i++)
                {
                    db.Competences.Remove(utilisateur.Competences.ElementAt(i));
                }
                for (int i = 0; i < utilisateur.Langues.Count; i++)
                {
                    db.Langues.Remove(utilisateur.Langues.ElementAt(i));
                }
                db.Motivations.Remove(utilisateur.Motivation);
                db.Candidatures.Remove(utilisateur.Candidature);
                db.Experiences.Remove(utilisateur.Experience);
                db.RenseignementsAdministratifs.Remove(utilisateur);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Administrateurs");
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
