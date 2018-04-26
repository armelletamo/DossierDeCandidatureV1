using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DossierDeCandidature.Models
{
   public class Langues
    {
        public int? Id { get; set; }
        
        [Display(Name = "Langue")]
        public  string Langue { get; set; }
     
        [Display(Name = "Niveau")]
        public string NiveauLangue { get; set; }
        [NotMapped]
        public int RenseignementAdministratif_Id { get; set; }

        public RenseignementAdministratif RenseignementAdministratif { get; set; }

       
    }

}
