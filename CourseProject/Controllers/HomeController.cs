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
        private readonly MarkRepository _markRepository;
        private readonly ComentRepository _comentRepository;
        private readonly LikeRepository _likeRepository;

        public HomeController(
            UserManager<ApplicationUser> userManager, 
            ArticleRepository articleRepository,
            ComentRepository comentRepository,
            MarkRepository markRepository,
            LikeRepository likeRepository)
        {
            _userManager = userManager;
            _articleRepository = articleRepository;
            _comentRepository = comentRepository;
            _markRepository = markRepository;
            _likeRepository= likeRepository;
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

        public async Task<IActionResult> SetLikeToComment(string articleId)//change to commentID
        { 
            var userId = (await GetCurrentUser()).Id;
            LikeModel like = new LikeModel() { AricleId = new Guid(articleId), UserId= new Guid(userId) };
            _likeRepository.Create(like);
            return RedirectPermanent("~/Home/PersonalArea");
        }

        public async Task<IActionResult> SetComment(string articleId, string text)
        {
            var userId = (await GetCurrentUser()).Id;
            ComentModel comment = new ComentModel() {
                Coment = text,
                Date = DateTime.Now,
                AricleId = new Guid(articleId),
                UserId = new Guid(userId)
            };
            _comentRepository.Create(comment);
            return RedirectPermanent("~/Home/PersonalArea");
        }
        

        public async Task<IActionResult> ArticleRead(Guid id)
        {
            var currentUser = await GetCurrentUser();
            //ComentModel coment = new ComentModel() { Coment = "comment1",Date = DateTime.Now, AricleId = id,UserId = new Guid(currentUser.Id)};

            //_comentRepository.Create(coment);
            // coment = new ComentModel() { Coment = "dfgdfg", Date = DateTime.Now, AricleId = id, UserId = new Guid(currentUser.Id) };
            //_comentRepository.Create(coment);
            // coment = new ComentModel() { Coment = "commehchnt1", Date = DateTime.Now, AricleId = id, UserId = new Guid(currentUser.Id) };
            //_comentRepository.Create(coment);
            // coment = new ComentModel() { Coment = "commenghfght1", Date = DateTime.Now, AricleId = id, UserId = new Guid(currentUser.Id) };
            //_comentRepository.Create(coment);
            // coment = new ComentModel() { Coment = "commengjfgjt1", Date = DateTime.Now, AricleId = id, UserId = new Guid(currentUser.Id) };
            //_comentRepository.Create(coment);


            ArticleModel article = _articleRepository.Get(id);
            IQueryable<MarkModel> marks = _markRepository.GetByArticleId(id);

            double rate=0;
            if (marks!=null && marks.Count()>0)
            {
                rate = marks.Select(p => p.Value).Average();
            }
            IQueryable<ComentModel> coments = _comentRepository.GetByArticleId(id);
            List<CommentViewModel> listComents = new List<CommentViewModel>();

           
            foreach (ComentModel i in coments)
            {
                var likes = _likeRepository.GetByCommentId(i.Id);
                listComents.Add(new CommentViewModel()
                {
                    Id = i.Id,
                    Date = i.Date,
                    Coment = i.Coment,
                    AricleId = i.AricleId,
                    UserId = i.UserId,
                    Likes = likes.Count(),
                    Name = (await _userManager.FindByIdAsync(i.UserId.ToString())).UserName
                });
            }


            ArticleReadViewModel model = new ArticleReadViewModel()
            {
                Id = article.Id,
                Data = article.Data,
                Description = article.Description,
                CreatedDate = article.CreatedDate,
                ModifitedDate = article.ModifitedDate,
                Specialty = article.Specialty,
                UserId = article.UserId,
                Name = article.Name,
                Rate = rate,
                Coments = listComents
            };

            return View(model);
        }
    }
}
