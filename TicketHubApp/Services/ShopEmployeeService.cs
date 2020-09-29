using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;
using Microsoft.AspNet.Identity.Owin;
using TicketHubDataLibrary;
using TicketHubApp.Interfaces;

namespace TicketHubApp.Services
{
    public class ShopEmployeeService
    {
        private TicketHubContext _context = new TicketHubContext();

        private readonly string _userid = HttpContext.Current.User.Identity.GetUserId();

        public List<ShopEmployeeViewModel> GetEmployeeList()
        {
            var result = new List<ShopEmployeeViewModel>();
            TicketHubContext context = new TicketHubContext();

            var shopid = context.ShopEmployee.Where((x) => x.UserId == _userid).FirstOrDefault().ShopId;

            var temp = (from u in context.Users
                        from ur in u.Roles
                        join r in context.Roles on ur.RoleId equals r.Id
                        join e in context.ShopEmployee on u.Id equals e.UserId
                        where e.ShopId == shopid
                        select new
                        {
                            UserId = e.UserId,
                            UserName = u.UserName,
                            Email = u.Email,
                            Gender = u.Sex,
                            Phone = u.PhoneNumber,
                            RoleId = r.Id
                        }).ToList();

            var group = from t in temp
                        group t by new { t.UserId, t.UserName, t.Email, t.Gender, t.Phone } into g
                        select new ShopEmployeeViewModel
                        {
                            UserId = g.Key.UserId,
                            UserName = g.Key.UserName,
                            Email = g.Key.Email,
                            Gender = g.Key.Gender,
                            Phone = g.Key.Phone,
                            RoleName = (g.Min(m => int.Parse(m.RoleId)) == 3) ? "經理" : "員工"
                        };

            result = group.ToList();
            return result;
        }

        public OperationResult DeleteEmployee(string id, AppIdentityUserManager userManager)
        {
            var result = new OperationResult();
            try
            {
                TicketHubContext context = new TicketHubContext();

                var emp = context.ShopEmployee.Where((s) => s.UserId == id).FirstOrDefault();
                context.ShopEmployee.Remove(emp);
                context.SaveChanges();

                var user = userManager.FindById(id);
                var roleId = user.Roles.Select(x => x.RoleId).ToList();

                foreach (var item in roleId)
                {
                    if (item == "3" || item == "4")
                    {
                        userManager.RemoveFromRole(id, item);
                    }
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
            }
            return result;
        }

        public OperationResult createEmployee(string account, AppIdentityUserManager userManager)
        {
            var result = new OperationResult();
            try
            {
                var context = new TicketHubContext();
                var employeeRepo = new GenericRepository<ShopEmployee>(context);

                var shopid = context.ShopEmployee.Where((x) => x.UserId == _userid).FirstOrDefault().ShopId;
                var userid = userManager.FindByEmail(account).Id;
                var entity = new ShopEmployee() { ShopId = shopid, UserId = userid };
                employeeRepo.Create(entity);
                context.SaveChanges();

                userManager.AddToRole(userid, RoleName.SHOP_EMPLOYEE);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.ToString();
            }
            return result;
        }

        public IResult CreateEmployeeWithRole(string account, AppIdentityUserManager userManager, string role = RoleName.SHOP_EMPLOYEE)
        {
            var result = new OperationResult();
            try
            {
                var repository = new GenericRepository<ShopEmployee>(_context);
                var shopId = repository.GetAll().Where(x => x.UserId == _userid).FirstOrDefault().ShopId;
                var userId = userManager.FindByEmail(account).Id;
                ShopEmployee shopEmployee = new ShopEmployee
                {
                    UserId = userId,
                    ShopId = shopId
                };
                repository.Create(shopEmployee);
                repository.SaveChanges();
                userManager.AddToRole(userId, role);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.ToString();
            }
            return result;
        }
    }
}