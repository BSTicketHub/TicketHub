using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ServiceModels;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class AccountService
    {
        public IResult CreateUser(LoginViewModel viewModel)
        {
            IResult result = new OperationResult();
            try
            {
                UserManager userManager = new UserManager();
                bool hasUser = userManager.HasUser(viewModel.Account);
                if (!hasUser)
                {
                    UserToken token = userManager.GenerateUserToken(viewModel.Password);
                    User user = new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = viewModel.Account,
                        Email = viewModel.Account,
                        PasswordHash = token.PasswordHash,
                        PasswordSalt = token.PasswordSalt,
                        PasswordWorkFactor = token.PasswordWorkFactor
                    };
                    User createdUser = userManager.AddUser(user);
                    userManager.AddUserWithRole(createdUser, RoleManager.Roles.Customer);
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public HttpCookie GenCookie(string account)
        {
            UserManager userManager = new UserManager();

            User user = userManager.GetUser(account);
            var roles = user.Roles.Select(x=>x.Name);

            var ticket = new FormsAuthenticationTicket(
                        version: 1,
                        name: account, //可以放使用者Id
                        issueDate: DateTime.UtcNow,//現在UTC時間
                        expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                        isPersistent: true,// 是否要記住我 true or false
                        userData: string.Join(",", roles), //可以放使用者角色名稱
                        cookiePath: FormsAuthentication.FormsCookiePath
                    );

            var encryptedTicket = FormsAuthentication.Encrypt(ticket); //把驗證的表單加密
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            return cookie;
        }

        /// <summary>
        /// 驗證使用者
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public IResult ValidateUser(LoginViewModel viewModel)
        {
            IResult result = new OperationResult();
            try
            {
                UserManager userManager = new UserManager();
                bool hasUser = userManager.HasUser(viewModel.Account);

                if (hasUser)
                {
                    User user = userManager.GetUser(viewModel.Account);
                    bool isValid = userManager.IsValidUser(user, viewModel.Password);
                    result.Success = isValid;
                    result.Message = isValid ? "User Valid" : "User InValid";
                }
                else
                {
                    result.Message = "User Invalid";
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 驗證商家使用者
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public IResult ValidateShopUser(LoginViewModel viewModel)
        {
            IResult result = new OperationResult();
            try
            {
                UserManager userManager = new UserManager();
                bool hasUser = userManager.HasUser(viewModel.Account);

                if (hasUser)
                {
                    User user = userManager.GetUser(viewModel.Account);
                    bool isValid = userManager.IsValidUser(user, viewModel.Password);
                    bool hasRole = userManager.UserHasRole(user, RoleManager.Roles.ShopEmployee);

                    result.Success = isValid && hasRole;
                    result.Message = isValid && hasRole ? "User Valid" : "User InValid";
                }
                else
                {
                    result.Message = "User Invalid";
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
