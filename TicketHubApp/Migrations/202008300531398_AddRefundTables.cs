namespace TicketHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRefundTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Refunds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AppliedDate = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Refunds", "UserId", "dbo.Users");
            DropForeignKey("dbo.RefundDetails", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.RefundDetails", "RefundId", "dbo.Refunds");
            DropIndex("dbo.RefundDetails", new[] { "TicketId" });
            DropIndex("dbo.RefundDetails", new[] { "RefundId" });
            DropIndex("dbo.Refunds", new[] { "UserId" });
            DropTable("dbo.RefundDetails");
            DropTable("dbo.Refunds");
        }
    }
}
