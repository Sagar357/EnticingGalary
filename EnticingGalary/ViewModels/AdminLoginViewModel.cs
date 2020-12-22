using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnticingGalary.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailId { get; set; }       

        [Required(ErrorMessage = "Please enter old password...")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Please enter new password...")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string AdminPwd { get; set; }


        [Required(ErrorMessage = "Please confirm your password...")]
        [DataType(DataType.Password)]
        [Compare("AdminPwd", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmAdminPwd{ get; set; }
    }
}