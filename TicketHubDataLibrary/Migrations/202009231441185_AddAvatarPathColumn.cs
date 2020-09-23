namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvatarPathColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AvatarPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AvatarPath");
        }
    }
}
