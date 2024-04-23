namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09apr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resenas", "Titulo", c => c.String(maxLength: 45));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resenas", "Titulo", c => c.String(maxLength: 15));
        }
    }
}
