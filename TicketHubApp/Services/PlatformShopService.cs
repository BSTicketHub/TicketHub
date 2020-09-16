using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class PlatformShopService
    {   
        public DataTableViewModel GetShopsTableData()
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Shop> repository = new GenericRepository<Shop>(context);
            IQueryable<Shop> shopList = repository.GetAll();
            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach(var item in shopList)
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
            //TicketHubContext context = new TicketHubContext();
            //GenericRepository<Issue> issueRepository = new GenericRepository<Issue>(context);
            //GenericRepository<Shop> shopRepository = new GenericRepository<Shop>(context);

            //var issueList = from i in issueRepository.GetAll().Where(i => i.ShopId.ToString() == id)
            //                join s in shopRepository.GetAll()
            //                on i.ShopId equals s.Id
            //                select new PlatformIssueViewModel
            //                {
            //                    Id = i.Id,
            //                    Title = i.Title,
            //                    IssuedDate = i.IssuedDate,
            //                    ReleasedDate = i.ReleasedDate,
            //                    ClosedDate = i.ClosedDate,
            //                    DiscountPrice = i.DiscountPrice,
            //                    Amount = i.Amount,
            //                };

            //DataTableViewModel table = new DataTableViewModel();
            //table.data = new List<List<string>>();

            //foreach (var item in issueList)
            //{
            //    List<string> dataInstance = new List<string>();

            //    dataInstance.Add(item.Id.ToString());
            //    dataInstance.Add(item.Title);
            //    dataInstance.Add(item.IssuedDate.ToString());
            //    dataInstance.Add(item.ReleasedDate.ToString());
            //    dataInstance.Add(item.ClosedDate.ToString());
            //    dataInstance.Add(item.DiscountPrice.ToString());
            //    dataInstance.Add(item.Amount.ToString());

            //    table.data.Add(dataInstance);
            //}

            //return table;

            ////////////////////////////////// 分界線 ////////////////////////////////////
            TicketHubContext context = new TicketHubContext();

            GenericRepository<Issue> issueRepository = new GenericRepository<Issue>(context);
            GenericRepository<Shop> shopRepository = new GenericRepository<Shop>(context);
            GenericRepository<OrderDetail> orderDetailRepository = new GenericRepository<OrderDetail>(context);
            
            var temp = from i in issueRepository.GetAll().Where(i => i.ShopId.ToString() == id)
                       join od in orderDetailRepository.GetAll()
                       on i.Id equals od.IssueId
                       select new
                       {
                           Id = i.Id,
                           Title = i.Title,
                           IssuedDate = i.IssuedDate,
                           ReleasedDate = i.ReleasedDate,
                           ClosedDate = i.ClosedDate,
                           DiscountPrice = i.DiscountPrice,
                           Amount = i.Amount,
                           SellingAmount = od.Amount,
                           Price = od.Price
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
                            SelledAmount = g.Sum(x => x.SellingAmount),
                            TotalRevenue = g.Sum(x => x.Price),
                            Stock = g.Key.Amount - g.Sum(x => x.SellingAmount)
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
    }
}