using CourseProject.Models;
using CourseProject.Services.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Filters
{
    public class SimpleActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));
            //Task<ApplicationUser> taskGetUser = userManager.FindByNameAsync(context.HttpContext.User.Identity.Name);
            //ApplicationUser user2 = taskGetUser.Result;
            //if (context.HttpContext.User.Identity.Name!=null)
            //{
            //}
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
