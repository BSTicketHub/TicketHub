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
        private TicketHubContext _context = new TicketHubContext();

        private readonly string _userid = HttpContext.Current.User.Identity.GetUserId();

        public OperationResult CreateIssue(ShopIssueViewModel input)
        {
            var result = new OperationResult();
            try
            {
                Guid Shopid = _context.ShopEmployee.FirstOrDefault((x) => x.UserId == _userid).ShopId;
                var issueRepo = new GenericRepository<Issue>(_context);
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
                    IssuedDate = DateTime.UtcNow.AddHours(8),
                    ReleasedDate = input.ReleasedDate,
                    ClosedDate = input.ClosedDate,
                    IssuerId = _userid,
                    ShopId = Shopid
                };
                issueRepo.Create(entity);
                _context.SaveChanges();
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
                Guid Shopid = _context.ShopEmployee.FirstOrDefault((x) => x.UserId == _userid).ShopId;
                var issueRepo = new GenericRepository<Issue>(_context);
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
                    IssuerId = _userid,
                    ShopId = Shopid
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
                _context.SaveChanges();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.ToString();
            }
            return result;
        }

        public ShopIssueListViewModel GetIssueListApi(int? order, bool closed)
        {
            Guid Shopid = _context.ShopEmployee.FirstOrDefault((x) => x.UserId == _userid).ShopId;
            var result = new ShopIssueListViewModel();
            result.Items = new List<ShopIssueViewModel>();
            var a = _context.OrderDetail.ToList();
            var b = _context.Issue.ToList();

            var temp = from i in _context.Issue
                       where (i.ShopId == Shopid)
                       join od in _context.OrderDetail on i.Id equals od.IssueId into j
                       from od in j.DefaultIfEmpty()
                       select new
                       {
                           i.Id,
                           i.ImgPath,
                           i.Title,
                           i.DiscountPrice,
                           i.ReleasedDate,
                           i.ClosedDate,
                           price = od == null ? 0 : od.Price
                       };
            var issues = from t in temp
                         group t by new { t.Id, t.ImgPath, t.Title, t.DiscountPrice, t.ReleasedDate, t.ClosedDate } into g
                         select new
                         {
                             g.Key.Id,
                             g.Key.ImgPath,
                             g.Key.Title,
                             g.Key.DiscountPrice,
                             g.Key.ReleasedDate,
                             g.Key.ClosedDate,
                             SalesAmount = g.Sum(x => x.price)
                         };
            //var tempxxx = temp.ToList();
            //var temp2222 = issues.ToList();

            var TimeNow = DateTime.Now;
            if (closed)
            {
                issues = issues.Where((i) => i.ClosedDate <= TimeNow);
            }
            else
            {
                issues = issues.Where((i) => (i.ClosedDate > TimeNow || i.ClosedDate == null));
            }

            switch (order)
            {
                case 1:
                    issues = issues.OrderByDescending(i => i.SalesAmount);
                    break;
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
                    Status = (item.ClosedDate <= DateTime.Now) ? "已下架" :
                            (item.ReleasedDate < DateTime.Now) ? "未上架" : "上架",
                    ReleasedDate = item.ReleasedDate,
                    SalesPrice = item.SalesAmount
                };
                result.Items.Add(p);
            }
            return result;
        }

        public ShopIssueViewModel GetIssue(Guid Id)
        {
            var issueRepo = new GenericRepository<Issue>(_context);
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
                Status = (item.ClosedDate <= DateTime.Now) ? "已下架" :
                            (item.ReleasedDate < DateTime.Now) ? "未上架" : "上架"
            };
            return entity;
        }

        public DataTableViewModel GetIssueDetailsApi(Guid id)
        {
            var issueDetail = new ShopIssueDetailViewModel();

            var ttt = from tic in _context.Ticket
                      join us in _context.Users on tic.UserId equals us.Id
                      where tic.IssueId == id
                      select new { tic.User.UserName, tic.User.PhoneNumber, tic.Exchanged, tic.OrderId };
            var aaa = from t3 in ttt
                      group t3 by new { t3.UserName, t3.PhoneNumber, t3.OrderId } into g
                      select new
                      {
                          g.Key.UserName,
                          TicketCount = g.Count(),
                          g.Key.OrderId,
                          g.Key.PhoneNumber,
                          ExchangeCount = g.Count(x => x.Exchanged == true),
                          ReleaseCount = g.Count(x => x.Exchanged != true)
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
                var issueRepo = new GenericRepository<Issue>(_context);
                Issue issue = _context.Issue.Where(x => x.Id == id).FirstOrDefault();
                if (issue == null)
                {
                    result.Success = false;
                    return result;
                }

                issue.ClosedDate = DateTime.Now;
                issueRepo.Update(issue);
                _context.SaveChanges();

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