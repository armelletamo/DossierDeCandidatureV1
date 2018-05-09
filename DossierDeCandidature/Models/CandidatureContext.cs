using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierDeCandidature.Models
{
    public class CandidatureContext : DbContext

    {
        public CandidatureContext() : base("name=DossierDeCandidatureConnect")
        {
            
            Database.SetInitializer<CandidatureContext>(new CreateDatabaseIfNotExists<CandidatureContext>());
        }


        public DbSet<RenseignementAdministratif> RenseignementsAdministratifs { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Competences> Competences { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Langues> Langues { get; set; }
        public DbSet<Motivation> Motivations { get; set; }
        public DbSet<References> References { get; set; }
        public DbSet<Administrateur> Administrateur { get; set; }
    }
}