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
                    IssuerId = "6a2b0a3d-9de5-470d-8519-3bba75de4246", //IssuerId = user,
                    ShopId = Guid.Parse("2298405e-54f4-ea11-9700-cb781453451a") //ShopId = employee.ShopId
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
                    IssuerId = "6a2b0a3d-9de5-470d-8519-3bba75de4246", //IssuerId = user,
                    ShopId = Guid.Parse("2298405e-54f4-ea11-9700-cb781453451a") //ShopId = employee.ShopId
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
    
        public ShopIssueListViewModel GetAll(int? order)
        {
            var result = new ShopIssueListViewModel();
            result.Items = new List<ShopIssueViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);
            GenericRepository<OrderDetail> orderDetailRepo = new GenericRepository<OrderDetail>(context);

            var issues = from i in issueRepo.GetAll()
                         select new ShopIssueViewModel
                         {
                             ImgPath = i.ImgPath,
                             Title = i.Title,
                             Id = i.Id,
                             DiscountPrice = i.DiscountPrice,
                             Status = (DateTime.Compare(i.ReleasedDate, DateTime.Now) > 0) ?
                                ((DateTime.Compare((DateTime)i.ClosedDate, DateTime.Now) < 0) ? "已下架" : "上架") : "未上架",
                             IssuedDate = i.IssuedDate,
                             ReleasedDate = i.ReleasedDate,
                             //SalesAmount =
                             //   (from i2 in issueRepo.GetAll()
                             //   join od in orderDetailRepo.GetAll() on i.Id equals od.IssueId
                             //   group i2 by i2.IssuerId into iod
                             //   where i.Id == iod.Key
                             //   select new { salesamount = (int)iod.Sum((x) => x.Amount) })
                         };
                                
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
                    issues = issues.OrderByDescending(i => i.DiscountPrice);
                    break;
            }

            //foreach (var item in issues)
            //{
            //    var p = new ShopIssueViewModel()
            //    {
            //        ImgPath = item.ImgPath,
            //        Title = item.Title,
            //        Id = item.Id,
            //        DiscountPrice = item.DiscountPrice,
            //        Status = (DateTime.Compare(item.ReleasedDate, DateTime.Now) > 0) ?
            //                ((DateTime.Compare((DateTime)item.ClosedDate, DateTime.Now) < 0) ? "已下架" : "上架") : "未上架",
            //        IssuedDate = item.IssuedDate
            //    };

            //    result.Items.Add(p);
            //}

            result.Items = issues.ToList();
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
    }
}