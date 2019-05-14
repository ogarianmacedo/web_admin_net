namespace WebAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_produto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produtos", "ValorCusto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Produtos", "ValorCursto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produtos", "ValorCursto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Produtos", "ValorCusto");
        }
    }
}
