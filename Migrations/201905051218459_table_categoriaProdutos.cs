namespace WebAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table_categoriaProdutos : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CategoriaProdutoes", newName: "CategoriaProdutos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CategoriaProdutos", newName: "CategoriaProdutoes");
        }
    }
}
