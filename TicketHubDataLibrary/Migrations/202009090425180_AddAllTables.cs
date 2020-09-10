namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAllTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Action = c.String(),
                        Message = c.String(),
                        LogedDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        Memo = c.String(),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IssuedDate = c.DateTime(nullable: false),
                        ReleasedDate = c.DateTime(nullable: false),
                        ClosedDate = c.DateTime(nullable: false),
                        IssuerId = c.String(maxLength: 128),
                        ShopId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IssuerId)
                .Index(t => t.IssuerId)
                .Index(t => t.ShopId);
            
            CreateTable(
                "dbo.IssueTags",
                c => new
                    {
                        IssueId = c.Guid(nullable: false),
                        TagId = c.Int(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ShopName = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        BannerImg = c.String(),
                        AppliedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ValidatedDate = c.DateTime(nullable: false),
                        ReviewerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ReviewerId)
                .Index(t => t.ReviewerId);
            
            CreateTable(
                "dbo.ShopEmployees",
                c => new
                    {
                        ShopId = c.Guid(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ShopId, t.UserId })
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ShopId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ShopTags",
                c => new
                    {
                        ShopId = c.Guid(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShopId, t.TagId })
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ShopId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.LoginLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        LogedDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        OrderedDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        IssueId = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.OrderId, t.IssueId })
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId)
                .Index(t => t.IssueId);
            
            CreateTable(
                "dbo.Refunds",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AppliedDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RefundDetails",
                c => new
                    {
                        RefundId = c.Guid(nullable: false),
                        TicketId = c.Guid(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Refunded = c.Boolean(nullable: false),
                        RefundedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.RefundId, t.TicketId })
                .ForeignKey("dbo.Refunds", t => t.RefundId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.RefundId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DeliveredDate = c.DateTime(nullable: false),
                        Exchanged = c.Boolean(nullable: false),
                        ExchangedDate = c.DateTime(nullable: false),
                        Voided = c.Boolean(nullable: false),
                        VoidedDate = c.DateTime(nullable: false),
                        IssueId = c.Guid(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.IssueId)
                .Index(t => t.UserId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.UserFavoriteShops",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ShopId = c.Guid(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ShopId })
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ShopId);
            
            CreateTable(
                "dbo.UserWishIssues",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        IssueId = c.Guid(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.IssueId })
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.IssueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserWishIssues", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserWishIssues", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.UserFavoriteShops", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFavoriteShops", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.Refunds", "UserId", "dbo.Users");
            DropForeignKey("dbo.RefundDetails", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Tickets", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.RefundDetails", "RefundId", "dbo.Refunds");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.LoginLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Issues", "IssuerId", "dbo.Users");
            DropForeignKey("dbo.Issues", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.Shops", "ReviewerId", "dbo.Users");
            DropForeignKey("dbo.ShopTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.ShopTags", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.ShopEmployees", "UserId", "dbo.Users");
            DropForeignKey("dbo.ShopEmployees", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.IssueTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.IssueTags", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.ActionLogs", "UserId", "dbo.Users");
            DropIndex("dbo.UserWishIssues", new[] { "IssueId" });
            DropIndex("dbo.UserWishIssues", new[] { "UserId" });
            DropIndex("dbo.UserFavoriteShops", new[] { "ShopId" });
            DropIndex("dbo.UserFavoriteShops", new[] { "UserId" });
            DropIndex("dbo.Tickets", new[] { "OrderId" });
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Tickets", new[] { "IssueId" });
            DropIndex("dbo.RefundDetails", new[] { "TicketId" });
            DropIndex("dbo.RefundDetails", new[] { "RefundId" });
            DropIndex("dbo.Refunds", new[] { "UserId" });
            DropIndex("dbo.OrderDetails", new[] { "IssueId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.LoginLogs", new[] { "UserId" });
            DropIndex("dbo.ShopTags", new[] { "TagId" });
            DropIndex("dbo.ShopTags", new[] { "ShopId" });
            DropIndex("dbo.ShopEmployees", new[] { "UserId" });
            DropIndex("dbo.ShopEmployees", new[] { "ShopId" });
            DropIndex("dbo.Shops", new[] { "ReviewerId" });
            DropIndex("dbo.IssueTags", new[] { "TagId" });
            DropIndex("dbo.IssueTags", new[] { "IssueId" });
            DropIndex("dbo.Issues", new[] { "ShopId" });
            DropIndex("dbo.Issues", new[] { "IssuerId" });
            DropIndex("dbo.ActionLogs", new[] { "UserId" });
            DropTable("dbo.UserWishIssues");
            DropTable("dbo.UserFavoriteShops");
            DropTable("dbo.Tickets");
            DropTable("dbo.RefundDetails");
            DropTable("dbo.Refunds");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.LoginLogs");
            DropTable("dbo.ShopTags");
            DropTable("dbo.ShopEmployees");
            DropTable("dbo.Shops");
            DropTable("dbo.Tags");
            DropTable("dbo.IssueTags");
            DropTable("dbo.Issues");
            DropTable("dbo.ActionLogs");
        }
    }
}
