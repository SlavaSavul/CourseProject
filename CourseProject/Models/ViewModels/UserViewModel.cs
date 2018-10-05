using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "RegistrationDate")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Lockout")]
        public bool Lockout { get; set; }

        public string Id { get; set; }

        [Display(Name = "Role")]
        public IList<string> Role { get; set; }
        public bool IsAdmin { get; set; }
    }
}
