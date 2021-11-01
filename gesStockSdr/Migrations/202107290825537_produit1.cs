namespace gesStockSdr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produit1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produits", "category_Id", "dbo.Categories");
            DropIndex("dbo.Produits", new[] { "category_Id" });
            RenameColumn(table: "dbo.Produits", name: "category_Id", newName: "categoryId");
            AlterColumn("dbo.Produits", "categoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Produits", "categoryId");
            AddForeignKey("dbo.Produits", "categoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produits", "categoryId", "dbo.Categories");
            DropIndex("dbo.Produits", new[] { "categoryId" });
            AlterColumn("dbo.Produits", "categoryId", c => c.Int());
            RenameColumn(table: "dbo.Produits", name: "categoryId", newName: "category_Id");
            CreateIndex("dbo.Produits", "category_Id");
            AddForeignKey("dbo.Produits", "category_Id", "dbo.Categories", "Id");
        }
    }
}
