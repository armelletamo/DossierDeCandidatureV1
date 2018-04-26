namespace DossierDeCandidature.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Candidatures", "Remuneration", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Candidatures", "Remuneration", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
