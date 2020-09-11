namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShopZipAlterColumnToDatetime2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shops", "Zip", c => c.String());
            AlterColumn("dbo.ActionLogs", "LogedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Issues", "IssuedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Issues", "ReleasedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Issues", "ClosedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Shops", "AppliedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Shops", "ModifiedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Shops", "ValidatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.LoginLogs", "LogedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Orders", "OrderedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Refunds", "AppliedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.RefundDetails", "RefundedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tickets", "DeliveredDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tickets", "ExchangedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tickets", "VoidedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.UserFavoriteShops", "AddedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.UserWishIssues", "AddedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserWishIssues", "AddedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserFavoriteShops", "AddedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tickets", "VoidedDate", c => c.DateTime());
            AlterColumn("dbo.Tickets", "ExchangedDate", c => c.DateTime());
            AlterColumn("dbo.Tickets", "DeliveredDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RefundDetails", "RefundedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Refunds", "AppliedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "OrderedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LoginLogs", "LogedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Shops", "ValidatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Shops", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Shops", "AppliedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Issues", "ClosedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Issues", "ReleasedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Issues", "IssuedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.ActionLogs", "LogedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Shops", "Zip");
        }
    }
}
