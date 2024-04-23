namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReorderColumnsInDestinos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Destinos", "Comunidad_Region", c => c.String(maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Destinos", "Comunidad_Region", c => c.String(nullable: false, maxLength: 35));
        }
    }
}
