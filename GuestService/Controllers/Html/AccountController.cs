﻿namespace GuestService.Controllers.Html
{
    using DotNetOpenAuth.AspNet;
    using GuestService;
    using GuestService.Code;
    using GuestService.Data;
    using GuestService.Models;
    using GuestService.Resources;
    using Microsoft.Web.WebPages.OAuth;
    using Sm.System.Mvc.Language;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Xml.Linq;
    using WebMatrix.WebData;

    [UrlLanguage, HttpPreferences, WebSecurityInitializer, Authorize]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Confirm(string email, string token)
        {
            ((dynamic) base.ViewBag).Confirmed = WebSecurity.ConfirmAccount(email, token);
            return base.View();
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.InvalidUserName:
                    return AccountStrings.ErrorInvalidUserName;

                case MembershipCreateStatus.InvalidPassword:
                    return AccountStrings.ErrorInvalidPassword;

                case MembershipCreateStatus.InvalidQuestion:
                    return AccountStrings.ErrorInvalidQuestion;

                case MembershipCreateStatus.InvalidAnswer:
                    return AccountStrings.ErrorInvalidAnswer;

                case MembershipCreateStatus.InvalidEmail:
                    return AccountStrings.ErrorInvalidEmail;

                case MembershipCreateStatus.DuplicateUserName:
                    return AccountStrings.ErrorDuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return AccountStrings.ErrorDuplicateEmail;

                case MembershipCreateStatus.UserRejected:
                    return AccountStrings.ErrorUserRejected;

                case MembershipCreateStatus.ProviderError:
                    return AccountStrings.ErrorProviderError;
            }
            return AccountStrings.ErrorUnknownError;
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, base.Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(base.Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return base.RedirectToAction("ExternalLoginFailure");
            }
            bool flag = false;
            string userName = OAuthWebSecurity.GetUserName(result.Provider, result.ProviderUserId);
            if (userName != null)
            {
                flag = WebSecurity.IsConfirmed(userName);
                if (flag)
                {
                    bool createPersistentCookie = false;
                    if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie))
                    {
                        return this.RedirectToLocal(returnUrl);
                    }
                    if (base.User.Identity.IsAuthenticated)
                    {
                        OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, base.User.Identity.Name);
                        return this.RedirectToLocal(returnUrl);
                    }
                }
                else
                {
                    string userConfirmationToken = MembershipHelper.GetUserConfirmationToken(WebSecurity.GetUserId(userName));
                    if (userConfirmationToken != null)
                    {
                        this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, userName, userConfirmationToken);
                    }
                }
            }
            ((dynamic) base.ViewBag).NotConfirmedEmail = (userName != null) && !flag;
            string str3 = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
            AuthenticationClientData oAuthClientData = OAuthWebSecurity.GetOAuthClientData(result.Provider);
            ((dynamic) base.ViewBag).ProviderDisplayName = oAuthClientData.DisplayName;
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            RegisterExternalLoginModel model = new RegisterExternalLoginModel {
                UserName = (result.UserName.Contains("@") && result.UserName.Contains(".")) ? result.UserName : "",
                ExternalLoginData = str3
            };
            return base.View("ExternalLoginConfirmation", model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string providerName = null;
            string providerUserId = null;
            if (!(!base.User.Identity.IsAuthenticated && OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out providerName, out providerUserId)))
            {
                return this.RedirectToLocal(returnUrl);
            }
            if (base.ModelState.IsValid)
            {
                try
                {
                    string password = Guid.NewGuid().ToString();
                    bool requireConfirmationToken = true;
                    string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, password, null, requireConfirmationToken);
                    OAuthWebSecurity.CreateOrUpdateAccount(providerName, providerUserId, model.UserName);
                    this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, model.UserName, confirmationToken);
                    return base.RedirectToAction("registersuccess", new { returnUrl = returnUrl });
                }
                catch (MembershipCreateUserException exception)
                {
                    base.ModelState.AddModelError("", ErrorCodeToString(exception.StatusCode));
                }
            }
            ((dynamic) base.ViewBag).NotConfirmedEmail = false;
            ((dynamic) base.ViewBag).ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(providerName).DisplayName;
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return base.View();
        }

        [ChildActionOnly, AllowAnonymous]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return this.PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LoginModel model = new LoginModel();
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                bool rememberMe = model.RememberMe;
                if (WebSecurity.Login(model.UserName, model.Password, rememberMe))
                {
                    return this.RedirectToLocal(returnUrl);
                }
                int userId = WebSecurity.GetUserId(model.UserName);
                if ((userId > 0) && !WebSecurity.IsConfirmed(model.UserName))
                {
                    base.ModelState.AddModelError("", AccountStrings.AccountLogin_EmailNotConfirmed);
                    string userConfirmationToken = MembershipHelper.GetUserConfirmationToken(userId);
                    if (userConfirmationToken != null)
                    {
                        base.ModelState.AddModelError("", AccountStrings.RegisterEmailNotConfirmedNote);
                        this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, model.UserName, userConfirmationToken);
                    }
                }
                else
                {
                    base.ModelState.AddModelError("", AccountStrings.AccountLogin_InvalidCredentails);
                }
            }
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return base.RedirectToAction("index", "welcome");
        }

        [AllowAnonymous]
        public ActionResult Recovery(string returnUrl)
        {
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Recovery(RecoveryModel model, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    string confirmationToken = WebSecurity.GeneratePasswordResetToken(model.UserName, 0x5a0);
                    this.SendRegistrationConfirmMail(ConfirmMailOperation.recovery, model.UserName, confirmationToken);
                    return base.RedirectToAction("recoverysuccess", new { returnUrl = returnUrl });
                }
                catch (Exception)
                {
                    base.ModelState.AddModelError("", AccountStrings.AccountRecovery_CannotRecovery);
                }
            }
            return base.View();
        }

        [AllowAnonymous]
        public ActionResult RecoverySuccess(string returnUrl)
        {
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (base.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            return base.RedirectToAction("index", "welcome");
        }

        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            RegisterModel model = new RegisterModel();
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [AllowAnonymous, ValidateAntiForgeryToken, HttpPost]
        public ActionResult Register(RegisterModel model, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    bool requireConfirmationToken = true;
                    string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, null, requireConfirmationToken);
                    this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, model.UserName, confirmationToken);
                    return base.RedirectToAction("registersuccess", new { returnUrl = returnUrl });
                }
                catch (MembershipCreateUserException exception)
                {
                    base.ModelState.AddModelError("", ErrorCodeToString(exception.StatusCode));
                }
            }
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterSuccess(string returnUrl)
        {
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accountsFromUserName = OAuthWebSecurity.GetAccountsFromUserName(base.User.Identity.Name);
            List<GuestService.Models.ExternalLogin> model = new List<GuestService.Models.ExternalLogin>();
            foreach (OAuthAccount account in accountsFromUserName)
            {
                AuthenticationClientData oAuthClientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);
                GuestService.Models.ExternalLogin item = new GuestService.Models.ExternalLogin {
                    Provider = account.Provider,
                    ProviderDisplayName = oAuthClientData.DisplayName,
                    ProviderUserId = account.ProviderUserId
                };
                model.Add(item);
            }
            ((dynamic) base.ViewBag).ShowRemoveButton = (model.Count > 1) || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(base.User.Identity.Name));
            return this.PartialView("_RemoveExternalLoginsPartial", model);
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    bool flag = WebSecurity.ResetPassword(model.Token, model.Password);
                    return base.RedirectToAction("resetpasswordresult", new { result = flag, returnUrl = base.Url.Action("index", "welcome") });
                }
                catch (Exception)
                {
                    base.ModelState.AddModelError("", AccountStrings.ResetPassword_CannotReset);
                }
            }
            return base.View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {
            ResetPasswordModel model = new ResetPasswordModel {
                Token = token
            };
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordResult(bool? result, string returnUrl)
        {
            bool? nullable = result;
            ((dynamic) base.ViewBag).ResetPasswordResult = nullable.HasValue ? nullable.GetValueOrDefault() : false;
            ((dynamic) base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        private void SendRegistrationConfirmMail(ConfirmMailOperation action, string userName, string confirmationToken)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (string.IsNullOrEmpty(confirmationToken))
            {
                throw new ArgumentNullException("confirmationToken");
            }
            string content = null;
            switch (action)
            {
                case ConfirmMailOperation.confirm:
                    content = new Uri(base.Request.BaseServerAddress(), base.Url.Action("confirm", new { email = userName, token = confirmationToken })).ToString();
                    break;

                case ConfirmMailOperation.recovery:
                    content = new Uri(base.Request.BaseServerAddress(), base.Url.Action("resetpassword", new { token = confirmationToken })).ToString();
                    break;
            }
            UserToolsProvider.UmgRaiseMessage(UrlLanguage.CurrentLanguage, "Guest Service Registration", userName, "GS_REGCONFIRM", new XElement("guestServiceRegistration", new object[] { new XAttribute("action", action.ToString()), new XElement("confirmUrl", content), new XElement("email", userName) }).ToString());
        }

        private enum ConfirmMailOperation
        {
            confirm,
            recovery
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                this.Provider = provider;
                this.ReturnUrl = returnUrl;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(this.Provider, this.ReturnUrl);
            }

            public string Provider { get; private set; }

            public string ReturnUrl { get; private set; }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess
        }
    }
}

