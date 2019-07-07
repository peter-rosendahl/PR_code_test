using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using PR_code_test.Models;
using System.Web.Security;
using Umbraco.Core.Models;
using Umbraco.Web.Security;
using Umbraco.Web;
using System.Net.Mail;
using System.Text.RegularExpressions;
using umbraco;
using System.Web.Profile;
using System.Text;
using System.Threading;

namespace PR_code_test.Controllers
{
    public class MemberController : BaseController
    {
        public ActionResult EnterLoginForm(MemberModel model)
        {
            ModelState.Clear();
            return PartialView("Member/LoginForm", model);
        }
        public ActionResult EnterSignUpForm(MemberModel model)
        {
            ModelState.Clear();
            return PartialView("Member/SignUpForm", model);
        }
        public ActionResult EnterForgotPasswordForm(MemberModel model)
        {
            ModelState.Clear();
            return PartialView("Member/ForgotPasswordForm", model);
        }

        [HttpPost]
        public ActionResult RenderLogin(MemberModel model)
        {
            ModelState.Clear();
            string errorMessage = "There was one or more errors in the sign in form. Please try again.";
            if (!LoginInputsAreValid(model))
            {
                return PartialView("Member/LoginForm", model);
            }

            if (Membership.ValidateUser(model.Email, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);

                int timeout = model.RememberMe ? 525600 : 30; // Timeout in minutes, 525600 = 365 days.
                var ticket = new FormsAuthenticationTicket(model.Email, model.RememberMe, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = System.DateTime.Now.AddMinutes(timeout);// Not my line
                cookie.HttpOnly = true; // cookie not available in javascript.
                Response.Cookies.Add(cookie);
            }
            else
            {
                ModelState.AddModelError("Email", "Invalid email or password");
                return PartialView("Member/LoginForm", model);
            }
            int? rootPageId = 1054;

            IPublishedContent landingPage = rootPageId != null ? Umbraco.TypedContent(rootPageId) : null;

            return RedirectToUmbracoPage(landingPage);

        }

        [HttpPost]
        public ActionResult RenderSignUp(MemberModel model)
        {
            ModelState.Clear();
            if (!SignUpInputsAreValid(model))
            {
                return PartialView("Member/SignUpForm", model);
            }

            try
            {
                MembershipUser member = Membership.GetUser(model.Email);

                if (member != null)
                {
                    ModelState.AddModelError("Email", library.GetDictionaryItem("Member already exists error message"));
                    return CurrentUmbracoPage();
                }
                else
                {
                    string existingMember = Membership.GetUserNameByEmail(model.Email);
                    if (!string.IsNullOrEmpty(existingMember))
                    {
                        ModelState.AddModelError("Email", library.GetDictionaryItem("Member already exists error message"));
                        return CurrentUmbracoPage();
                    }
                    MembershipUser newMember = Membership.CreateUser(model.Email, model.Password, model.Email);
                    newMember.IsApproved = true; // NOTE: If member has to activate account, remember to set this to false.
                    Membership.UpdateUser(newMember);

                    FormsAuthentication.SetAuthCookie(newMember.UserName, false);

                    // PR_comment: If the app is to send email to user with link to activate account, this code below should be inserted.
                    // But this requires an smtp mail setup in web.config to be made before this will work properly.
                    // Also: If member has to activate account, then we should build a method, that checks the url for the parameters like in below code, in order to approve the right member.

                    //member = Membership.GetUser(model.Email);
                    //if (member != null)
                    //{
                    //    StringBuilder mailBody = new StringBuilder();
                    //    mailBody.AppendLine($"Hi {model.Name}!");
                    //    mailBody.AppendLine();
                    //    mailBody.AppendLine($"Click on the link below to activate your account:");
                    //    mailBody.AppendLine();
                    //    IPublishedContent currentPage = GetCurrentPage();
                    //    IPublishedContent homePage = currentPage != null ? currentPage.AncestorOrSelf(1) : null;
                    //    string homePageUrl = homePage != null ? homePage.Url : string.Empty;
                    //    if (!string.IsNullOrEmpty(homePageUrl))
                    //    {
                    //        mailBody.AppendLine($"{homePageUrl}?a={member.UserName}&c=true");
                    //        mailBody.AppendLine();
                    //        mailBody.AppendLine($"If you don't recognize this appearance, you can ignore this mail.");
                    //        mailBody.AppendLine();
                    //        mailBody.AppendLine("Best wishes, ");
                    //        mailBody.AppendLine($"{homePage.CreatorName}");
                    //    }
                    //    MailMessage mailMessage = new MailMessage();
                    //    mailMessage.To.Add(member.UserName);
                    //    mailMessage.Subject = "Thank you for signing up at AppCMS - remember to activate your account";

                    //    mailMessage.Body = mailBody.ToString();

                    //    using (SmtpClient mailClient = new SmtpClient())
                    //    {
                    //        mailClient.Send(mailMessage);
                    //    }

                    //    FormsAuthentication.SignOut();
                    //}
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return PartialView("Member/SignUpForm", model);
            }

            return PartialView("Member/SuccesfullySignedUp");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RenderForgotPassword(MemberModel model)
        {
            CheckEmail(model.Email);
            ModelState.Remove("Password");

            if (!ModelState.IsValid)
            {
                return PartialView("Member/ForgotPasswordForm", model);
            }
            else
            {
                MembershipUser existingMember = Membership.GetUser(model.Email);
                if (existingMember == null)
                {
                    ModelState.AddModelError("Email", library.GetDictionaryItem("Member does not exist"));
                    return PartialView("Member/ForgotPasswordForm", model);
                }
                else
                {
                    // PR_comment: If the app is to send email to user with link to reset password, this code below should be inserted.
                    // But this requires an smtp mail setup in web.config to be made before this will work properly.

                    //StringBuilder mailBody = new StringBuilder();
                    //mailBody.AppendLine($"Hi {model.Name}!");
                    //mailBody.AppendLine();
                    //mailBody.AppendLine($"Click on the link below to reset your password:");
                    //mailBody.AppendLine();
                    //IPublishedContent currentPage = GetCurrentPage();
                    //IPublishedContent homePage = currentPage != null ? currentPage.AncestorOrSelf(1) : null;
                    //string homePageUrl = homePage != null ? homePage.Url : string.Empty;
                    //if (!string.IsNullOrEmpty(homePageUrl))
                    //{
                    //    mailBody.AppendLine($"{homePageUrl}?a={existingMember.UserName}&c=true");
                    //    mailBody.AppendLine();
                    //    mailBody.AppendLine($"If you don't recognize this appearance, you can ignore this mail.");
                    //    mailBody.AppendLine();
                    //    mailBody.AppendLine("Best wishes, ");
                    //    mailBody.AppendLine($"{homePage.CreatorName}");
                    //}
                    //MailMessage mailMessage = new MailMessage();
                    //mailMessage.To.Add(existingMember.UserName);
                    //mailMessage.Subject = "Reset your password at AppCMS";

                    //mailMessage.Body = mailBody.ToString();

                    //using (SmtpClient mailClient = new SmtpClient())
                    //{
                    //    mailClient.Send(mailMessage);
                    //}

                    return PartialView("Member/SuccessfullySentPasswordRenewalRequest");
                }
            }


            return PartialView("Member/ForgotPasswordForm", model);
        }

        public ActionResult LogoutView(MemberModel model)
        {
            MembershipUser member = Membership.GetUser();

            if (member != null)
            {
                MembershipHelper membershipHelper = new MembershipHelper(Umbraco.UmbracoContext);
                IPublishedContent username = membershipHelper.GetCurrentMember();
                if (username != null)
                {
                    string memberName = username.Name;
                    model.Name = memberName != null ? memberName : member.UserName;
                }

            }
            return PartialView("Member/Logout", model);
        }

        public ActionResult SubmitLogout()
        {
            TempData.Clear();
            Session.Clear();
            FormsAuthentication.SignOut();
            IPublishedContent homePage = CurrentPage.AncestorOrSelf(1);
            return RedirectToUmbracoPage(homePage.Id);
        }

        private bool LoginInputsAreValid(MemberModel model)
        {
            CheckEmail(model.Email);
            bool validPassword = IsPasswordValid(model.Password, "Password");

            return ModelState.IsValid;
        }

        private bool SignUpInputsAreValid(MemberModel model)
        {
            CheckName(model.Name);
            CheckEmail(model.Email);
            bool validPassword = IsPasswordValid(model.Password, "Password") && IsPasswordValid(model.RepeatPassword, "RepeatPassword");
            if (validPassword)
            {
                CombinePasswords(model.Password, model.RepeatPassword);
            }

            return ModelState.IsValid;
        }

        private void CheckName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("Name", library.GetDictionaryItem("Name empty error message"));
            }
        }

        private void CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("Email", library.GetDictionaryItem("Email empty error message"));
            }
            else
            {
                try
                {
                    MailAddress mailaddress = new MailAddress(email);
                }
                catch (Exception)
                {
                    ModelState.Remove("Email");
                    ModelState.AddModelError("Email", library.GetDictionaryItem("Email invalid error message"));
                }
            }
        }

        private bool IsPasswordValid(string password, string fieldName)
        {
            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(fieldName, "Password field cannot be empty");
                return false;
            }
            else
            {
                string AllowedChars = @"([a-zA-Z\d\S]{6,})";

                if (!Regex.IsMatch(password, AllowedChars))
                {
                    ModelState.AddModelError(fieldName, library.GetDictionaryItem("Password not strong enough error message"));
                    return false;
                }
            }
            return true;
        }

        private void CombinePasswords(string password1, string password2)
        {
            if (!password1.ToLower().Trim().Equals(password2.ToLower().Trim()))
            {
                ModelState.AddModelError("RepeatPassword", library.GetDictionaryItem("Password not identical error message"));
            }
        }
    }
}