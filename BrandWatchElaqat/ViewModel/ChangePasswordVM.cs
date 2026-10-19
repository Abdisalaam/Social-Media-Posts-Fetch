using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BrandWatchElaqat.ViewModel
{
    public class ChangePasswordVM
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "EmailIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "EmailStructureIsNotCorrect")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "CurrentPasswordIsRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "NewPassword")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+=\[{\]};:<>|./?,-]).{8,}$",
         ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "PasswordRequirementsMessage")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "ConfirmNewPassword")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "PasswordAndPasswordConfirmationDontMatch")]
        public string ConfirmNewPassword { get; set; }
    }
}