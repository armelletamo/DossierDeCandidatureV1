using DossierDeCandidature.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DossierDeCandidature.Controllers
{
    public class EnregistrementController : Controller
    {
        private CandidatureContext db = new CandidatureContext();

        public async Task<ActionResult> Enregistrement()
        {
            RenseignementAdministratif renseignementAdministratif = (RenseignementAdministratif)Session["administratif"];
            db.RenseignementsAdministratifs.Add(renseignementAdministratif);
            await db.SaveChangesAsync();
            return RedirectToAction("Verification", "Enregistrement");
        }

        public async Task<ActionResult> Verification()
        {
            CandidatureVM cand = new CandidatureVM();

            var renseignement = (RenseignementAdministratif)Session["administratif"];
            int? id = db.RenseignementsAdministratifs
                .Where(x => x.Secu == renseignement.Secu)
                .Select(x => x.Id).FirstOrDefault();

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var renseignementAdministratif = await db.RenseignementsAdministratifs
               .Where(x => x.Id == id)
               .Include(x => x.Candidature)
                .Include(x => x.Experience)
                 .Include(x => x.Langues)
                  .Include(x => x.Competences)
                   .Include(x => x.References)
                    .Include(x => x.Motivation)
                    .FirstOrDefaultAsync();
            cand.Renseignement = renseignementAdministratif;
            if (renseignementAdministratif == null)
            {
                return HttpNotFound();
            }
            return View(cand);
        }

        public async Task<ActionResult> ModifierEnregistrement()
        {
            RenseignementAdministratif renseignementAdministratif = (RenseignementAdministratif)Session["administratif"];
            db.Entry(renseignementAdministratif).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Verification", "Enregistrement");
        }
    }
}