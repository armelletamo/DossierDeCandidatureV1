using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DossierDeCandidature.Models
{
   public class Langues
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Langue")]
        public  string Langue { get; set; }
        [Required]
        [Display(Name = "Niveau")]
        public string NiveauLangue { get; set; }

        public string Autre { get; set; }
        public string Niveau { get; set; }


        public RenseignementAdministratif RenseignementAdministratif { get; set; }

       
    }

}
