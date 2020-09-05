namespace TicketHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberViewModels", "Account", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberViewModels", "Account", c => c.Int(nullable: false));
        }
    }
}
