namespace WebAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelasIniciais : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoUsuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        TipoUsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoUsuarios", t => t.TipoUsuarioId, cascadeDelete: true)
                .Index(t => t.TipoUsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "TipoUsuarioId", "dbo.TipoUsuarios");
            DropIndex("dbo.Usuarios", new[] { "TipoUsuarioId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.TipoUsuarios");
        }
    }
}
