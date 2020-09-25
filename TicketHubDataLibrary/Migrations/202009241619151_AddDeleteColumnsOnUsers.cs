namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeleteColumnsOnUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "DeletedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DeletedDate");
            DropColumn("dbo.Users", "Deleted");
        }
    }
}
