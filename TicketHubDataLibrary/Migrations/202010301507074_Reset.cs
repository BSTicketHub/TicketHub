namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reset : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IssueCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.IssueCategories", "IssueId", "dbo.Issues");
            DropIndex("dbo.IssueCategories", new[] { "IssueId" });
            DropIndex("dbo.IssueCategories", new[] { "CategoryId" });
            AddColumn("dbo.Users", "AvatarPath", c => c.String());
            AddColumn("dbo.Users", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "DeletedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Issues", "Category", c => c.String());
            AddColumn("dbo.Shops", "Lat", c => c.String());
            AddColumn("dbo.Shops", "Lng", c => c.String());
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
            
            DropColumn("dbo.Shops", "Lng");
            DropColumn("dbo.Shops", "Lat");
            DropColumn("dbo.Issues", "Category");
            DropColumn("dbo.Users", "DeletedDate");
            DropColumn("dbo.Users", "Deleted");
            DropColumn("dbo.Users", "AvatarPath");
            CreateIndex("dbo.IssueCategories", "CategoryId");
            CreateIndex("dbo.IssueCategories", "IssueId");
            AddForeignKey("dbo.IssueCategories", "IssueId", "dbo.Issues", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IssueCategories", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
