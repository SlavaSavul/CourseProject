using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "NameLengthError")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [EmailAddress(ErrorMessage = "EmailError")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(100, ErrorMessage = "PasswordError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "ConfirmPasswordError")]
        public string ConfirmPassword { get; set; }
    }
}
