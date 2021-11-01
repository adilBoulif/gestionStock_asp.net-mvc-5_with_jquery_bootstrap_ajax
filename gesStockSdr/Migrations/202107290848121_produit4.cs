namespace gesStockSdr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produit4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produits", "provenances_Id", "dbo.Provenances");
            DropIndex("dbo.Produits", new[] { "provenances_Id" });
            RenameColumn(table: "dbo.Produits", name: "provenances_Id", newName: "provenancesId");
            AlterColumn("dbo.Produits", "provenancesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Produits", "provenancesId");
            AddForeignKey("dbo.Produits", "provenancesId", "dbo.Provenances", "Id", cascadeDelete: true);
            DropColumn("dbo.Produits", "provenanceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produits", "provenanceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Produits", "provenancesId", "dbo.Provenances");
            DropIndex("dbo.Produits", new[] { "provenancesId" });
            AlterColumn("dbo.Produits", "provenancesId", c => c.Int());
            RenameColumn(table: "dbo.Produits", name: "provenancesId", newName: "provenances_Id");
            CreateIndex("dbo.Produits", "provenances_Id");
            AddForeignKey("dbo.Produits", "provenances_Id", "dbo.Provenances", "Id");
        }
    }
}
