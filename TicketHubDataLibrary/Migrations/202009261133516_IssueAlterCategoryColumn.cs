namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IssueAlterCategoryColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IssueCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.IssueCategories", "IssueId", "dbo.Issues");
            DropIndex("dbo.IssueCategories", new[] { "IssueId" });
            DropIndex("dbo.IssueCategories", new[] { "CategoryId" });
            AddColumn("dbo.Issues", "Category", c => c.String());
            DropTable("dbo.IssueCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IssueCategories",
                c => new
                    {
                        IssueId = c.Guid(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IssueId, t.CategoryId });
            
            DropColumn("dbo.Issues", "Category");
            CreateIndex("dbo.IssueCategories", "CategoryId");
            CreateIndex("dbo.IssueCategories", "IssueId");
            AddForeignKey("dbo.IssueCategories", "IssueId", "dbo.Issues", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IssueCategories", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
