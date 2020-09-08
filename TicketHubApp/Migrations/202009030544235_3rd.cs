namespace TicketHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3rd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberViewModels", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberViewModels", "UserName", c => c.String(nullable: false));
        }
    }
}
