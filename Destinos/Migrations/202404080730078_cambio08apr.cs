namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio08apr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Destinos", "Comunidad_Region", c => c.String(nullable: false, maxLength: 35));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Destinos", "Comunidad_Region");
        }
    }
}
