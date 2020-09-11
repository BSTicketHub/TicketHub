using Microsoft.AspNet.Identity;
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

            var userStore = new UserStore<TicketHubUser>(context);
            var userManager = new UserManager<TicketHubUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            string[] userNames = new string[] { "admin", "platform", "manager", "employee", "cust" };
            GenUsers(userManager, roleManager, userNames);

            GenShop(context, userManager);
        }

        private void GenUsers(UserManager<TicketHubUser> userManager, RoleManager<IdentityRole> roleManager, string[] userNames)
        {
            for (int i = 1; i <= 5; i++)
            {
                var name = userNames[i - 1];
                var roleName = roleManager.FindById(i.ToString()).Name;
                for (int j = 1; j <= i * i; j++)
                {
                    GenUser(userManager, $"{name}{j}", roleName);
                }
            }
        }

        private void GenUser(UserManager<TicketHubUser> userManager, string userName, string roleName)
        {
            var userEmail = $"{userName}@tickethub.com";
            if (!userManager.Users.Any(u => u.UserName == userEmail))
            {
                var userToInsert = new TicketHubUser { UserName = userEmail, PhoneNumber = "0987654321", Email = userEmail };
                userManager.Create(userToInsert, "Pwd12345.");
                userManager.AddToRole(userToInsert.Id, roleName);
            }
            else
            {
                var user = userManager.FindByEmail($"{userName}@tickethub.com");
                if (!userManager.IsInRole(user.Id, roleName))
                {
                    userManager.AddToRole(user.Id, roleName);
                }
            }
        }

        private void GenShop(TicketHubContext context, UserManager<TicketHubUser> userManager)
        {
            List<Issue> issues = new List<Issue>();
            for (int i = 1; i <= 9; i++)
            {
                var shop = new Shop
                {
                    ShopName = $"ExampleShop{i}",
                    ShopIntro = $"This is introduction of ExampleShop{i}.",
                    Phone = "02-345-6789",
                    Fax = "02-987-6543",
                    City = "台北市",
                    District = "大安區",
                    Address = "忠孝東路三段96號11號樓之1",
                    Zip = "106",
                    Email = $"BuildSchool_{i}@tickethub.com",
                    Website = "https://www.build-school.com/",
                    AppliedDate = DateTime.Now,
                };

                var shopManager = userManager.FindByEmail($"manager{i}@tickethub.com");
                for (int j = 1; j <= 5; j++)
                {
                    Issue issue = new Issue
                    {
                        Title = $"ExampleShop{i} Issue{j}",
                        Memo = $"This is memo of ExampleShop{i} Issue{j}",
                        OriginalPrice = i * j * 131,
                        DiscountRatio = 0.85m,
                        DiscountPrice = i * j * 131 * 0.85m,
                        Amount = 60 * j,
                        IssuedDate = DateTime.Now,
                        ReleasedDate = DateTime.Now,
                        User = shopManager,
                        Shop = shop,
                    };
                    issues.Add(issue);
                }
            }
            context.Issue.AddOrUpdate(i => i.Title, issues.ToArray());
        }
    }
}
