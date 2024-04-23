namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_dos : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Destinos", new[] { "Nombre" });
            CreateTable(
                "dbo.Fotos",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IdResena = c.Guid(nullable: false),
                        UrlFoto = c.String(nullable: false, maxLength: 15),
                        Descripción = c.String(maxLength: 15),
                        FechaSubida = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resenas", t => t.IdResena, cascadeDelete: true)
                .Index(t => t.IdResena);
            
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.NickName, unique: true)
                .Index(t => t.Nombre, unique: true);
            
            AddColumn("dbo.Resenas", "FechaResena", c => c.DateTime(nullable: false));
            AddColumn("dbo.Resenas", "Comentario", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Destinos", "Nombre", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Destinos", "Ciudad", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Destinos", "Pais", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Resenas", "Titulo", c => c.String(maxLength: 15));
            CreateIndex("dbo.Destinos", "Nombre", unique: true);
            CreateIndex("dbo.Resenas", "IdUsuario");
            AddForeignKey("dbo.Resenas", "IdUsuario", "dbo.Usuarios", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resenas", "IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Fotos", "IdResena", "dbo.Resenas");
            DropIndex("dbo.Usuarios", new[] { "Nombre" });
            DropIndex("dbo.Usuarios", new[] { "NickName" });
            DropIndex("dbo.Fotos", new[] { "IdResena" });
            DropIndex("dbo.Resenas", new[] { "IdUsuario" });
            DropIndex("dbo.Destinos", new[] { "Nombre" });
            AlterColumn("dbo.Resenas", "Titulo", c => c.String());
            AlterColumn("dbo.Destinos", "Pais", c => c.String(maxLength: 15));
            AlterColumn("dbo.Destinos", "Ciudad", c => c.String(maxLength: 15));
            AlterColumn("dbo.Destinos", "Nombre", c => c.String(maxLength: 15));
            DropColumn("dbo.Resenas", "Comentario");
            DropColumn("dbo.Resenas", "FechaResena");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Fotos");
            CreateIndex("dbo.Destinos", "Nombre", unique: true);
        }
    }
}
