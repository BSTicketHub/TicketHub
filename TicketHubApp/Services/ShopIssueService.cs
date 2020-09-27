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
using System.Runtime.CompilerServices;

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
                var tagRepo = new GenericRepository<Tag>(_context);
                var imgurService = new ImgurService();
                var imgPath = imgurService.UploadImgur(input.ImgFile);

                string[] tagList = input.TagString.Split(' ');
                var issueTagCol = new List<IssueTag>();
                foreach (var item in tagList)
                {
                    if(!tagRepo.GetAll().Select(x => x.Name).Contains(item))
                    {
                        var tag = new Tag { Name = item };
                        tagRepo.Create(tag);
                    }
                }
                _context.SaveChanges();

                using (var _context2 = new TicketHubContext())
                {
                    var tagIds = _context2.Tag.Where(x => tagList.Contains(x.Name)).Select(y => y.Id).ToList();
                    foreach(var id in tagIds)
                    {
                        issueTagCol.Add(new IssueTag() { TagId = id});
                    }
                    
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
                        ShopId = Shopid,
                        IssueTags = issueTagCol,
                        Category = input.Category
                    };
                    _context2.Issue.Add(entity);
                    _context2.SaveChanges();
                }

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
                var tagRepo = new GenericRepository<Tag>(_context);

                List<string> tagList = input.TagString.Split(' ').ToList();
                // 新增 Tag table
                foreach (var item in tagList)
                {
                    if (!tagRepo.GetAll().Select(x => x.Name).Contains(item))
                    {
                        var tag = new Tag { Name = item };
                        tagRepo.Create(tag);
                    }
                }
                _context.SaveChanges();

                using (var _context2 = new TicketHubContext())
                {
                    // 新增 / 刪除 issueTag table
                    var issTagRepo = new GenericRepository<IssueTag>(_context2);
                    var beforeTag = (from t in _context2.Tag
                                     join it in _context2.IssueTag on t.Id equals it.TagId
                                     where it.IssueId == input.Id
                                     select t).ToList();
                    var beforeTagName = beforeTag.Select(g => g.Name).ToList();
                    var discardTagId = beforeTag.Where(x => !tagList.Contains(x.Name)).Select(y => y.Id).ToList();
                    var newAddTag = tagList.Where(x => !beforeTagName.Contains(x));
                    var newTagId = _context2.Tag.Where(x => newAddTag.Contains(x.Name)).Select(y => y.Id).ToList();

                    foreach (var tagid in discardTagId)
                    {
                        issTagRepo.Delete(new IssueTag { IssueId = input.Id, TagId = tagid });
                    }

                    foreach (var tagid in newTagId)
                    {
                        issTagRepo.Create(new IssueTag { IssueId = input.Id, TagId = tagid });
                    }
                    _context2.SaveChanges();
                }

                var oldIssue = _context.Issue.FirstOrDefault(x => x.Id == input.Id);
                using (var _context3 = new TicketHubContext())
                {
                    // 修改 issue table
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
                        ShopId = Shopid,
                        ImgPath = oldIssue.ImgPath,
                        Category = input.Category
                    };
                    if (input.ImgFile != null)
                    {
                        var imgurService = new ImgurService();
                        entity.ImgPath = imgurService.UploadImgur(input.ImgFile);
                    }

                    var issueRepo = new GenericRepository<Issue>(_context3);
                    issueRepo.Update(entity);
                    _context3.SaveChanges();
                }

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
                           count = od == null ? 0 : od.Amount,
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
                             SalesCount = g.Sum(x => x.count),
                             SalesPrice = g.Sum(x => x.price)
                         };

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
                    issues = issues.OrderByDescending(i => i.SalesCount);
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
                    SalesAmount = (int)item.SalesCount,
                    SalesPrice = item.SalesPrice
                };
                result.Items.Add(p);
            }
            return result;
        }

        public ShopIssueViewModel GetIssue(Guid Id)
        {
            var issueRepo = new GenericRepository<Issue>(_context);
            var item = issueRepo.GetAll().First((x) => x.Id == Id);

            var tempString = from it in _context.IssueTag
                             join t in _context.Tag on it.TagId equals t.Id
                             where it.IssueId == item.Id
                             select t.Name;
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
                            (item.ReleasedDate < DateTime.Now) ? "未上架" : "上架",
                TagList = tempString.ToList(),
                TagString = string.Join(" ", tempString),
                Category = item.Category
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