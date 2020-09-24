namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLatLngColumnsOnShop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shops", "Lat", c => c.String());
            AddColumn("dbo.Shops", "Lng", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shops", "Lng");
            DropColumn("dbo.Shops", "Lat");
        }
    }
}
