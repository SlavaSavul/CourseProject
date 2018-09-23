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
            var currentUser = await GetCurrentUser();
            if (currentUser != null)
            {
                Articles = _articleRepository.GetUserArticle(new Guid(currentUser.Id));
                IEnumerable<ArticleListViewModel> articlesList = GetArticlesList(Articles);

                return View(articlesList);

            }
            return View();
        }
        private IEnumerable<ArticleListViewModel> GetArticlesList(IQueryable<ArticleModel> articles)
        {
            List<ArticleListViewModel> articlesList=new List<ArticleListViewModel>();
            foreach (ArticleModel article in articles)
            {
                articlesList.Add(new ArticleListViewModel() 
                {
                    Name= article.Name,
                    Description= article.Description,
                    Specialty= article.Specialty,
                    Id= article.Id
                });
            }
            return articlesList;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }


        public IActionResult SaveUpdatedArticle(ArticleDetailsViewModel article)
        {
            ArticleModel model = new ArticleModel()
            {
                Data = article.Data,
                CreatedDate = article.CreatedDate,
                UserId = article.UserId,
                ModifitedDate = DateTime.Now,
                Id = article.Id,
                Description = article.Description,
                Specialty = article.Specialty,
                Name = article.Name
            };
            _articleRepository.Update(model);
            return RedirectPermanent("~/Home/PersonalArea");
        }

        public async Task<IActionResult> CreateArticle(ArticleDetailsViewModel article)
        {
            var currentUser = await GetCurrentUser();
            ArticleModel model = new ArticleModel()
            {
                Data = article.Data,
                CreatedDate = DateTime.Now,
                Description = article.Description,
                Specialty = article.Specialty,
                Name = article.Name,
                UserId = new Guid(currentUser.Id)
            };
            _articleRepository.Create(model);
            return RedirectPermanent("~/Home/PersonalArea");
        }

        public IActionResult RenderCreateArticle()
        {
            return View(new ArticleDetailsViewModel());
        }

        public IActionResult ArticleEditor(string id)
        {
            ArticleModel article = _articleRepository.Get(new Guid(id));
            ArticleDetailsViewModel model = new ArticleDetailsViewModel()
            {
                Data = article.Data,
                Description = article.Description,
                Specialty = article.Specialty,
                Name = article.Name,
                UserId = article.UserId,
                Id= article.Id,
                CreatedDate= article.CreatedDate,
                ModifitedDate= article.ModifitedDate
            };
            return View(model);
        }
        public IActionResult DeleteArticle(string id)
        {
            _articleRepository.Delete(new Guid(id));
            return RedirectPermanent("~/Home/PersonalArea");
        }


    }
}
