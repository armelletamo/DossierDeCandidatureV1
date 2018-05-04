using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierDeCandidature.Models
{
    public class Candidature : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Origine de votre candidature")]
        public string Origine { get; set; }

        [Display(Name = "Autre(précisez)")]
        public string Autre { get; set; }

        [Required]
        [Display(Name = "Statut actuel")]
        public string statutActuel { get; set; }

        
        [Display(Name = "Poste actuel")]
        public string PosteActuel { get; set; }

        [Required]
        [Display(Name = "Poste souhaité")]
        public string PosteSouhaite { get; set; }

        
        [Display(Name = "Rémunération annuelle brute actuelle (euro)")]
        public decimal? Remuneration { get; set; }

        [Required]
        [Display(Name = "Rémunération annuelle brute souhaitée (euro)")]
        public decimal RemunerationVoulu { get; set; }

        [Required]
        [Display(Name = "Nombre d'année d'expérience")]
        public int Experience { get; set; }

        [Required]
        [Display(Name = "Date de disponiblité")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Disponibilite { get; set; }

        [Required]
        [Display(Name = "Cochez la case si Mobilité géographique")]
        public bool mobilité { get; set; }
        
        [Display(Name = "si oui précisez")]
        public string Precision { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Candidature cand = (Candidature)validationContext.ObjectInstance;

            if (cand.Disponibilite.Day < DateTime.Today.Day)
            {

                yield return new ValidationResult("la date ne peux pas être antérieur au :" + DateTime.Now.ToShortDateString(), new string[] { "Disponibilite" });
            }
        }
    }
   
}
