namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IssueTags",
                c => new
                    {
                        IssueId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IssueId, t.TagId })
                .ForeignKey("dbo.Issues", t => t.IssueId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.IssueId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShopTags",
                c => new
                    {
                        ShopId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShopId, t.TagId })
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ShopId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShopTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.ShopTags", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.IssueTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.IssueTags", "IssueId", "dbo.Issues");
            DropIndex("dbo.ShopTags", new[] { "TagId" });
            DropIndex("dbo.ShopTags", new[] { "ShopId" });
            DropIndex("dbo.IssueTags", new[] { "TagId" });
            DropIndex("dbo.IssueTags", new[] { "IssueId" });
            DropTable("dbo.ShopTags");
            DropTable("dbo.Tags");
            DropTable("dbo.IssueTags");
        }
    }
}
