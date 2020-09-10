namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketExchangeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "Exchanged", c => c.Boolean());
            AlterColumn("dbo.Tickets", "ExchangedDate", c => c.DateTime());
            AlterColumn("dbo.Tickets", "Voided", c => c.Boolean());
            AlterColumn("dbo.Tickets", "VoidedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "VoidedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tickets", "Voided", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Tickets", "ExchangedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tickets", "Exchanged", c => c.Boolean(nullable: false));
        }
    }
}
