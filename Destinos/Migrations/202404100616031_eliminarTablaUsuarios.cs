namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eliminarTablaUsuarios : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Resenas", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.Resenas", new[] { "Usuario_Id" });
            DropIndex("dbo.Usuarios", new[] { "NickName" });
            DropIndex("dbo.Usuarios", new[] { "Nombre" });
            DropColumn("dbo.Resenas", "Usuario_Id");
            DropTable("dbo.Usuarios");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        NickName = c.String(nullable: false, maxLength: 15),
                        Nombre = c.String(nullable: false, maxLength: 15),
                        Apellido = c.String(maxLength: 15),
                        Correo = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 15),
                        FechaRegistro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Resenas", "Usuario_Id", c => c.Guid());
            CreateIndex("dbo.Usuarios", "Nombre", unique: true);
            CreateIndex("dbo.Usuarios", "NickName", unique: true);
            CreateIndex("dbo.Resenas", "Usuario_Id");
            AddForeignKey("dbo.Resenas", "Usuario_Id", "dbo.Usuarios", "Id");
        }
    }
}
