namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urlFotoOpcional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fotos", "UrlFoto", c => c.String(maxLength: 55));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fotos", "UrlFoto", c => c.String(nullable: false, maxLength: 55));
        }
    }
}
