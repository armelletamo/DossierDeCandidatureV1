﻿using DossierDeCandidature.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            try
            {
                RenseignementAdministratif renseignementAdministratif = (RenseignementAdministratif)Session["administratif"];
                if (renseignementAdministratif == null)
                {
                    RedirectToAction("Index", "Home");
                }
                db.RenseignementsAdministratifs.Add(renseignementAdministratif);
                await db.SaveChangesAsync();
                string NewID = string.Empty;
                int Id = (db.RenseignementsAdministratifs.Where(r => r.Secu == renseignementAdministratif.Secu).FirstOrDefault()).Id;
                Session.Timeout = 40;
                Session["idRenseignement"] = Id;
                NewID = Convert.ToBase64String(BitConverter.GetBytes(Id)).Replace("==", "");
            }
            catch
            {
                return HttpNotFound();
            }

            return RedirectToAction("Verification", "Enregistrement", new { id = NewID });

        }

        //Affichage des données de l'utilisateur pour vérification

        public async Task<ActionResult> Verification(string id)
        {
            CandidatureVM cand = new CandidatureVM();

            //int Id = (int)Session["idRenseignement"];

            try
            {
                int? ID = BitConverter.ToInt32(Convert.FromBase64String(id + "=="), 0);
                //int Id = (int)Session["idRenseignement"];
                if (ID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //Affectation de la propriété Renseignement de l'objet cand de type CandidatureVM

                var renseignementAdministratif = await db.RenseignementsAdministratifs
                   .Where(x => x.Id == ID)
                   .Include(x => x.Candidature)
                    .Include(x => x.Experience)
                     .Include(x => x.Langues)
                      .Include(x => x.Competences)
                       .Include(x => x.References)
                        .Include(x => x.Motivation)
                        .FirstOrDefaultAsync();

                cand.Renseignement = renseignementAdministratif;
                return View(cand);
            }
            catch
            {
                return HttpNotFound();

            }
        }

        //Generation du pdf et envoi par mail
        public ActionResult PrintRenseignement(string Stringid)
        {
            if (Session["idRenseignement"] == null)
                return HttpNotFound();
            try
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
                if (renseignementAdministratif == null)
                {
                    return new HttpNotFoundResult();
                }
                string nom = renseignementAdministratif.Nom + " " + renseignementAdministratif.Prenom;

                var report = new ActionAsPdf("Verification", new { id = Stringid });
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

                MailAddress to = new MailAddress(ConfigurationManager.AppSettings["Destinataire"]);
                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(from, to);

                mm.Subject = nom;
                mm.Body = "Ci-joint le dossier de candidature de " + nom;
                mm.Attachments.Add(new Attachment(new MemoryStream(applicationPDFData), "Dossier_De_Candidature.pdf"));
                mm.IsBodyHtml = true;


                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["smtpHost"];
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();

                //compte gmail de test de la reception du mail et mot de passe

                NetworkCred.UserName = ConfigurationManager.AppSettings["Expediteur"];
                NetworkCred.Password = ConfigurationManager.AppSettings["ExpediteurPwd"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                int port;
                Int32.TryParse(ConfigurationManager.AppSettings["Port"], out port);

                smtp.Port = port;
                smtp.Send(mm);
            }
            catch
            {
                //erreur d'envoie du mail 
                //if(renseignementAdministratif)
                //var report = new ActionAsPdf("Verification", new { id = Stringid });
                //report.PageOrientation = Rotativa.Options.Orientation.Portrait;
                //report.FileName = "Dossier_De_Candidature.pdf";
                //report.PageSize = Rotativa.Options.Size.A4;
                //report.CustomSwitches = "--print-media-type " + /*"--no-images " +*/ "--disable-smart-shrinking";
                //report.PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0);

                //return report;
                HttpNotFound();
            }
            //*********************************************
            Session.Clear();
            //return report;
            return RedirectToAction("Index", "Home");
        }

    }
}