namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadirFoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fotos", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Fotos", "UrlFoto", c => c.String(nullable: false, maxLength: 55));
            CreateIndex("dbo.Fotos", "ApplicationUser_Id");
            AddForeignKey("dbo.Fotos", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Fotos", "Descripción");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fotos", "Descripción", c => c.String(maxLength: 15));
            DropForeignKey("dbo.Fotos", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Fotos", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Fotos", "UrlFoto", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.Fotos", "ApplicationUser_Id");
        }
    }
}
