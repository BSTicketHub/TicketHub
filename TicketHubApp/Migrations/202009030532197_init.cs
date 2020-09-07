namespace TicketHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberViewModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false),
                        Mobile = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShopViewModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ShopName = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShopViewModels");
            DropTable("dbo.MemberViewModels");
        }
    }
}
