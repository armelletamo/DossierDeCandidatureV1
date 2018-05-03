namespace DossierDeCandidature.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Origine = c.String(nullable: false),
                        Autre = c.String(),
                        statutActuel = c.String(nullable: false),
                        PosteActuel = c.String(),
                        PosteSouhaite = c.String(nullable: false),
                        Remuneration = c.Decimal(precision: 18, scale: 2),
                        RemunerationVoulu = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Experience = c.Int(nullable: false),
                        Disponibilite = c.DateTime(nullable: false),
                        mobilité = c.Boolean(nullable: false),
                        Precision = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Competences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Competence = c.String(maxLength: 40),
                        NiveauCompetence = c.String(),
                        RenseignementAdministratif_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RenseignementAdministratifs", t => t.RenseignementAdministratif_Id)
                .Index(t => t.RenseignementAdministratif_Id);
            
            CreateTable(
                "dbo.RenseignementAdministratifs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        NomJeuneFille = c.String(),
                        Adresse = c.String(nullable: false),
                        CodePostal = c.String(nullable: false),
                        Ville = c.String(nullable: false),
                        indicatif = c.String(nullable: false, maxLength: 5),
                        Telephone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Secu = c.String(nullable: false),
                        DateNaiss = c.DateTime(nullable: false),
                        LieuNaiss = c.String(nullable: false),
                        AutorisationTravail = c.Boolean(nullable: false),
                        DateExpiration = c.DateTime(),
                        PermisConduire = c.Boolean(nullable: false),
                        Vehicule = c.Boolean(nullable: false),
                        Handicap = c.Boolean(nullable: false),
                        AmenagementPoste = c.Boolean(nullable: false),
                        Candidature_Id = c.Int(),
                        Experience_Id = c.Int(),
                        Motivation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidatures", t => t.Candidature_Id)
                .ForeignKey("dbo.Experiences", t => t.Experience_Id)
                .ForeignKey("dbo.Motivations", t => t.Motivation_Id)
                .Index(t => t.Candidature_Id)
                .Index(t => t.Experience_Id)
                .Index(t => t.Motivation_Id);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Formation = c.String(nullable: false),
                        Diplome = c.String(nullable: false),
                        Ecole = c.String(nullable: false),
                        Annee = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Langues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Langue = c.String(),
                        NiveauLangue = c.String(),
                        RenseignementAdministratif_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RenseignementAdministratifs", t => t.RenseignementAdministratif_Id)
                .Index(t => t.RenseignementAdministratif_Id);
            
            CreateTable(
                "dbo.Motivations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExperisIt = c.String(nullable: false),
                        MotifRecherche = c.String(nullable: false),
                        Objectif = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.References",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomPrenom = c.String(),
                        Fonction = c.String(),
                        Societe = c.String(),
                        TelMail = c.String(),
                        RenseignementAdministratif_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RenseignementAdministratifs", t => t.RenseignementAdministratif_Id)
                .Index(t => t.RenseignementAdministratif_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.References", "RenseignementAdministratif_Id", "dbo.RenseignementAdministratifs");
            DropForeignKey("dbo.RenseignementAdministratifs", "Motivation_Id", "dbo.Motivations");
            DropForeignKey("dbo.Langues", "RenseignementAdministratif_Id", "dbo.RenseignementAdministratifs");
            DropForeignKey("dbo.RenseignementAdministratifs", "Experience_Id", "dbo.Experiences");
            DropForeignKey("dbo.Competences", "RenseignementAdministratif_Id", "dbo.RenseignementAdministratifs");
            DropForeignKey("dbo.RenseignementAdministratifs", "Candidature_Id", "dbo.Candidatures");
            DropIndex("dbo.References", new[] { "RenseignementAdministratif_Id" });
            DropIndex("dbo.Langues", new[] { "RenseignementAdministratif_Id" });
            DropIndex("dbo.RenseignementAdministratifs", new[] { "Motivation_Id" });
            DropIndex("dbo.RenseignementAdministratifs", new[] { "Experience_Id" });
            DropIndex("dbo.RenseignementAdministratifs", new[] { "Candidature_Id" });
            DropIndex("dbo.Competences", new[] { "RenseignementAdministratif_Id" });
            DropTable("dbo.References");
            DropTable("dbo.Motivations");
            DropTable("dbo.Langues");
            DropTable("dbo.Experiences");
            DropTable("dbo.RenseignementAdministratifs");
            DropTable("dbo.Competences");
            DropTable("dbo.Candidatures");
        }
    }
}
