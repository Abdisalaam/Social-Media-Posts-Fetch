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
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
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
        // GET: Employee
        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;
            var models = ElaqatEmployee.GetAllEmployees();
            return View(models);
        }

        // GET: Employee/Details/5
        public ActionResult Details(string msg, int id)
        {
            ViewBag.Message = msg;
            var model = ElaqatEmployee.GetElaqatEmployeeDetails(id);
            return View(model);
        }

        // GET: Employee/Create
        public ActionResult Create(string msg)
        {
            ViewBag.Message = msg;
            var model = ElaqatEmployee.PrepareElaqatEmployeeToBeCreated();
            return View(model);
        }

        // POST: Employee/Create
        [HttpPost]
        public async Task<ActionResult> Create(EmployeeVM elaqatEmployeeVM)
        {
            try
            {
                var userExisit = await UserManager.FindByNameAsync(elaqatEmployeeVM.Email);
                if (userExisit != null)
                {
                    string msg = Resources.RegisterPage.UserEmailExist;
                    ViewBag.Message = msg;
                    var model = ElaqatEmployee.PrepareElaqatEmployeeToBeCreated();
                    return View(model);
                }
                var user = new ApplicationUser { UserName = elaqatEmployeeVM.Email, Email = elaqatEmployeeVM.Email };
                var result = await UserManager.CreateAsync(user, elaqatEmployeeVM.Password);
                if (result.Succeeded)
                {
                    var _role = "Employee";
                    if (elaqatEmployeeVM.SelectedRole_ID == 1)
                    {
                        _role = "Admin";
                    }
                    if (elaqatEmployeeVM.SelectedRole_ID == 4)
                    {
                        _role = "Observer";

                    }
                    UserManager.AddToRole(user.Id, _role);
                    elaqatEmployeeVM.EmployeeRole = _role;
                    elaqatEmployeeVM.EmployeeRegisterID = user.Id;
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var confirmEmail = await UserManager.ConfirmEmailAsync(user.Id, code);
                    var model = ElaqatEmployee.AddNewElaqatEmployee(elaqatEmployeeVM);
                    return RedirectToAction("Details", new { id = model.Id });
                }
                else
                {
                    string msg = Resources.RegisterPage.RegisterationIsNotSuccessfull;
                    ViewBag.Message = msg;
                    var model = ElaqatEmployee.PrepareElaqatEmployeeToBeCreated();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                string msg = Resources.RegisterPage.TechnicalError + "" + ex.Message;
                ViewBag.Message = msg;
                var model = ElaqatEmployee.PrepareElaqatEmployeeToBeCreated();
                return View(model);
            }
        }



        [AllowAnonymous]
        public ActionResult CreateELaqatMasterAdmin(string msg)
        {
            ViewBag.Message = msg;
            var model = ElaqatEmployee.PrepareElaqatEmployeeToBeCreated();
            model.SelectedRole_ID = 1;
            return View(model);
        }

        // POST: Employee/Create
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateELaqatMasterAdmin(EmployeeVM elaqatEmployeeVM)
        {
            try
            {
                var userExisit = await UserManager.FindByNameAsync(elaqatEmployeeVM.Email);
                if (userExisit != null)
                {
                    string msg = Resources.RegisterPage.UserEmailExist;
                    ViewBag.Message = msg;
                    var model = ElaqatEmployee.PrepareElaqatEmployeeToBeCreated();
                    return View(model);
                }
                var user = new ApplicationUser { UserName = elaqatEmployeeVM.Email, Email = elaqatEmployeeVM.Email };
                var result = await UserManager.CreateAsync(user, elaqatEmployeeVM.Password);
                if (result.Succeeded)
                {
                    var _role = "Admin";
                    if (elaqatEmployeeVM.SelectedRole_ID == 3)
                    {
                       
                        _role = "Employee";
                    }
                    if (elaqatEmployeeVM.SelectedRole_ID == 4)
                    {
                        _role = "Observer";

                    }
                    UserManager.AddToRole(user.Id, _role);
                    elaqatEmployeeVM.EmployeeRole = _role;
                    elaqatEmployeeVM.EmployeeRegisterID = user.Id;
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var confirmEmail = await UserManager.ConfirmEmailAsync(user.Id, code);
                    var model = ElaqatEmployee.AddNewElaqatEmployee(elaqatEmployeeVM);
                    return RedirectToAction("Details", new { id = model.Id });
                }
                else
                {
                    string msg = Resources.RegisterPage.RegisterationIsNotSuccessfull;
                    ViewBag.Message = msg;
                    var model = ElaqatEmployee.PrepareElaqatEmployeeToBeCreated();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                string msg = Resources.RegisterPage.TechnicalError + "" + ex.Message;
                ViewBag.Message = msg;
                var model = ElaqatEmployee.PrepareElaqatEmployeeToBeCreated();
                return View(model);
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(string msg, int id)
        {
            ViewBag.Message = msg;
            var model = ElaqatEmployee.PrepareElaqatEmployeeToBeEdited(id);
            return View(model);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeVM elaqatEmployeeVM)
        {
            try
            {
                var _role = "Employee";
                if (elaqatEmployeeVM.SelectedRole_ID == 1)
                {
                    _role = "Admin";
                }
                if (elaqatEmployeeVM.SelectedRole_ID == 4)
                {
                    _role = "Observer";

                }
               
                elaqatEmployeeVM.EmployeeRole = _role; 
                var model = ElaqatEmployee.UpdateElaqatEmployee(elaqatEmployeeVM);
                return RedirectToAction("Details", new { id = model.Id });
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                var model = ElaqatEmployee.PrepareElaqatEmployeeToBeEdited(elaqatEmployeeVM.Id);
                return View(model);
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
            var employee = ElaqatEmployee.GetElaqatEmployeeDetails(id);
            var model = new ChangePasswordVM
            {
                EmployeeID = employee.Id,
                Email = employee.EmployeeEmail,
                EmployeeName = employee.EmployeeName
            };
            return View(model);
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordVM model)
        {
            // User.Identity.Name or User.IsInRole("Administrator").           

            var elaqatEmployee = ElaqatEmployee.GetElaqatEmployeeDetails(model.EmployeeID);
            var user = await UserManager.FindByNameAsync(elaqatEmployee.EmployeeEmail);
            if (user == null || elaqatEmployee == null)
            {
                // Don't reveal that the user does not exist or is not confirmed

                ViewBag.Message = Resources.LoginPage.UserEmailNotExist;

                return View(model);
            }
            if (user.Id == elaqatEmployee.EmployeeRegisterID)
            {
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var result = await UserManager.ResetPasswordAsync(user.Id, code, model.NewPassword);

                if (result.Succeeded)
                {
                    var msg = Resources.LoginPage.PasswordHasBeenSuccessfullyReseted;
                    var elaqatClient = ElaqatEmployee.ElaqatEmployeePasswordISReseted(elaqatEmployee.Id);

                    return RedirectToAction("Details", new { msg, id = elaqatEmployee.Id });

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
