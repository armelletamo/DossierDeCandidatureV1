using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierDeCandidature.Models
{
    public class RenseignementAdministratif : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Display(Name = "Nom de jeune fille")]
        public string NomJeuneFille { get; set; }

        [Required]

        public string Adresse { get; set; }

        [Required]
        [Display(Name = "Code postal")]
        [RegularExpression("^(?:[0-9]{5}|)$", ErrorMessage = "Code postal invalide")]
        public string CodePostal { get; set; }

        [Required]
        public string Ville { get; set; }

        [Required]
        [Display(Name = "Indicatif du pays")]
        [MaxLength(5, ErrorMessage = "Veuillez saisir l'indicatif")]
        public string indicatif { get; set; }

        [Required]
        [Display(Name = "Téléphone")]
        [RegularExpression("^0[1-9]([-. ]?[0-9]{2}){4}$", ErrorMessage = "Numero de telephone incorrect")]
        public string Telephone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email invalide")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Numéro de sécurité sociale")]
        [RegularExpression("^[12][ .-]?[0-9]{2}[ .-]?(0[1-9]|[1][0-2])[ .-]?([0-9]{2}|2A|2B)[ .-]?[0-9]{3}[ .-]?[0-9]{3}[ .-]?[0-9]{2}$", ErrorMessage = "Numero de securité sociale incorrect")]
        public string Secu { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Date de naissance")]
        public DateTime DateNaiss { get; set; }

        [Required]
        [Display(Name = "lieu de naissance")]
        public string LieuNaiss { get; set; }

        [Required]
        [Display(Name = "Cocher la case si autorisation de travail(si de nationalité étrangère)")]
        public bool AutorisationTravail { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date d'expiration")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateExpiration { get; set; }

        [Required]
        [Display(Name = "Cocher la case si vous possédez un permis de conduire")]
        public bool PermisConduire { get; set; }

        [Required]
        [Display(Name = "Cocher la case si vous possédez un véhicule")]
        public bool Vehicule { get; set; }

        [Required]
        [Display(Name = "Cocher la case si vous avez la qualité de travailleur handicapé")]
        public bool Handicap { get; set; }

        [Required]
        [Display(Name = "Cocher la case si un aménagement de poste de travail est nécessaire")]
        public bool AmenagementPoste { get; set; }

        public Candidature Candidature { get; set; }

        public Experience Experience { get; set; }

        public ICollection<Langues> Langues { get; set; }

        public ICollection<Competences> Competences { get; set; }

        public ICollection<References> References { get; set; }

        public Motivation Motivation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            RenseignementAdministratif rens = (RenseignementAdministratif)validationContext.ObjectInstance;

            if (rens.DateNaiss > DateTime.Now)
            {

                yield return new ValidationResult("la date ne peux pas être supérieur au :" + DateTime.Now.ToShortDateString(), new string[] { "DateNaiss" });
            }
            if (rens.DateExpiration < DateTime.Now)
            {

                yield return new ValidationResult("la date ne peux pas être antérieur au :" + DateTime.Now.ToShortDateString(), new string[] { "DateExpiration" });
            }
        }
    }
}
