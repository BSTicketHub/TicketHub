using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
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
            context.Roles.AddOrUpdate(
                new IdentityRole { Id = "1", Name = RoleName.ADMINISTRATOR },
                new IdentityRole { Id = "2", Name = RoleName.PLATFORM_ADMIN },
                new IdentityRole { Id = "3", Name = RoleName.SHOP_MANAGER },
                new IdentityRole { Id = "4", Name = RoleName.SHOP_EMPLOYEE },
                new IdentityRole { Id = "5", Name = RoleName.CUSTOMER }
            );
        }
    }
}
