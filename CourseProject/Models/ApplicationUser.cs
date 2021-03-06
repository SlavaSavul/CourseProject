﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Style { get; set; }
        public string Language { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
