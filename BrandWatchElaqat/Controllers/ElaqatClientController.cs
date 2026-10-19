using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BrandWatchElaqat.Models;
using BrandWatchElaqat.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace BrandWatchElaqat.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    public class ElaqatClientController : Controller
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
        // GET: ElaqatClient
        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;
            var models = ElaqatClient.GetAllClient();
            return View(models);
        }

        // GET: ElaqatClient/Details/5
        public ActionResult Details(string msg, int id)
        {
            ViewBag.Message = msg;
            var model = ElaqatClient.GetElaqatClientDetails(id);
            return View(model);
        }

        // GET: ElaqatClient/Create
        public ActionResult Create(string msg)
        {
            ViewBag.Message = msg;
            var clientModel = ElaqatClient.PrepareElaqatClientToBeCreated();
            return View(clientModel);
        }
    
        // POST: ElaqatClient/Create
        [HttpPost]
        public async Task<ActionResult> Create(ElaqatClientVM elaqatClientVM)
        {
            try
            {
                var userExisit = await UserManager.FindByNameAsync(elaqatClientVM.Email);
                if (userExisit != null)
                {
                    string msg = Resources.RegisterPage.UserEmailExist;
                    ViewBag.Message = msg;
                    var clientModel = ElaqatClient.PrepareElaqatClientToBeCreated();
                    return View(clientModel);
                }
                var user = new ApplicationUser { UserName = elaqatClientVM.Email, Email = elaqatClientVM.Email };
                var result = await UserManager.CreateAsync(user, elaqatClientVM.Password);

                if (result.Succeeded)
                    {
                        var _role = "Client";
                        UserManager.AddToRole(user.Id, _role);
                        elaqatClientVM.ClientRole = _role;
                        elaqatClientVM.ClientRegisterID = user.Id;
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var confirmEmail = await UserManager.ConfirmEmailAsync(user.Id, code);
                       var model = ElaqatClient.AddNewElaqatClient(elaqatClientVM);
                        return RedirectToAction("Details", new { id = model.Id });

                }
                else
                {
                    string msg = Resources.RegisterPage.RegisterationIsNotSuccessfull;
                    ViewBag.Message = msg;
                    var clientModel = ElaqatClient.PrepareElaqatClientToBeCreated();
                    return View(clientModel);
                }

            }
            catch(Exception ex)
            {
                string msg = Resources.RegisterPage.TechnicalError + "" + ex.Message;
                ViewBag.Message = msg;
                var clientModel = ElaqatClient.PrepareElaqatClientToBeCreated();
                return View(clientModel);
            }
        }

        // GET: ElaqatClient/Edit/5
        public ActionResult Edit(string msg, int id)
        {
            ViewBag.Message = msg;
            var model = ElaqatClient.PrepareElaqatClientToBeEdit(id);
            return View(model);
        }

        // POST: ElaqatClient/Edit/5
        [HttpPost]
        public ActionResult Edit(ElaqatClientVM elaqatClientVM)
        {
            try
            {
                var model = ElaqatClient.EditElaqatClient(elaqatClientVM);

                return RedirectToAction("Details", new { id = model.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: ElaqatClient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ElaqatClient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ChangePassword(string msg, int id)
        {
            ViewBag.Message = msg;
            var client = ElaqatClient.GetElaqatClientDetails(id);
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
                    var msg = Resources.LoginPage.PasswordHasBeenSuccessfullyReseted;
                    var elaqatClient = ElaqatClient.ElaqatClientPasswordISReseted(elaqatUser.Id);

                    return RedirectToAction("Details", new {msg, id = elaqatUser.Id });

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
