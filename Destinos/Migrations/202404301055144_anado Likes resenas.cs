namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadoLikesresenas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resenas", "Likes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resenas", "Likes");
        }
    }
}
