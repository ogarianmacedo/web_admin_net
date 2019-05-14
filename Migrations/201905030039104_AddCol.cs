namespace WebAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCol : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "Imagem", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "Imagem", c => c.Binary());
        }
    }
}
