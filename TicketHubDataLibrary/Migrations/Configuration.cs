using System.Data.Entity.Migrations;
using TicketHubDataLibrary.Models;

namespace TicketHubDataLibrary.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TicketHubContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TicketHubContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
