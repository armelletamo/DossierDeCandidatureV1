using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DossierDeCandidature.Models
{
    public class Administrateur
    {
        public int Id { get; set; }
        [Display(Name = "Identifiant")]
        [Required(ErrorMessage = "Identifiant requis")]
        public string Identifiant { get; set; }
        [Display(Name = "Mot De Passe")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mot de passe requis")]
        public string MotDePasse { get; set; }
    }
}