using DossierDeCandidature.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;

namespace DossierDeCandidature.Controllers
{
    public class EnregistrementController : Controller
    {
        private CandidatureContext db = new CandidatureContext();
        // enregistrement des données en session
        public async Task<ActionResult> Enregistrement()
        {
            RenseignementAdministratif renseignementAdministratif = (RenseignementAdministratif)Session["administratif"];
            if (renseignementAdministratif == null)
            {
                RedirectToAction("Index", "Home");
            }
            db.RenseignementsAdministratifs.Add(renseignementAdministratif);
            await db.SaveChangesAsync();
            try
            {
                int Id = (db.RenseignementsAdministratifs.Where(r => r.Secu == renseignementAdministratif.Secu).FirstOrDefault()).Id;
                Session["idRenseignement"] = Id;
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Verification", "Enregistrement");

        }

        //Affichage des données de l'utilisateur pour vérification

        public async Task<ActionResult> Verification()
        {
            CandidatureVM cand = new CandidatureVM();

            int? id = (int)Session["idRenseignement"];


            if (id == null)
            {
                //Session expiré, redirigé vers la page d'accueil
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Affectation de la propriété Renseignement de l'objet cand de type CandidatureVM
            var renseignementAdministratif = await db.RenseignementsAdministratifs
               .Where(x => x.Id == id)
               .Include(x => x.Candidature)
                .Include(x => x.Experience)
                 .Include(x => x.Langues)
                  .Include(x => x.Competences)
                   .Include(x => x.References)
                    .Include(x => x.Motivation)
                    .FirstOrDefaultAsync();
            if (renseignementAdministratif == null)
            {
                return HttpNotFound();
            }
            cand.Renseignement = renseignementAdministratif;
            return View(cand);
        }

        //Generation du pdf et envoi par mail
        public ActionResult PrintRenseignement()
        {
            int ID = (int)Session["idRenseignement"];
            var renseignementAdministratif = db.RenseignementsAdministratifs
               .Where(x => x.Id == ID)
               .Include(x => x.Candidature)
                .Include(x => x.Experience)
                 .Include(x => x.Langues)
                  .Include(x => x.Competences)
                   .Include(x => x.References)
                    .Include(x => x.Motivation)
                    .FirstOrDefault();
            string nom = renseignementAdministratif.Nom + " " + renseignementAdministratif.Prenom;
            var report = new ActionAsPdf("Verification", new { Id = ID });
            report.PageOrientation = Rotativa.Options.Orientation.Portrait;
            report.FileName = "Dossier_De_Candidature.pdf";
            report.PageSize = Rotativa.Options.Size.A4;
            report.CustomSwitches = "--print-media-type " + /*"--no-images " +*/ "--disable-smart-shrinking";
            report.PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0);



            //Envoi du mail***************************
            byte[] applicationPDFData = report.BuildFile(ControllerContext);
            //email from : noreply@YourDomain.com
            string mailFrom = "experisetest@gmail.com";
            MailAddress from = new MailAddress(mailFrom, "Candidature");
            //hedi.lachtane@experis-it.fr
            MailAddress to = new MailAddress("armelle.youmbi@experis-it.fr");
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(from, to);

            mm.Subject = "Dossier de candidature";
            mm.Body = "Ci-joint le dossier de candidature de " + nom;
            mm.Attachments.Add(new Attachment(new MemoryStream(applicationPDFData), "Dossier_De_Candidature.pdf"));
            mm.IsBodyHtml = true;


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            //commpte gmail de test de la reception du mail et mot de passe
            NetworkCred.UserName = "experisetest@gmail.com";
            NetworkCred.Password = "test2018";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            try
            {

                smtp.Send(mm);
            }
            catch (Exception)
            {
                return report;
            }
            //*********************************************
            Session.Clear();
            //return report;
            return RedirectToAction("Index", "Home");
        }

    }
}