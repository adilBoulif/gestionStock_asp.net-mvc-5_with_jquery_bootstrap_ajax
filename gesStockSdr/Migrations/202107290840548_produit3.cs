namespace gesStockSdr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produit3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produits", "fournisseur_Id", "dbo.Fournisseurs");
            DropIndex("dbo.Produits", new[] { "fournisseur_Id" });
            RenameColumn(table: "dbo.Produits", name: "fournisseur_Id", newName: "fournisseurId");
            AddColumn("dbo.Produits", "provenanceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Produits", "fournisseurId", c => c.Int(nullable: false));
            CreateIndex("dbo.Produits", "fournisseurId");
            AddForeignKey("dbo.Produits", "fournisseurId", "dbo.Fournisseurs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produits", "fournisseurId", "dbo.Fournisseurs");
            DropIndex("dbo.Produits", new[] { "fournisseurId" });
            AlterColumn("dbo.Produits", "fournisseurId", c => c.Int());
            DropColumn("dbo.Produits", "provenanceId");
            RenameColumn(table: "dbo.Produits", name: "fournisseurId", newName: "fournisseur_Id");
            CreateIndex("dbo.Produits", "fournisseur_Id");
            AddForeignKey("dbo.Produits", "fournisseur_Id", "dbo.Fournisseurs", "Id");
        }
    }
}
