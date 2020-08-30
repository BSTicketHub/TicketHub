namespace TicketHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShopTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ShopName = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        BannerImg = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        ValidDate = c.DateTime(nullable: false),
                        ReviewerId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.ReviewerId)
                .Index(t => t.ReviewerId);
            
            CreateTable(
                "dbo.ShopEmployees",
                c => new
                    {
                        ShopId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShopId, t.UserId })
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ShopId)
                .Index(t => t.UserId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .Index(t => t.ShopId);
            
            CreateTable(
                "dbo.UserFavoriteShops",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ShopId = c.Guid(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ShopId })
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ShopId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFavoriteShops", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFavoriteShops", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.ShopLogs", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.ShopEmployees", "UserId", "dbo.Users");
            DropForeignKey("dbo.ShopEmployees", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.Shops", "ReviewerId", "dbo.Admins");
            DropIndex("dbo.UserFavoriteShops", new[] { "ShopId" });
            DropIndex("dbo.UserFavoriteShops", new[] { "UserId" });
            DropIndex("dbo.ShopLogs", new[] { "ShopId" });
            DropIndex("dbo.ShopEmployees", new[] { "UserId" });
            DropIndex("dbo.ShopEmployees", new[] { "ShopId" });
            DropIndex("dbo.Shops", new[] { "ReviewerId" });
            DropTable("dbo.UserFavoriteShops");
            DropTable("dbo.ShopLogs");
            DropTable("dbo.ShopEmployees");
            DropTable("dbo.Shops");
        }
    }
}
