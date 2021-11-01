namespace gesStockSdr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class login : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Historiques", new[] { "user_Id" });
            DropColumn("dbo.Historiques", "userId");
            RenameColumn(table: "dbo.Historiques", name: "user_Id", newName: "userId");
            AlterColumn("dbo.Historiques", "userId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Historiques", "userId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Historiques", new[] { "userId" });
            AlterColumn("dbo.Historiques", "userId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Historiques", name: "userId", newName: "user_Id");
            AddColumn("dbo.Historiques", "userId", c => c.Int(nullable: false));
            CreateIndex("dbo.Historiques", "user_Id");
        }
    }
}
