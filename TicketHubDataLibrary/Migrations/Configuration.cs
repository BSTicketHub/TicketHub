using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

            string[] userNames = new string[] { "admin", "platform", "manager", "employee", "cust" };
            GenUsers(context, userNames);

        }

        private void GenUsers(TicketHubContext context, string[] userNames)
        {
            var userStore = new UserStore<TicketHubUser>(context);
            var userManager = new UserManager<TicketHubUser>(userStore);

            for (int i = 1; i <= 5; i++)
            {
                var name = userNames[i - 1];
                var roleName = context.Roles.FirstOrDefault(r => r.Id == i.ToString()).Name;
                for (int j = 1; j <= i * i; j++)
                {
                    GenUser(context, userManager, $"{name}{j}", roleName);
                }
            }
        }

        private void GenUser(TicketHubContext context, UserManager<TicketHubUser> userManager, string userName, string roleName)
        {
            if (!(context.Users.Any(u => u.UserName == userName)))
            {
                var userToInsert = new TicketHubUser { UserName = userName, PhoneNumber = "0987654321", Email = $"{userName}@tickethub.com" };
                userManager.Create(userToInsert, "Pwd12345.");
                userManager.AddToRole(userToInsert.Id, roleName);
            }
        }
    }
}
