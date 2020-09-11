namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryTableIssueImgPath : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IssueCategories",
                c => new
                    {
                        IssueId = c.Guid(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IssueId, t.CategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Issues", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.IssueId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ShopCategories",
                c => new
                    {
                        ShopId = c.Guid(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShopId, t.CategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .Index(t => t.ShopId)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.Issues", "ImgPath", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShopCategories", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.ShopCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.IssueCategories", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.IssueCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ShopCategories", new[] { "CategoryId" });
            DropIndex("dbo.ShopCategories", new[] { "ShopId" });
            DropIndex("dbo.IssueCategories", new[] { "CategoryId" });
            DropIndex("dbo.IssueCategories", new[] { "IssueId" });
            DropColumn("dbo.Issues", "ImgPath");
            DropTable("dbo.ShopCategories");
            DropTable("dbo.IssueCategories");
            DropTable("dbo.Categories");
        }
    }
}
