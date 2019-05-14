namespace WebAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table_produtos : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ImagemProdutoes", newName: "ImagemProdutos");
            RenameTable(name: "dbo.Produtoes", newName: "Produtos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Produtos", newName: "Produtoes");
            RenameTable(name: "dbo.ImagemProdutos", newName: "ImagemProdutoes");
        }
    }
}
