namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16apr_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resenas", "UrlFoto", c => c.String(maxLength: 900));
            AlterColumn("dbo.Fotos", "UrlFoto", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fotos", "UrlFoto", c => c.String(maxLength: 55));
            AlterColumn("dbo.Resenas", "UrlFoto", c => c.String(maxLength: 100));
        }
    }
}
