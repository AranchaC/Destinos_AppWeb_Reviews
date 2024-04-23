namespace Destinos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifUsers_correo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Correo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Correo", c => c.String(nullable: false, maxLength: 35));
        }
    }
}
