namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eliminoUsuarioEnResenas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Resenas", "IdUsuario", "dbo.Usuarios");
            DropIndex("dbo.Resenas", new[] { "IdUsuario" });
            RenameColumn(table: "dbo.Resenas", name: "IdUsuario", newName: "Usuario_Id");
            AlterColumn("dbo.Resenas", "Usuario_Id", c => c.Guid());
            CreateIndex("dbo.Resenas", "Usuario_Id");
            AddForeignKey("dbo.Resenas", "Usuario_Id", "dbo.Usuarios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resenas", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.Resenas", new[] { "Usuario_Id" });
            AlterColumn("dbo.Resenas", "Usuario_Id", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.Resenas", name: "Usuario_Id", newName: "IdUsuario");
            CreateIndex("dbo.Resenas", "IdUsuario");
            AddForeignKey("dbo.Resenas", "IdUsuario", "dbo.Usuarios", "Id", cascadeDelete: true);
        }
    }
}
