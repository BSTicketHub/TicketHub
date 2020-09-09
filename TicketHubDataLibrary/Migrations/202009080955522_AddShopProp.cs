namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShopProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shops", "ShopIntro", c => c.String());
            AddColumn("dbo.Shops", "City", c => c.String());
            AddColumn("dbo.Shops", "District", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shops", "District");
            DropColumn("dbo.Shops", "City");
            DropColumn("dbo.Shops", "ShopIntro");
        }
    }
}
