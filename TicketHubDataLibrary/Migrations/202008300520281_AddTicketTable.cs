namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeliveredDate = c.DateTime(nullable: false),
                        Exchanged = c.Boolean(nullable: false),
                        ExchangedDate = c.DateTime(nullable: false),
                        Voided = c.Boolean(nullable: false),
                        VoidedDate = c.DateTime(nullable: false),
                        IssueId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.IssueId)
                .Index(t => t.UserId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Tickets", "IssueId", "dbo.Issues");
            DropIndex("dbo.Tickets", new[] { "OrderId" });
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Tickets", new[] { "IssueId" });
            DropTable("dbo.Tickets");
        }
    }
}
