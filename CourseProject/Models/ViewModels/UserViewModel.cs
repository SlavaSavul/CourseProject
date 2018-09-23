using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.ViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Lockout { get; set; }
        public string Id { get; set; }
        public IList<string> Role { get; set; }

    }
}
