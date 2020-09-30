using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class ShopService
    {
        private TicketHubContext _context;
        GenericRepository<Shop> _repository;
        public ShopService() : this(new TicketHubContext())
        {
        }
        public ShopService(TicketHubContext context)
        {
            _context = context;
            _repository = new GenericRepository<Shop>(_context);
        }

        public bool HasShop(string name)
        {
            return _repository.GetAll().Any(x => x.ShopName == name);
        }
        public Shop GetShop(string name)
        {
            _context.Users.Include(u => u.Roles.Select(r => r.RoleId));
            return _repository.GetAll()
                .Include(x => x.ShopEmployees.Select(se => se.User))
                .Include(x => x.ShopEmployees.Select(se => se.User.Roles))
                .Where(x => x.ShopName == name).FirstOrDefault();
        }
        public IResult CreateShop(ShopApplyViewModel viewModel)
        {
            var result = new OperationResult();
            if (HasShop(viewModel.ShopName))
            {
                result.Message = "Shop already exists";
                return result;
            }

            try
            {
                Shop newShop = new Shop
                {
                    ShopName = viewModel.ShopName,
                    ShopIntro = viewModel.ShopIntro,
                    Phone = viewModel.Phone,
                    Fax = viewModel.Fax,
                    City = viewModel.City,
                    District = viewModel.District,
                    Address = viewModel.Address,
                    Zip = viewModel.Zip,
                    Email = viewModel.Email,
                    Website = viewModel.Website,
                    AppliedDate = DateTime.Now,
                };
                _repository.Create(newShop);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public IResult AddEmployeeWithRole(string userId, string shopName, string role = RoleName.SHOP_EMPLOYEE)
        {
            var result = new OperationResult();
            try
            {
                var userStore = new UserStore<TicketHubUser>(_context);
                var userManager = new UserManager<TicketHubUser>(userStore);
                userManager.AddToRole(userId, role);
                var shop = GetShop(shopName);
                shop.ShopEmployees.Add(new ShopEmployee { Shop = shop, UserId = userId });
                _repository.Update(shop);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public IResult RemoveEmployeeWithRole(string userId, string shopName, string role = RoleName.SHOP_EMPLOYEE)
        {
            var result = new OperationResult();
            try
            {
                var userStore = new UserStore<TicketHubUser>(_context);
                var userManager = new UserManager<TicketHubUser>(userStore);
                userManager.RemoveFromRole(userId, role);
                var shop = GetShop(shopName);
                var shopEmployee = shop.ShopEmployees.First(x => x.UserId == userId);
                shop.ShopEmployees.Remove(shopEmployee);
                _repository.Update(shop);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public List<TicketHubUser> GetManagers(string shopName)
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var role = roleManager.FindByName(RoleName.SHOP_MANAGER);
            var managers = GetAllEmployees(shopName).Where(u => u.Roles.Select(r => r.RoleId).Contains(role.Id)).ToList();

            return managers;
        }
        public List<TicketHubUser> GetEmployees(string shopName)
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var role = roleManager.FindByName(RoleName.SHOP_MANAGER);
            var employees = GetAllEmployees(shopName).Where(u => !u.Roles.Select(r => r.RoleId).Contains(role.Id)).ToList();

            return employees;
        }
        public List<TicketHubUser> GetAllEmployees(string shopName)
        {
            var shop = GetShop(shopName);
            List<TicketHubUser> employees = shop != null ? shop.ShopEmployees.Select(x => x.User).ToList() : new List<TicketHubUser>();
            return employees;
        }
    }
}
