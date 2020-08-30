namespace TicketHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterLoginLogTableReference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoginLogs", "Id", "dbo.Admins");
            DropForeignKey("dbo.LoginLogs", "Id", "dbo.Users");
            DropIndex("dbo.LoginLogs", new[] { "Id" });
            AddColumn("dbo.LoginLogs", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.LoginLogs", "UserId");
            AddForeignKey("dbo.LoginLogs", "UserId", "dbo.Admins", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoginLogs", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.LoginLogs", "UserId", "dbo.Admins");
            DropIndex("dbo.LoginLogs", new[] { "UserId" });
            DropColumn("dbo.LoginLogs", "UserId");
            CreateIndex("dbo.LoginLogs", "Id");
            AddForeignKey("dbo.LoginLogs", "Id", "dbo.Users", "Id");
            AddForeignKey("dbo.LoginLogs", "Id", "dbo.Admins", "Id");
        }
    }
}
