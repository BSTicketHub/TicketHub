using System;
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
                UserService userManager = new UserService();
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
                    userManager.AddUserWithRole(createdUser, RoleService.Roles.Customer);
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
                UserService userManager = new UserService();
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
                UserService userManager = new UserService();
                bool hasUser = userManager.HasUser(viewModel.Account);

                if (hasUser)
                {
                    User user = userManager.GetUser(viewModel.Account);
                    bool isValid = userManager.IsValidUser(user, viewModel.Password);
                    bool hasRole = userManager.UserHasRole(user, RoleService.Roles.ShopEmployee);

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
