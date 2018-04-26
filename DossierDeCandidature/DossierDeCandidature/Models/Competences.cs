using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierDeCandidature.Models
{
    public class Competences
    {
        public int? Id { get; set; }
        [MaxLength(40)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Compétences")]
        public string Competence { get; set; }
        [Display(Name = "Niveau")]
        public string NiveauCompetence { get; set; }
        
        public RenseignementAdministratif RenseignementAdministratif { get; set; }
    }
}
