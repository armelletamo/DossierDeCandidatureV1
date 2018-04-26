using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierDeCandidature.Models
{
    public class Motivation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "COMMENT AVEZ VOUS CONNU EXPERIS-IT ET QUEL IMAGE EN AVEZ VOUS?")]
        public string ExperisIt { get; set; }
        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "QUELS SONT LES MOTIFS DE VOTRE RECHERCHE/CANDIDATURE?")]
        public string MotifRecherche { get; set; }
        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "QUELS SONT VOS OBJECTIFS DE CARRIERE A MOYEN TERME?")]
        public string Objectif { get; set; }

        //public RenseignementAdministratif RenseignementAdministratif { get; set; }
    }
}
