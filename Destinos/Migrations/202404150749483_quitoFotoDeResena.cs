namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quitoFotoDeResena : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resenas", "UrlFoto", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resenas", "UrlFoto");
        }
    }
}
