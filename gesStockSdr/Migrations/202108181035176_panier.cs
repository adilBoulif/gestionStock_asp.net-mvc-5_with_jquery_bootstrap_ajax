namespace gesStockSdr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class panier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Paniers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        quantite = c.Int(nullable: false),
                        ProduitsId = c.Int(nullable: false),
                        CommandeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commandes", t => t.CommandeId, cascadeDelete: true)
                .ForeignKey("dbo.Produits", t => t.ProduitsId, cascadeDelete: true)
                .Index(t => t.ProduitsId)
                .Index(t => t.CommandeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paniers", "ProduitsId", "dbo.Produits");
            DropForeignKey("dbo.Paniers", "CommandeId", "dbo.Commandes");
            DropIndex("dbo.Paniers", new[] { "CommandeId" });
            DropIndex("dbo.Paniers", new[] { "ProduitsId" });
            DropTable("dbo.Paniers");
        }
    }
}
