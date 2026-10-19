using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandWatchElaqat.Models
{
    public class RegisterationModel
    {
        public static RegisterViewModel PrepareRegisterViewModel()
        {
            var allRoless = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).ToList();
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel._RoleList = new SelectList(allRoless, "Id", "Name");
            registerViewModel.SelectedRole_ID = Convert.ToInt32(allRoless.FirstOrDefault().Id);
            return registerViewModel;

        }
    }
}