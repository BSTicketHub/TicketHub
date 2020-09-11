namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSexColumnToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Sex", c => c.String());
            DropColumn("dbo.Users", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Address", c => c.String());
            DropColumn("dbo.Users", "Sex");
        }
    }
}
