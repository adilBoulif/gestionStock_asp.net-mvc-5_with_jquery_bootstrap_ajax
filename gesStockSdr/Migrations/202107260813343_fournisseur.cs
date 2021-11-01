namespace gesStockSdr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fournisseur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fournisseurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nom = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fournisseurs");
        }
    }
}
