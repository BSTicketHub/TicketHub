namespace TicketHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserWishIssueTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserWishIssues",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        IssueId = c.Guid(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.IssueId })
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.IssueId);
            
            AddColumn("dbo.ActionLogs", "LogedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LoginLogs", "LogedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Issues", "IssuedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Issues", "ReleasedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shops", "AppliedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shops", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shops", "ValidatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ActionLogs", "LogDate");
            DropColumn("dbo.LoginLogs", "LogDate");
            DropColumn("dbo.Issues", "IssueDate");
            DropColumn("dbo.Issues", "ReleaseDate");
            DropColumn("dbo.Shops", "ApplyDate");
            DropColumn("dbo.Shops", "ModifyDate");
            DropColumn("dbo.Shops", "ValidDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shops", "ValidDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shops", "ModifyDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shops", "ApplyDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Issues", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Issues", "IssueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LoginLogs", "LogDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ActionLogs", "LogDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.UserWishIssues", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserWishIssues", "IssueId", "dbo.Issues");
            DropIndex("dbo.UserWishIssues", new[] { "IssueId" });
            DropIndex("dbo.UserWishIssues", new[] { "UserId" });
            DropColumn("dbo.Shops", "ValidatedDate");
            DropColumn("dbo.Shops", "ModifiedDate");
            DropColumn("dbo.Shops", "AppliedDate");
            DropColumn("dbo.Issues", "ReleasedDate");
            DropColumn("dbo.Issues", "IssuedDate");
            DropColumn("dbo.LoginLogs", "LogedDate");
            DropColumn("dbo.ActionLogs", "LogedDate");
            DropTable("dbo.UserWishIssues");
        }
    }
}
