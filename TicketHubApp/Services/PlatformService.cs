using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TicketHubApp.Controllers;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class PlatformService
    {
        private readonly string _errorIcon = ConfigurationManager.AppSettings["ErrorIcon"].ToString();

        // User Service //
        public DataTableViewModel GetUsersTableData()
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);
            GenericRepository<IdentityUserRole> roleRepository = new GenericRepository<IdentityUserRole>(context);

            var memberList = from u in repository.GetAll()
                             join r in roleRepository.GetAll()
                             on u.Id equals r.UserId
                             //where r.RoleId == "5"
                             select new PlatformUserViewModel
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 Mobile = u.PhoneNumber,
                                 Deleted = u.Deleted,
                             };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in memberList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id);
                dataInstance.Add(item.UserName);
                dataInstance.Add(item.Mobile ?? "NA");
                dataInstance.Add(item.Deleted ? "已註銷" : "正常");

                table.data.Add(dataInstance);
            }

            return table;
        }
        public PlatformUserViewModel GetUser(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);
            var user = repository.GetAll().FirstOrDefault(x => x.Id == id);

            var userVM = new PlatformUserViewModel
            {
                Id = user.Id,
                Sex = user.Sex,
                AvatarPath = user.AvatarPath,
                UserAccount = user.Email,
                Deleted = user.Deleted,
                UserName = user.UserName,
                Mobile = user.PhoneNumber,
                Locked = user.LockoutEnabled == true && user.LockoutEndDateUtc >= DateTime.Now
            };

            return userVM;
        }

        public ActionResult CreateUser(PlatformUserViewModel userVM, AppIdentityUserManager userManager)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);
         
            var userExistInDb = userManager.FindByEmail(userVM.UserAccount);
            if(userExistInDb != null)
            {   
                return new InfoViewService().ErrorInfoView(new InfoViewModel() { InfoType = false, Title = "此帳號已存在 !", Content = "請重新輸入要建立的帳號", IconName = _errorIcon});
            }
            var newUser = new TicketHubUser
            {
                UserName = userVM.UserName,
                Email = userVM.UserAccount
            };

            var createResult = userManager.Create(newUser, userVM.UserPwd);
            if (createResult.Succeeded)
            {
                userManager.AddToRole(newUser.Id, RoleGroup.CUSTOMER);

                newUser.UserName = userVM.UserName;
                newUser.PhoneNumber = userVM.Mobile;
                newUser.Sex = userVM.Sex;
                newUser.EmailConfirmed = true;
                repository.Update(newUser);

                return new InfoViewService().RegisterSuccess();
            }
            else
            {   
                return new InfoViewService().ErrorInfoView(new InfoViewModel() { InfoType = false, Title = "新增帳號發生錯誤!", Content = "請重新輸入要建立的帳號", IconName = _errorIcon });
            }
        }

        public void EditUserById(PlatformUserViewModel userVM)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);

            var user = repository.GetAll().FirstOrDefault(u => u.Id == userVM.Id);
            user.UserName = userVM.UserName;
            user.PhoneNumber = userVM.Mobile;
            user.Sex = userVM.Sex;

            context.SaveChanges();
        }
        public void DeleteUserById(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);
            var user = repository.GetAll().FirstOrDefault(x => x.Id == id);

            user.DeletedDate = DateTime.Now;
            user.Deleted = true;
            context.SaveChanges();
        }
        public void RestoreUserById(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);
            var user = repository.GetAll().FirstOrDefault(x => x.Id == id);

            user.Deleted = false;

            context.SaveChanges();
        }
        public DataTableViewModel GetTicketsTableData(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Ticket> ticketRepository = new GenericRepository<Ticket>(context);
            GenericRepository<Shop> shopRepository = new GenericRepository<Shop>(context);
            GenericRepository<Issue> issueRepository = new GenericRepository<Issue>(context);

            var ticketList = from t in ticketRepository.GetAll().Where(t => t.UserId == id)
                             join i in issueRepository.GetAll()
                             on t.IssueId equals i.Id
                             join s in shopRepository.GetAll()
                             on i.ShopId equals s.Id
                             select new PlatformUserTicketsViewModel
                             {
                                 Id = t.Id,
                                 Name = i.Title,
                                 Price = i.DiscountPrice,
                                 ShopName = s.ShopName,
                                 DeliveredDate = t.DeliveredDate,
                                 Exchanged = t.Exchanged,
                                 ExchangedDate = t.ExchangedDate,
                                 Voided = t.Voided,
                                 VoidedDate = t.VoidedDate
                             };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in ticketList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id.ToString());
                dataInstance.Add(item.Name);
                dataInstance.Add(item.Price.ToString());
                dataInstance.Add(item.ShopName);
                dataInstance.Add(item.DeliveredDate.ToLocalTime().ToString());
                dataInstance.Add(item.Exchanged.ToString());
                dataInstance.Add(item.ExchangedDate.ToString() ?? "NA");
                dataInstance.Add(item.Voided.ToString());
                dataInstance.Add(item.VoidedDate.ToString() ?? "NA");

                table.data.Add(dataInstance);
            }

            return table;
        }
        // Shop Service //
        public PlatformShopViewModel GetShop(string id)

        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Shop> repository = new GenericRepository<Shop>(context);
            var shop = repository.GetAll().FirstOrDefault(s => s.Id.ToString() == id);

            var shopVM = new PlatformShopViewModel()
            {
                Id = shop.Id,
                Address = shop.Address,
                BannerImg = shop.BannerImg,
                ShopName = shop.ShopName,
                Email = shop.Email,
                Fax = shop.Fax,
                Phone = shop.Phone,
                Website = shop.Website
            };

            return shopVM;
        }
        public DataTableViewModel GetShopsTableData()
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Shop> repository = new GenericRepository<Shop>(context);
            IQueryable<Shop> shopList = repository.GetAll();
            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in shopList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id.ToString());
                dataInstance.Add(item.ShopName);
                dataInstance.Add(item.Email);
                dataInstance.Add(item.Phone);
                dataInstance.Add(item.AppliedDate.ToString());
                dataInstance.Add(item.ModifiedDate.ToString());

                table.data.Add(dataInstance);
            }
            return table;
        }
        public DataTableViewModel GetEmployeesTableData(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<ShopEmployee> employeeRepository = new GenericRepository<ShopEmployee>(context);
            GenericRepository<TicketHubUser> userRepository = new GenericRepository<TicketHubUser>(context);

            var employeesList = from e in employeeRepository.GetAll().Where(e => e.ShopId.ToString() == id)
                                join u in userRepository.GetAll()
                                on e.UserId equals u.Id
                                select new PlatformEmployeeViewModel
                                {
                                    Id = u.Id,
                                    UserName = u.UserName,
                                    Email = u.Email,
                                    Phone = u.PhoneNumber

                                };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in employeesList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id);
                dataInstance.Add(item.UserName);
                dataInstance.Add(item.Email);
                dataInstance.Add(item.Phone);

                table.data.Add(dataInstance);
            }

            return table;
        }
        public DataTableViewModel GetIssuesTableData(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepository = new GenericRepository<Issue>(context);
            GenericRepository<OrderDetail> orderDetailRepository = new GenericRepository<OrderDetail>(context);

            var temp = from od in orderDetailRepository.GetAll()
                       join i in issueRepository.GetAll().Where(i => i.ShopId.ToString() == id)
                       on od.IssueId equals i.Id into j
                       from sub in j.DefaultIfEmpty()
                       select new
                       {
                           Id = sub.Id,
                           Title = sub.Title,
                           IssuedDate = sub.IssuedDate,
                           ReleasedDate = sub.ReleasedDate,
                           ClosedDate = sub.ClosedDate,
                           DiscountPrice = sub.DiscountPrice,
                           Amount = sub.Amount,
                           SelledAmount = od.Amount,
                           TotalRevenue = od.Price
                       };

            var group = from t in temp
                        group t by new { t.Id, t.Title, t.Amount, t.IssuedDate, t.ReleasedDate, t.ClosedDate, t.DiscountPrice } into g
                        select new
                        {
                            Id = g.Key.Id,
                            Title = g.Key.Title,
                            IssuedDate = g.Key.IssuedDate,
                            ReleasedDate = g.Key.ReleasedDate,
                            ClosedDate = g.Key.ClosedDate,
                            DiscountPrice = g.Key.DiscountPrice,
                            Amount = g.Key.Amount,
                            SelledAmount = g.Sum(x => x.SelledAmount),
                            TotalRevenue = g.Sum(x => x.TotalRevenue),
                            Stock = g.Key.Amount - g.Sum(x => x.SelledAmount)
                        };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in group)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id.ToString());
                dataInstance.Add(item.Title);
                dataInstance.Add(item.IssuedDate.ToString());
                dataInstance.Add(item.ReleasedDate.ToString());
                dataInstance.Add(item.ClosedDate.ToString());
                dataInstance.Add(item.DiscountPrice.ToString());
                dataInstance.Add(item.Amount.ToString());
                dataInstance.Add(item.SelledAmount.ToString());
                dataInstance.Add(item.TotalRevenue.ToString());
                dataInstance.Add(item.Stock.ToString());

                table.data.Add(dataInstance);
            }

            return table;
        }
        public Object GetSalesData(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepository = new GenericRepository<Issue>(context);
            GenericRepository<OrderDetail> orderDetailRepository = new GenericRepository<OrderDetail>(context);

            var temp = from i in issueRepository.GetAll().Where(i => i.Id.ToString() == id)
                       join od in orderDetailRepository.GetAll()
                       on i.Id equals od.IssueId
                       select new
                       {
                           Id = i.Id,
                           Title = i.Title,
                           Amount = i.Amount,
                           SellingAmount = od.Amount,
                           Price = od.Price
                       };

            var group = from t in temp
                        group t by new { t.Id, t.Title, t.Amount } into g
                        select new
                        {
                            Id = g.Key.Id,
                            Title = g.Key.Title,
                            Amount = g.Key.Amount,
                            SelledAmount = g.Sum(x => x.SellingAmount),
                            TotalRevenue = g.Sum(x => x.Price)
                        };

            return group;
        }

        public DataTableViewModel GetShopsToBeReviewedTableData()
        {
            // VadlidDate 是 null 的 所有 Shops
            var context = new TicketHubContext();

            var shopsToBeReviewed = context.Shop.Where(s => s.ValidatedDate == null).OrderBy(s => s.AppliedDate);

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in shopsToBeReviewed)
            {
                List<string> dataInstance = new List<string>();
                dataInstance.Add(item.ShopName);
                dataInstance.Add(item.Phone);
                dataInstance.Add(item.Fax);
                dataInstance.Add(item.Address);
                dataInstance.Add(item.Website);
                dataInstance.Add(item.AppliedDate.ToString());

                table.data.Add(dataInstance);
            }

            return table;
        }

        // Order Service //
        public DataTableViewModel GetAllOrders()
        {
            var result = new List<PlatformOrderViewModel>();
            var context = new TicketHubContext();

            var orderRepository = new GenericRepository<Order>(context);
            var orderDetailRepository = new GenericRepository<OrderDetail>(context);

            var temp = from o in orderRepository.GetAll()
                       join od in orderDetailRepository.GetAll()
                       on o.Id equals od.OrderId
                       select new
                       {
                           Id = o.Id,
                           OrderDate = o.OrderedDate,
                           UserId = o.UserId,
                           UserName = o.User.UserName,
                           Price = od.Price,
                       };

            var group = from t in temp
                        group t by new { t.Id, t.OrderDate, t.UserId, t.UserName } into g
                        select new PlatformOrderViewModel
                        {
                            Id = g.Key.Id,
                            UserId = g.Key.UserId,
                            OrderDate = g.Key.OrderDate,
                            UserName = g.Key.UserName,
                            TotalPrice = g.Sum(x => x.Price)
                        };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in group)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id.ToString());
                dataInstance.Add(item.UserId);
                dataInstance.Add(item.UserName);
                dataInstance.Add(item.TotalPrice.ToString());

                table.data.Add(dataInstance);
            }

            return table;
        }
        //Admin Service //
        public DataTableViewModel GetAdminsTableData()
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> userRepository = new GenericRepository<TicketHubUser>(context);
            GenericRepository<IdentityUserRole> roleRepository = new GenericRepository<IdentityUserRole>(context);

            var adminList = from u in userRepository.GetAll()
                            join r in roleRepository.GetAll()
                            on u.Id equals r.UserId
                            where r.RoleId == "1" || r.RoleId == "2"
                            select new PlatformAdminViewModel
                            {
                                Id = u.Id,
                                Name = u.UserName,
                                Email = u.Email,
                                Phone = u.PhoneNumber
                            };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in adminList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id);
                dataInstance.Add(item.Name);
                dataInstance.Add(item.Email);
                dataInstance.Add(item.Phone);

                table.data.Add(dataInstance);
            }

            return table;
        }
        public PlatformAdminViewModel GetAdmin(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);
            var admin = repository.GetAll().FirstOrDefault(x => x.Id == id);

            var adminVM = new PlatformAdminViewModel
            {
                Id = admin.Id,
                Name = admin.UserName,
                Email = admin.Email,
                Phone = admin.PhoneNumber
            };

            return adminVM;
        }

        //public static string HashPassword(string password)
        //{
        //    byte[] salt;
        //    byte[] buffer2;
        //    if (password == null)
        //    {
        //        throw new ArgumentNullException("password");
        //    }
        //    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
        //    {
        //        salt = bytes.Salt;
        //        buffer2 = bytes.GetBytes(0x20);
        //    }
        //    byte[] dst = new byte[0x31];
        //    Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        //    Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        //    return Convert.ToBase64String(dst);
        //}
    }
}
