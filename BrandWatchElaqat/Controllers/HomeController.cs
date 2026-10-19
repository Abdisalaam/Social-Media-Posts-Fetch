using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BrandWatchElaqat.Models;
using BrandWatchElaqat.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;

namespace BrandWatchElaqat.Controllers
{
    [Authorize(Roles = "Employee,Admin,Client")]

    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;
            string userId = User.Identity.GetUserId();
            var client = ElaqatClient.GetElaqatClientByRegisterID(userId);
            return View(client);
        }

        [HttpGet]
        public ActionResult ChangePassword(string msg, int clientId)
        {
            ViewBag.Message = msg;
            var client = ElaqatClient.GetElaqatClientDetails(clientId);
            var model = new ChangePasswordVM();
            model.ClientID = client.Id;
            model.Email = client.ClientEmail;
            model.ClientName = client.ClientName;
            return View(model);
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordVM model)
        {
            // User.Identity.Name or User.IsInRole("Administrator").           

            var elaqatUser = ElaqatClient.GetElaqatClientDetails(model.ClientID);
            var user = await UserManager.FindByNameAsync(elaqatUser.ClientEmail);
            if (user == null || elaqatUser == null)

            {
                // Don't reveal that the user does not exist or is not confirmed

                ViewBag.Message = Resources.LoginPage.UserEmailNotExist;

                return View(model);
            }
            if (user.Id == elaqatUser.ClientRegisterID)
            {
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var result = await UserManager.ResetPasswordAsync(user.Id, code, model.NewPassword);

                if (result.Succeeded)

                {

                    //var elaqatClient = ElaqatClient.GetElaqatClientByRegisterIdAndEmail(user.UserName, user.Id);

                    return RedirectToAction("Details", new { id = elaqatUser.Id });

                }

            }
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link



            ViewBag.Message = Resources.RegisterPage.TechnicalError;

            return View(model);

            // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

            // await UserManager.SendEmailAsync(user.Id, Resources.LoginPage.PageTitleResetPassword, Resources.LoginPage.ToCreateNewPassword + "<a href=\"" + callbackUrl + "\">" + Resources.LoginPage.PleaseClickHere + "</a>");

            // return RedirectToAction("ForgotPasswordConfirmation", new { email = model.Email });


            // If we got this far, something failed, redisplay form


        }
    }
}