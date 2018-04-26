using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierDeCandidature.Models
{
    public class References
    {
        public int? Id { get; set; }
        [Display(Name = "Nom/Prenom")]
        public string NomPrenom { get; set; }

        public string Fonction { get; set; }
        [Display(Name = "Société")]
        public string Societe { get; set; }
        [Display(Name = "Tel/Mail")]
        public string TelMail { get; set; }
       
        public RenseignementAdministratif RenseignementAdministratif { get; set; }
    }
}
