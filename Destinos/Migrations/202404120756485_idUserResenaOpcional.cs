namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idUserResenaOpcional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Resenas", "IdUser", "dbo.AspNetUsers");
            DropIndex("dbo.Resenas", new[] { "IdUser" });
            AlterColumn("dbo.Resenas", "IdUser", c => c.String(maxLength: 128));
            CreateIndex("dbo.Resenas", "IdUser");
            AddForeignKey("dbo.Resenas", "IdUser", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resenas", "IdUser", "dbo.AspNetUsers");
            DropIndex("dbo.Resenas", new[] { "IdUser" });
            AlterColumn("dbo.Resenas", "IdUser", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Resenas", "IdUser");
            AddForeignKey("dbo.Resenas", "IdUser", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
