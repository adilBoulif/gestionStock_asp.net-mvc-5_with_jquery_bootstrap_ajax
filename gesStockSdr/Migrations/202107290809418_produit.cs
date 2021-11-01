namespace gesStockSdr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nom = c.String(nullable: false),
                        num_article = c.Int(nullable: false),
                        quantite = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        category_Id = c.Int(),
                        fournisseur_Id = c.Int(),
                        provenances_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.category_Id)
                .ForeignKey("dbo.Fournisseurs", t => t.fournisseur_Id)
                .ForeignKey("dbo.Provenances", t => t.provenances_Id)
                .Index(t => t.category_Id)
                .Index(t => t.fournisseur_Id)
                .Index(t => t.provenances_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produits", "provenances_Id", "dbo.Provenances");
            DropForeignKey("dbo.Produits", "fournisseur_Id", "dbo.Fournisseurs");
            DropForeignKey("dbo.Produits", "category_Id", "dbo.Categories");
            DropIndex("dbo.Produits", new[] { "provenances_Id" });
            DropIndex("dbo.Produits", new[] { "fournisseur_Id" });
            DropIndex("dbo.Produits", new[] { "category_Id" });
            DropTable("dbo.Produits");
        }
    }
}
