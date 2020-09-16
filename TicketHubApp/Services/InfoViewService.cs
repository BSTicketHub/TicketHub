using System;
using System.Configuration;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;

namespace TicketHubApp.Services
{
    public class InfoViewService
    {
        private readonly static string InfoViewName = "Info";
        private readonly string _checkIcon = ConfigurationManager.AppSettings["CheckIcon"].ToString();
        private readonly string _errorIcon = ConfigurationManager.AppSettings["ErrorIcon"].ToString();
        private readonly string _mailConfirmIcon = ConfigurationManager.AppSettings["MailConfirmIcon"].ToString();
        private readonly string _passResetIcon = ConfigurationManager.AppSettings["PassResetIcon"].ToString();
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
            return SuccessInfoView(infoViewModel);
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
            return SuccessInfoView(infoViewModel);
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
            return SuccessInfoView(infoViewModel);
        }

        public ViewResult Error()
        {
            InfoViewModel infoViewModel = new InfoViewModel
            {
                Title = "Error",
                Content = "Something is wrong.",
                SubTitle = "Error"
            };
            return ErrorInfoView(infoViewModel);
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
            return SuccessInfoView(infoViewModel);
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
            return SuccessInfoView(infoViewModel);
        }
        public ViewResult SuccessInfoView(InfoViewModel viewModel)
        {
            //default icon
            if (string.IsNullOrEmpty(viewModel.IconName))
            {
                viewModel.IconName = _checkIcon;
            }
            return InfoView(InfoType.Success, viewModel);
        }
        public ViewResult ErrorInfoView(InfoViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.IconName))
            {
                viewModel.IconName = _errorIcon;
            }
            return InfoView(InfoType.Error, viewModel);
        }
        public ViewResult InfoView(InfoType infoType, InfoViewModel viewModel)
        {
            viewModel.InfoType = Convert.ToBoolean(infoType);

            ViewResult viewResult = new ViewResult();
            viewResult.ViewName = InfoViewName;
            viewResult.ViewData.Model = viewModel;
            return viewResult;
        }
    }
}
