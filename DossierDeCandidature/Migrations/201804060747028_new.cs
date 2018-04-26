namespace DossierDeCandidature.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Langues", "Langue", c => c.String());
            AlterColumn("dbo.Langues", "NiveauLangue", c => c.String());
            DropColumn("dbo.Langues", "Autre");
            DropColumn("dbo.Langues", "Niveau");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Langues", "Niveau", c => c.String());
            AddColumn("dbo.Langues", "Autre", c => c.String());
            AlterColumn("dbo.Langues", "NiveauLangue", c => c.String(nullable: false));
            AlterColumn("dbo.Langues", "Langue", c => c.String(nullable: false));
        }
    }
}
