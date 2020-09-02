namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActionLogRemoveShopLog : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShopLogs", "ShopId", "dbo.Shops");
            DropIndex("dbo.ShopLogs", new[] { "ShopId" });
            CreateTable(
                "dbo.ActionLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Action = c.String(),
                        Message = c.String(),
                        LogDate = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Memo = c.String(),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IssueDate = c.DateTime(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        ClosedDate = c.DateTime(nullable: false),
                        IssuerId = c.Guid(nullable: false),
                        ShopId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IssuerId, cascadeDelete: true)
                .Index(t => t.IssuerId)
                .Index(t => t.ShopId);
            
            DropTable("dbo.ShopLogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ShopLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Action = c.String(),
                        Message = c.String(),
                        LogDate = c.DateTime(nullable: false),
                        ShopId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Issues", "IssuerId", "dbo.Users");
            DropForeignKey("dbo.Issues", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.ActionLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActionLogs", "UserId", "dbo.Admins");
            DropIndex("dbo.Issues", new[] { "ShopId" });
            DropIndex("dbo.Issues", new[] { "IssuerId" });
            DropIndex("dbo.ActionLogs", new[] { "UserId" });
            DropTable("dbo.Issues");
            DropTable("dbo.ActionLogs");
            CreateIndex("dbo.ShopLogs", "ShopId");
            AddForeignKey("dbo.ShopLogs", "ShopId", "dbo.Shops", "Id", cascadeDelete: true);
        }
    }
}
