namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSaltColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordSalt", c => c.String());
            AddColumn("dbo.Users", "PasswordWorkFactor", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PasswordWorkFactor");
            DropColumn("dbo.Users", "PasswordSalt");
        }
    }
}
