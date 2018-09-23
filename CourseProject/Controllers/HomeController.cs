using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProject.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using CourseProject.Services.Repositories;
using Microsoft.AspNetCore.Identity;
using CourseProject.Models.ViewModels;

namespace CourseProject.Controllers
{
    public class HomeController : Controller
    {
        private ArticleRepository _articleRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            UserManager<ApplicationUser> userManager, 
            ArticleRepository articleRepository)
        {
            _userManager = userManager;
            _articleRepository = articleRepository;

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> PersonalArea()
        {
            IQueryable<ArticleModel> Articles;
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (currentUser!=null)
            {
                Articles = _articleRepository.GetUserArticle(new Guid(currentUser.Id));
                return View(Articles);

            }
            return View();
        }



        public async Task<IActionResult> CreateArticle(ArticleDetailsViewModel article)
        {


        }



    }
}
