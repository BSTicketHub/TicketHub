using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary;
using TicketHubDataLibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;

namespace TicketHubApp.Services
{
    public class ShopIssueService
    {
        public OperationResult Create(ShopIssueViewModel input)
        {
            var result = new OperationResult();
            try
            {
                TicketHubContext context = new TicketHubContext();
                var issueRepo = new GenericRepository<Issue>(context);
                var employeeRepo = new GenericRepository<ShopEmployee>(context);
                //var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                //var employee = employeeRepo.GetAll().Where((x) => x.UserId == user).FirstOrDefault();

                var entity = new Issue
                {
                    Title = input.Title,
                    Memo = input.Memo,
                    ImgPath = input.TicketImg,
                    OriginalPrice = input.OriginalPrice,
                    DiscountPrice = input.DiscountPrice,
                    DiscountRatio = input.OriginalPrice / input.DiscountPrice,
                    Amount = input.Amount,
                    IssuedDate = DateTime.Now,
                    ReleasedDate = input.ReleasedDate,
                    ClosedDate = input.ClosedDate,
                    IssuerId = "26c751ea-d1ce-45bf-8a65-78f0d48ce2c4", //IssuerId = user,
                    ShopId = Guid.Parse("fa10840d-3a73-4374-aafd-d592a3623ec1") //ShopId = employee.ShopId
                };

                issueRepo.Create(entity);
                context.SaveChanges();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.ToString();
            }

            return result;
        }

        public OperationResult Update(ShopIssueViewModel input)
        {
            var result = new OperationResult();
            try
            {
                TicketHubContext context = new TicketHubContext();
                var issueRepo = new GenericRepository<Issue>(context);
                //var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                //var employee = employeeRepo.GetAll().Where((x) => x.UserId == user).FirstOrDefault();

                var entity = new Issue
                {
                    Id = Guid.Parse("bcb2db96-15ef-4d87-93bc-504e5bb95f3d"),
                    Title = input.Title,
                    Memo = input.Memo,
                    ImgPath = input.TicketImg,
                    OriginalPrice = input.OriginalPrice,
                    DiscountPrice = input.DiscountPrice,
                    DiscountRatio = input.OriginalPrice / input.DiscountPrice,
                    Amount = input.Amount,
                    IssuedDate = DateTime.Now,
                    ReleasedDate = input.ReleasedDate,
                    ClosedDate = input.ClosedDate,
                    IssuerId = "26c751ea-d1ce-45bf-8a65-78f0d48ce2c4", //IssuerId = user,
                    ShopId = Guid.Parse("fa10840d-3a73-4374-aafd-d592a3623ec1") //ShopId = employee.ShopId
                };

                issueRepo.Update(entity);
                context.SaveChanges();

                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.ToString();
            }

            return result;
        }
    
        public ShopIssueListViewModel GetAll()
        {
            var result = new ShopIssueListViewModel();
            result.Items = new List<ShopIssueViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);

            foreach (var item in issueRepo.GetAll().OrderBy((x) => x.IssuedDate))
            {
                var p = new ShopIssueViewModel()
                {
                    TicketImg = item.ImgPath,
                    Title = item.Title,
                    Id = item.Id,
                    DiscountPrice = item.DiscountPrice,
                    Status = (DateTime.Compare(item.ReleasedDate, DateTime.Now) > 0) ?
                            ((DateTime.Compare((DateTime)item.ClosedDate, DateTime.Now) < 0) ? "已下架" : "上架") : "未上架",
                    IssuedDate = item.IssuedDate
                };

                result.Items.Add(p);
            }
            return result;
        }

        public ShopIssueViewModel GetIssue(Guid Id)
        {
            TicketHubContext context = new TicketHubContext();
            var issueRepo = new GenericRepository<Issue>(context);
            var item = issueRepo.GetAll().First((x) => x.Id == Id);

            var entity = new ShopIssueViewModel
            {
                Id = item.Id,
                shopId = item.ShopId,
                TicketImg = item.ImgPath,
                Title = item.Title,
                Memo = item.Memo,
                OriginalPrice = item.OriginalPrice,
                DiscountPrice = item.DiscountPrice,
                Amount = item.Amount,
                IssuedDate = item.IssuedDate,
                ReleasedDate = item.ReleasedDate,
                ClosedDate = (DateTime)item.ClosedDate,
                IssuerId = item.IssuerId,
                Status = (DateTime.Compare(item.ReleasedDate, DateTime.Now) > 0) ?
                         ((DateTime.Compare((DateTime)item.ClosedDate, DateTime.Now) < 0) ? "已下架" : "上架") : "未上架"
            };

            return entity;
        }
    }
}