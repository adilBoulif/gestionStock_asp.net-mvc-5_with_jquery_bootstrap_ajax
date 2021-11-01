namespace gesStockSdr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commandes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        status = c.Boolean(nullable: false),
                        serviceId = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.serviceId, cascadeDelete: true)
                .Index(t => t.serviceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Commandes", "serviceId", "dbo.Services");
            DropIndex("dbo.Commandes", new[] { "serviceId" });
            DropTable("dbo.Commandes");
        }
    }
}
