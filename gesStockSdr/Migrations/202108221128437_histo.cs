namespace gesStockSdr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class histo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Historiques",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nom = c.String(nullable: false),
                        num_article = c.Int(nullable: false),
                        quantite = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        categoryId = c.Int(nullable: false),
                        provenancesId = c.Int(nullable: false),
                        fournisseurId = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.categoryId, cascadeDelete: true)
                .ForeignKey("dbo.Fournisseurs", t => t.fournisseurId, cascadeDelete: true)
                .ForeignKey("dbo.Provenances", t => t.provenancesId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.categoryId)
                .Index(t => t.provenancesId)
                .Index(t => t.fournisseurId)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Historiques", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Historiques", "provenancesId", "dbo.Provenances");
            DropForeignKey("dbo.Historiques", "fournisseurId", "dbo.Fournisseurs");
            DropForeignKey("dbo.Historiques", "categoryId", "dbo.Categories");
            DropIndex("dbo.Historiques", new[] { "user_Id" });
            DropIndex("dbo.Historiques", new[] { "fournisseurId" });
            DropIndex("dbo.Historiques", new[] { "provenancesId" });
            DropIndex("dbo.Historiques", new[] { "categoryId" });
            DropTable("dbo.Historiques");
        }
    }
}
