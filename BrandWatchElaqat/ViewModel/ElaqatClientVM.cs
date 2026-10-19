using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandWatchElaqat.ViewModel
{
    public class ElaqatClientVM
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "CLientNameIsRequired")]
        public string ClientName { get; set; }
        public string CompanyName { get; set; }

        [Phone]
        [MaxLength(10, ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "PhoneNumberShouldBeTenDigits")]
        [MinLength(10, ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "PhoneNumberShouldBeTenDigits")]
        [RegularExpression(@"^\d{10}$", ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "PhoneNumberShouldBeTenDigits")]
        public string PhoneNumber { get; set; }
        public Nullable<System.DateTime> RegisterDate { get; set; }
        public string ClientRegisterID { get; set; }
        public string Adress { get; set; }
        public string ContactPersonName { get; set; }
        public string ClientRole { get; set; }       

        [Required(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "EmailIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "EmailStructureIsNotCorrect")]        
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "PasswordIsRequired")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+=\[{\]};:<>|./?,-]).{8,}$",
         ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "PasswordRequirementsMessage")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "ConfirmPasswordIsRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "PasswordAndPasswordConfirmationDontMatch")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.RegisterPage), ErrorMessageResourceName = "RoleIsRequired")]
        public int SelectedRole_ID { get; set; }
        public SelectList _RoleList { get; set; }
        public bool IsActive { get; set; }

    }
}