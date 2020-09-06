using System;
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
            context.Role.AddOrUpdate(r => r.Id,
                new Role { Id = Guid.Parse("8962DF55-017B-4A47-B78A-7261D81532DC"), Name = "Customer" },
                new Role { Id = Guid.Parse("13832714-14D8-42FD-A1E8-8B7A6729C245"), Name = "ShopOwner" },
                new Role { Id = Guid.Parse("26356D75-867A-4733-9FF8-6F1D39C3D4E6"), Name = "ShopEmployee" },
                new Role { Id = Guid.Parse("BDA7B8AF-3017-4963-97AB-625A017D5B20"), Name = "PlatformAdmin" },
                new Role { Id = Guid.Parse("CAABE219-0858-45EB-B921-28B79126D358"), Name = "PlatformEmployee" }
                );
        }
    }
}
