using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierDeCandidature.Models
{
    public class Experience: IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Formation")]
        public string Formation { get; set; }
        [Required]
        [Display(Name = "Dernier diplôme obtenu")]
        public string Diplome { get; set; }
        [Required]
        [Display(Name = "Université/Ecole/Organisme")]
        public string Ecole { get; set; }
        [Required]
        [RegularExpression("^[1-2][0|9][0-9]{2}$", ErrorMessage = "Année incorrect")]
        [Display(Name = "Année d'obtention")]
        public string Annee { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Experience formantion = (Experience)validationContext.ObjectInstance;
            int annee = int.Parse(formantion.Annee);
            if (annee > DateTime.Now.Year)
            {
                yield return new ValidationResult("Année 2 incorrect", new string[] { "Annee" });
            }
        }
    }
}
