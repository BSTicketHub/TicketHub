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
using Microsoft.AspNet.Identity.EntityFramework;

namespace TicketHubApp.Services
{
    public class ShopEmployeeService
    {
        private TicketHubContext _context;
        public ShopEmployeeService() : this(new TicketHubContext()) { }
        public ShopEmployeeService(TicketHubContext context)
        {
            _context = context;
        }
        private readonly string _userid = HttpContext.Current.User.Identity.GetUserId();

        public List<ShopEmployeeViewModel> GetEmployeeList()
        {
            var result = new List<ShopEmployeeViewModel>();

            var shopid = _context.ShopEmployee.Where((x) => x.UserId == _userid).FirstOrDefault().ShopId;

            var temp = (from u in _context.Users
                        from ur in u.Roles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        join e in _context.ShopEmployee on u.Id equals e.UserId
                        where e.ShopId == shopid
                        select new
                        {
                            UserId = e.UserId,
                            UserName = u.UserName,
                            Email = u.Email,
                            Gender = u.Sex,
                            Phone = u.PhoneNumber,
                            RoleName = r.Name
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
                            RoleName = g.Any(x => x.RoleName == RoleName.SHOP_MANAGER) ? "經理" : "員工"
                        };

            result = group.ToList();
            return result;
        }

        public OperationResult DeleteEmployee(string id)
        {
            var result = new OperationResult();
            try
            {
                var userStore = new UserStore<TicketHubUser>(_context);
                var userManager = new UserManager<TicketHubUser>(userStore);
                var emp = _context.ShopEmployee.Where((s) => s.UserId == id).FirstOrDefault();
                var empCount = _context.ShopEmployee.Where(s => s.ShopId == emp.ShopId).Count();

                if(empCount > 1)
                {
                    _context.ShopEmployee.Remove(emp);
                    _context.SaveChanges();

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
                else
                {
                    result.Message = "員工不得少於一位";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
            }
            return result;
        }

        public OperationResult createEmployee(string account)
        {
            var result = new OperationResult();
            try
            {
                var userStore = new UserStore<TicketHubUser>(_context);
                var userManager = new UserManager<TicketHubUser>(userStore);
                var employeeRepo = new GenericRepository<ShopEmployee>(_context);

                var shopid = _context.ShopEmployee.Where((x) => x.UserId == _userid).FirstOrDefault().ShopId;
                var userid = userManager.FindByEmail(account).Id;
                var entity = new ShopEmployee() { ShopId = shopid, UserId = userid };
                employeeRepo.Create(entity);
                _context.SaveChanges();

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

        public IResult CreateEmployeeWithRole(string account, string role = RoleName.SHOP_EMPLOYEE)
        {
            var result = new OperationResult();
            try
            {
                var userStore = new UserStore<TicketHubUser>(_context);
                var userManager = new UserManager<TicketHubUser>(userStore);
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