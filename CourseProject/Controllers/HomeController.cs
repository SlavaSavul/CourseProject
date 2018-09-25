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
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

namespace CourseProject.Controllers
{
    public class HomeController : Controller
    {
        private ArticleRepository _articleRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MarkRepository _markRepository;
        private readonly ComentRepository _comentRepository;
        private readonly LikeRepository _likeRepository;
        private readonly TagRepository _tagRepository;
        private readonly ArticleTagRepository _articleTagRepository;

        public HomeController(
            UserManager<ApplicationUser> userManager, 
            ArticleRepository articleRepository,
            ComentRepository comentRepository,
            MarkRepository markRepository,
            LikeRepository likeRepository,
            TagRepository tagRepository,
            ArticleTagRepository articleTagRepository)
        {
            _userManager = userManager;
            _articleRepository = articleRepository;
            _comentRepository = comentRepository;
            _markRepository = markRepository;
            _likeRepository= likeRepository;
            _tagRepository = tagRepository;
            _articleTagRepository = articleTagRepository;

        }


        public IActionResult Index()
        {
            IEnumerable<ArticleModel> articles = _articleRepository.GetAll();
            List<ArticleListViewModel> articlesList = new List<ArticleListViewModel>();
            foreach (ArticleModel article in articles)
            {
                articlesList.Add(new ArticleListViewModel()
                {
                    Name = article.Name,
                    Description = article.Description,
                    Specialty = article.Specialty,
                    Id = article.Id
                });
            }
            return View(articlesList);
        }

        /*public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }*/
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

        /*public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }*/

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> PersonalArea()
        {
            IQueryable<ArticleModel> articles;
            List<ArticleListViewModel> articlesList = new List<ArticleListViewModel>();
            var currentUser = await GetCurrentUser();
            if (currentUser != null)
            {
                articles = _articleRepository.GetUserArticle(new Guid(currentUser.Id));
                foreach (ArticleModel article in articles)
                {
                    articlesList.Add(new ArticleListViewModel()
                    {
                        Name = article.Name,
                        Description = article.Description,
                        Specialty = article.Specialty,
                        Id = article.Id
                    });
                }
            }
            return View(articlesList);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }

        [Authorize]
        [HttpPost]
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

        [Authorize]
        [HttpPost]
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
           /* string tags = article.Tags.Trim();
            tags = Regex.Replace(tags, " +", " ");
            foreach (string i in tags.Split(' '))
            {
                TagModel tag = new TagModel() { Title = i };
                _tagRepository.Create(tag);
                _articleTagRepository.Create(new ArticleTagModel() { ArticleId =model.Id, TagsId=tag.Id });

            }*/
            return RedirectPermanent("~/Home/PersonalArea");
        }

        [Authorize]
        public IActionResult RenderCreateArticle()
        {
            return View(new ArticleDetailsViewModel());
        }

        [Authorize]
        public async Task<IActionResult> ArticleEditor(string id)
        {
            var userId = (await GetCurrentUser()).Id;
            ArticleModel article = _articleRepository.Get(new Guid(id));
            if (article!=null && article.UserId == new Guid(userId))
            {
                ArticleDetailsViewModel model = new ArticleDetailsViewModel()
                {
                    Data = article.Data,
                    Description = article.Description,
                    Specialty = article.Specialty,
                    Name = article.Name,
                    UserId = article.UserId,
                    Id = article.Id,
                    CreatedDate = article.CreatedDate,
                    ModifitedDate = article.ModifitedDate
                };
                return View(model);
            }
            return RedirectPermanent("~/Home/Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteArticle(string id)
        {
            ArticleModel article = _articleRepository.Get(new Guid(id));
            var userId = (await GetCurrentUser()).Id;
            if (article != null && article.UserId==new Guid(userId))
            {
                _articleRepository.Delete(new Guid(id));
            }
            return RedirectPermanent("~/Home/PersonalArea");
        }

        [Authorize]
        public async Task<bool> SetLikeToComment(string id)
        {
            var userId = (await GetCurrentUser()).Id;
            LikeModel likeModel =_likeRepository.Get(new Guid(id), new Guid(userId));
            CommentModel commentModel = _comentRepository.Get(new Guid(id));
            if (likeModel == null && commentModel.UserId != new Guid(userId))
            {
                LikeModel like = new LikeModel()
                {
                    CommentId = new Guid(id),
                    UserId = new Guid(userId)
                };
                _likeRepository.Create(like);
                return true;
            }
            return false;
        }

        [Authorize]
        public async Task<bool> SetComment(string articleId, string text)
        {
            var userId = (await GetCurrentUser()).Id;
            CommentModel comment = new CommentModel()
            {
                Comment = text,
                Date = DateTime.Now,
                AricleId = new Guid(articleId),
                UserId = new Guid(userId)
            };
            _comentRepository.Create(comment);
            return true;
        }

        [Authorize]
        public async Task<bool> SetRate(string articleId, int rate)
        {
            var userId = (await GetCurrentUser()).Id;
            MarkModel mark = _markRepository.Get(new Guid(articleId), new Guid(userId));
            ArticleModel article = _articleRepository.Get(new Guid(articleId));
            if (mark == null && article.UserId!=new Guid(userId))
            {
                MarkModel newMark = new MarkModel()
                {
                    UserId = new Guid(userId),
                    AricleId = new Guid(articleId),
                    Value = rate
                };
                _markRepository.Create(newMark);
                return true;
            }
            return false;
        }

        [Authorize]
        public async Task<IActionResult> ArticleRead(Guid id)
        {
            var currentUser = await GetCurrentUser();

            ArticleModel article = _articleRepository.Get(id);
            ApplicationUser articleUser = await _userManager.FindByIdAsync(article.UserId.ToString());
            IQueryable<MarkModel> articleMarcks = _markRepository.GetByArticleId(id);
            double rate=0;
            if (articleMarcks != null && articleMarcks.Count()>0)
            {
                rate = articleMarcks.Select(p => p.Value).Average();
            }

            IQueryable<CommentModel> coments = _comentRepository.GetByArticleId(id);
            List<CommentViewModel> listViewComents = new List<CommentViewModel>();
            foreach (CommentModel i in coments)
            {
                var likes = _likeRepository.GetByCommentId(i.Id);
                listViewComents.Add(new CommentViewModel()
                {
                    Id = i.Id,
                    Date = i.Date,
                    Coment = i.Comment,
                    AricleId = i.AricleId,
                    UserId = i.UserId,
                    Likes = likes.Count(),
                    Name = (await _userManager.FindByIdAsync(i.UserId.ToString())).UserName
                });
            }

            ArticleReadViewModel viewModel = new ArticleReadViewModel()
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
                Coments = listViewComents,
                UserName= articleUser.UserName
            };

            return View(viewModel);
        }
    }
}
