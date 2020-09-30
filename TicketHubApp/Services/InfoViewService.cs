using System;
using System.Configuration;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;

namespace TicketHubApp.Services
{
    public class InfoViewService
    {
        private readonly static string _infoSingle = "InfoSingle";
        private readonly static string _infoHome = "InfoHome";
        private readonly string _checkIcon = ConfigurationManager.AppSettings["CheckIcon"].ToString();
        private readonly string _errorIcon = ConfigurationManager.AppSettings["ErrorIcon"].ToString();
        private readonly string _mailConfirmIcon = ConfigurationManager.AppSettings["MailConfirmIcon"].ToString();
        private readonly string _passResetIcon = ConfigurationManager.AppSettings["PassResetIcon"].ToString();
        private readonly string _notFoundIcon = ConfigurationManager.AppSettings["NotFoundIcon"].ToString();
        public enum InfoType
        {
            Error = 0,
            Success = 1,
        }
        public ViewResult RegisterSuccess()
        {
            InfoViewModel infoViewModel = new InfoViewModel
            {
                Title = "Registration Success",
                IsCheckMail = true,
                SubTitle = "Registration"
            };
            return SuccessInfoSingleView(infoViewModel);
        }
        public ViewResult EmailConfirmed()
        {
            InfoViewModel infoViewModel = new InfoViewModel
            {
                IsShowLogin = true,
                IconName = _mailConfirmIcon,
                Title = "Email Confirmed",
                Content = "",
                SubTitle = "Confirmed"
            };
            return SuccessInfoSingleView(infoViewModel);
        }
        public ViewResult ResetPasswordConfirmation()
        {
            InfoViewModel infoViewModel = new InfoViewModel
            {
                IsShowLogin = true,
                IconName = _passResetIcon,
                Title = "Reset Password Confirmed",
                Content = "Password has reset.",
                SubTitle = "Confirmed"
            };
            return SuccessInfoSingleView(infoViewModel);
        }

        public ViewResult Error()
        {
            InfoViewModel infoViewModel = new InfoViewModel
            {
                Title = "Error",
                Content = "Something is wrong.",
                SubTitle = "Error"
            };
            return ErrorInfoSingleView(infoViewModel);
        }
        public ViewResult CheckMail(string title)
        {
            InfoViewModel infoViewModel = new InfoViewModel
            {
                IsShowLogin = false,
                Title = title,
                IsCheckMail = true,
                SubTitle = "Check Mail"
            };
            return SuccessInfoSingleView(infoViewModel);
        }
        public ViewResult CommonSuccess(string title, string content)
        {
            InfoViewModel infoViewModel = new InfoViewModel
            {
                IsShowLogin = false,
                Title = title,
                Content = content,
                SubTitle = "Success"
            };
            return SuccessInfoSingleView(infoViewModel);
        }
        public ViewResult SearchNotFound()
        {
            InfoViewModel infoViewModel = new InfoViewModel
            {
                Title = "很抱歉，您的搜尋沒有結果",
                Content = "試著用其他關鍵字再搜尋一次吧",
                SubTitle = "NotFound"
            };
            return CommonInfoHomeView(infoViewModel);
        }
        public ViewResult PageNotFound()
        {
            InfoViewModel infoViewModel = new InfoViewModel
            {
                Title = "很抱歉，無此頁面",
                Content = "請左轉離開另尋他路",
                SubTitle = "NotFound"
            };
            return CommonInfoHomeView(infoViewModel);
        }
        public ViewResult SuccessInfoSingleView(InfoViewModel viewModel)
        {
            //default icon
            if (string.IsNullOrEmpty(viewModel.IconName))
            {
                viewModel.IconName = _checkIcon;
            }
            return InfoView(InfoType.Success, _infoSingle, viewModel);
        }
        public ViewResult ErrorInfoSingleView(InfoViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.IconName))
            {
                viewModel.IconName = _errorIcon;
            }
            return InfoView(InfoType.Error, _infoSingle, viewModel);
        }

        public ViewResult CommonInfoHomeView(InfoViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.IconName))
            {
                viewModel.IconName = _notFoundIcon;
            }
            return InfoView(InfoType.Error, _infoHome, viewModel);
        }

        public ViewResult InfoView(InfoType infoType, string viewName, InfoViewModel viewModel)
        {
            viewModel.InfoType = Convert.ToBoolean(infoType);

            ViewResult viewResult = new ViewResult();
            viewResult.ViewName = viewName;
            viewResult.ViewData.Model = viewModel;
            return viewResult;
        }
    }
}
