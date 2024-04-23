namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resenas", "IdUser", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Nombre", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.AspNetUsers", "Apellido", c => c.String(maxLength: 25));
            AddColumn("dbo.AspNetUsers", "Correo", c => c.String(nullable: false, maxLength: 35));
            AddColumn("dbo.AspNetUsers", "FechaRegistro", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Resenas", "IdUser");
            AddForeignKey("dbo.Resenas", "IdUser", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resenas", "IdUser", "dbo.AspNetUsers");
            DropIndex("dbo.Resenas", new[] { "IdUser" });
            DropColumn("dbo.AspNetUsers", "FechaRegistro");
            DropColumn("dbo.AspNetUsers", "Correo");
            DropColumn("dbo.AspNetUsers", "Apellido");
            DropColumn("dbo.AspNetUsers", "Nombre");
            DropColumn("dbo.Resenas", "IdUser");
        }
    }
}
