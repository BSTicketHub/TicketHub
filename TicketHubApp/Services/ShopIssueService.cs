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
using TicketHubApp.Services;
using System.Data.Entity;
using System.Security.Cryptography;

namespace TicketHubApp.Services
{
    public class ShopIssueService
    {
        public OperationResult CreateIssue(ShopIssueViewModel input)
        {
            var result = new OperationResult();
            try
            {
                TicketHubContext context = new TicketHubContext();
                var issueRepo = new GenericRepository<Issue>(context);
                var employeeRepo = new GenericRepository<ShopEmployee>(context);
                //var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                //var employee = employeeRepo.GetAll().Where((x) => x.UserId == user).FirstOrDefault();

                var imgurService = new ImgurService();
                var imgPath = imgurService.UploadImgur(input.ImgFile);

                var entity = new Issue
                {
                    Title = input.Title,
                    Memo = input.Memo,
                    ImgPath = imgPath,
                    OriginalPrice = input.OriginalPrice,
                    DiscountPrice = input.DiscountPrice,
                    DiscountRatio = input.OriginalPrice / input.DiscountPrice,
                    Amount = input.Amount,
                    IssuedDate = DateTime.Now,
                    ReleasedDate = input.ReleasedDate,
                    ClosedDate = input.ClosedDate,
                    IssuerId = "26c751ea-d1ce-45bf-8a65-78f0d48ce2c4", //IssuerId = user,
                    ShopId = Guid.Parse("FA10840D-3A73-4374-AAFD-D592A3623EC1") //ShopId = employee.ShopId
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

        public OperationResult UpdateIssue(ShopIssueViewModel input)
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
                    Id = input.Id,
                    Title = input.Title,
                    Memo = input.Memo,
                    OriginalPrice = input.OriginalPrice,
                    DiscountPrice = input.DiscountPrice,
                    DiscountRatio = input.OriginalPrice / input.DiscountPrice,
                    Amount = input.Amount,
                    IssuedDate = DateTime.Now,
                    ReleasedDate = input.ReleasedDate,
                    ClosedDate = input.ClosedDate,
                    IssuerId = "26c751ea-d1ce-45bf-8a65-78f0d48ce2c4", //IssuerId = user,
                    ShopId = Guid.Parse("FA10840D-3A73-4374-AAFD-D592A3623EC1") //ShopId = employee.ShopId
                };

                if (input.ImgFile != null)
                {
                    var imgurService = new ImgurService();
                    entity.ImgPath = imgurService.UploadImgur(input.ImgFile);
                }
                else
                {
                    entity.ImgPath = input.ImgPath;
                }

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
    
        public ShopIssueListViewModel GetIssueListApi(int? order, bool closed)
        {
            var result = new ShopIssueListViewModel();
            result.Items = new List<ShopIssueViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);
            GenericRepository<OrderDetail> orderDetailRepo = new GenericRepository<OrderDetail>(context);

            var issues = issueRepo.GetAll();
            var TimeNow = DateTime.Now;
            if (closed)
            {
                issues = issues.Where((i) => i.ClosedDate <= TimeNow);
            }
            else
            {
                issues = issues.Where((i) => i.ClosedDate > TimeNow);
            }

            switch (order)
            {
                //case 1:
                //    issues = issues.OrderByDescending(i => i.SalesAmount);
                //    break;
                case 2:
                    issues = issues.OrderBy(i => i.ReleasedDate);
                    break;
                case 3:
                    issues = issues.OrderByDescending(i => i.Id);
                    break;
                default:
                    issues = issues.OrderByDescending(i => (int)i.DiscountPrice);
                    break;
            }

            foreach (var item in issues)
            {
                var p = new ShopIssueViewModel()
                {
                    ImgPath = item.ImgPath,
                    Title = item.Title,
                    Id = item.Id,
                    DiscountPrice = item.DiscountPrice,
                    Status = (item.ClosedDate.Value <= DateTime.Now) ? "已下架" :
                            (item.ReleasedDate < DateTime.Now) ? "未上架" : "上架",
                    ReleasedDate = item.ReleasedDate
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
                ImgPath = item.ImgPath,
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

        public DataTableViewModel GetIssueDetailsApi(Guid id)
        {
            TicketHubContext context = new TicketHubContext();
            var issueDetail = new ShopIssueDetailViewModel();

            var ttt = from tic in context.Ticket
                      join us in context.Users on tic.UserId equals us.Id
                      where tic.IssueId == id
                      select new { tic.User.UserName, tic.User.PhoneNumber, tic.Exchanged, tic.OrderId };
            var aaa = from t3 in ttt
                      group t3 by new { t3.UserName, t3.PhoneNumber, t3.OrderId } into g
                      select new { g.Key.UserName, TicketCount = g.Count(), g.Key.OrderId, g.Key.PhoneNumber,
                                ExchangeCount = g.Count(x => x.Exchanged == true), ReleaseCount = g.Count(x => x.Exchanged != true)
                      };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();
            foreach (var item in aaa)
            {
                List<string> dataInstance = new List<string>();
                dataInstance.Add(item.OrderId.ToString());
                dataInstance.Add(item.UserName);
                dataInstance.Add(item.TicketCount.ToString());
                dataInstance.Add(item.ExchangeCount.ToString());
                dataInstance.Add(item.ReleaseCount.ToString());
                dataInstance.Add(item.PhoneNumber);

                table.data.Add(dataInstance);
            }
            return table;
        }
    
        public OperationResult closeIssueAPi(Guid id)
        {
            var result = new OperationResult();
            try
            {
                TicketHubContext context = new TicketHubContext();
                var issueRepo = new GenericRepository<Issue>(context);
                Issue issue = context.Issue.Where(x => x.Id == id).FirstOrDefault();
                if (issue == null) {
                    result.Success = false;
                    return result;
                }

                issue.ClosedDate = DateTime.Now;
                issueRepo.Update(issue);
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
    }
}