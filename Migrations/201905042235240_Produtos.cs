namespace WebAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Produtos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaProdutos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImagemProdutos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imagem = c.String(),
                        StPrincipal = c.Boolean(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produtos", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        ValorCursto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorPromocao = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataEntrada = c.DateTime(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        StPromocao = c.Boolean(nullable: false),
                        CategoriaProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoriaProdutos", t => t.CategoriaProdutoId, cascadeDelete: true)
                .Index(t => t.CategoriaProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImagemProdutos", "ProdutoId", "dbo.Produtos");
            DropForeignKey("dbo.Produtos", "CategoriaProdutoId", "dbo.CategoriaProdutos");
            DropIndex("dbo.Produtos", new[] { "CategoriaProdutoId" });
            DropIndex("dbo.ImagemProdutos", new[] { "ProdutoId" });
            DropTable("dbo.Produtos");
            DropTable("dbo.ImagemProdutos");
            DropTable("dbo.CategoriaProdutos");
        }
    }
}
