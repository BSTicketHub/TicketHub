namespace TicketHubDataLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterShopDatetimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shops", "ModifiedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Shops", "ValidatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shops", "ValidatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Shops", "ModifiedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
