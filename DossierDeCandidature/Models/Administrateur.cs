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
        [Required]
        public string Identifiant { get; set; }
        [Display(Name = "Mot De Passe")]
        [DataType(DataType.Password)]
        [Required]
        public string MotDePasse { get; set; }
    }
}