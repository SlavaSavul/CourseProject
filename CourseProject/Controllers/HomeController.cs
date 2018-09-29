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
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

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
        private readonly IHubContext<ChatHub> _hubContext;


        public HomeController(
            UserManager<ApplicationUser> userManager,
            ArticleRepository articleRepository,
            ComentRepository comentRepository,
            MarkRepository markRepository,
            LikeRepository likeRepository,
            TagRepository tagRepository,
            ArticleTagRepository articleTagRepository,
            IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
            _userManager = userManager;
            _articleRepository = articleRepository;
            _comentRepository = comentRepository;
            _markRepository = markRepository;
            _likeRepository = likeRepository;
            _tagRepository = tagRepository;
            _articleTagRepository = articleTagRepository;
        }

        public IActionResult Index()
        {
            List<ArticleListViewModel> articles = new List<ArticleListViewModel>();
            foreach (ArticleModel article in _articleRepository.GetAll())//not all
            {
                articles.Add(new ArticleListViewModel()
                {
                    Name = article.Name,
                    Description = article.Description,
                    Specialty = article.Specialty,
                    Id = article.Id,
                    ModifitedDate = article.ModifitedDate,
                    Rate = GetAverageRate(article.Marks)
                });
            }
            MainPageViewModel model = new MainPageViewModel();
            model.LatestModified= articles.OrderByDescending(a => a.ModifitedDate).Take(5).ToList();
            model.TopRating = articles.OrderByDescending(a => a.Rate).Take(5).ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            ApplicationUser user = await GetCurrentUser();
            user.Language = culture;
            await _userManager.UpdateAsync(user);
            return LocalRedirect(returnUrl);
        }

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

        [NonAction]
        private async Task<ApplicationUser> GetCurrentUser()
        {
            if (User.Identity.Name != null)
            {
                return await _userManager.FindByNameAsync(User.Identity.Name);
            }
            return null;
        }

        [Authorize]
        [HttpPost]
        public IActionResult SaveUpdatedArticle(ArticleDetailsViewModel changedArticle)
        {
            ArticleModel article = _articleRepository.Get(changedArticle.Id);
            article.Data = changedArticle.Data;
            article.Description = changedArticle.Description;
            article.Specialty = changedArticle.Specialty;
            article.Name = changedArticle.Name;
            article.ModifitedDate = DateTime.Now;
            article.Tags = new List<ArticleTagModel>();
            if (changedArticle.Tags != null)
            {
                foreach (string item in changedArticle.Tags)
                {
                    TagModel tag = new TagModel() { Title = item };
                    _tagRepository.Create(tag);
                    article.Tags.Add(new ArticleTagModel()
                    {
                        ArticleId = article.Id,
                        TagId = tag.Id
                    });

                }
            }
            _articleRepository.Update(article);


            return RedirectPermanent("~/Home/PersonalArea");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleDetailsViewModel article)
        {
            var currentUser = await GetCurrentUser();
            List<ArticleTagModel> articleTags = new List<ArticleTagModel>();
            if (article.Tags!=null)
            {
                foreach (string item in article.Tags)
                {
                    TagModel tag = new TagModel() { Title = item };
                    _tagRepository.Create(tag);
                    articleTags.Add( new ArticleTagModel()
                    {
                        ArticleId = article.Id,
                        TagId = tag.Id
                    });

                }
            }
            ArticleModel model = new ArticleModel()
            {
                Data = article.Data,
                CreatedDate = DateTime.Now,
                Description = article.Description,
                Specialty = article.Specialty,
                Name = article.Name,
                UserId = new Guid(currentUser.Id),
                Tags= articleTags
            };
            _articleRepository.Create(model);
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
            if (article != null && article.UserId == new Guid(userId))
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
                    ModifitedDate = article.ModifitedDate,
                    Tags = article.Tags.Select(t => t.Tag.Title).ToList()
                };
                return View(model);
            }
            return RedirectPermanent("~/Home/Index");
        }

        [Authorize]
        //[HttpPost]
        public async Task<IActionResult> DeleteArticle(string id)
        {
            ArticleModel article = _articleRepository.Get(new Guid(id));
            //delete tags
            var userId = (await GetCurrentUser()).Id;
            if (article != null && article.UserId == new Guid(userId))
            {
                _articleRepository.Delete(new Guid(id));
            }
            return RedirectPermanent("~/Home/PersonalArea");
        }

        [Authorize]
        public async Task SetLikeToComment(string id)
        {
            var userId = (await GetCurrentUser()).Id;
            LikeModel likeModel = _likeRepository.Get(new Guid(id), new Guid(userId));
            CommentModel commentModel = _comentRepository.Get(new Guid(id));
            if (likeModel == null && commentModel.UserId != new Guid(userId))
            {
                LikeModel like = new LikeModel()
                {
                    CommentId = new Guid(id),
                    UserId = new Guid(userId)
                };
                commentModel.Likes.Add(like);
                _likeRepository.Update(like);
            }
        }

        [Authorize]
        public async Task SetComment(string articleId, string text)
        {
            var userId = (await GetCurrentUser()).Id;
            CommentModel comment = new CommentModel()
            {
                Comment = text,
                Date = DateTime.Now,
                ArticleId = new Guid(articleId),
                UserId = new Guid(userId)
            };
            ArticleModel article = _articleRepository.Get(new Guid(articleId));
            article.Comments.Add(comment);
            _articleRepository.Update(article);
            await SendComment(comment);
        }

        [NonAction]
        private async Task SendComment(CommentModel comment)
        {
            CommentViewModel commentForSend = new CommentViewModel()
            {
                Comment = comment.Comment,
                Date = comment.Date.ToString(),
                ArticleId = comment.ArticleId,
                UserId = comment.UserId,
                Name = User.Identity.Name,
                Likes = 0
            };
            await _hubContext.Clients.All.SendAsync("SendComment", commentForSend);
        }
        
        [Authorize]
        public async Task SetRate(string articleId, int rate)
        {
            var userId = (await GetCurrentUser()).Id;
            MarkModel mark = _markRepository.Get(new Guid(articleId), new Guid(userId));
            ArticleModel article = _articleRepository.Get(new Guid(articleId));
            if (mark == null && article.UserId != new Guid(userId))
            {
                MarkModel newMark = new MarkModel()
                {
                    UserId = new Guid(userId),
                    ArticleId = new Guid(articleId),
                    Value = rate
                };
                article.Marks.Add(newMark);
                _articleRepository.Update(article);
            }
        }

        public async Task<IActionResult> ArticleRead(Guid id)
        {
            var currentUser = await GetCurrentUser();
            ArticleModel article = _articleRepository.Get(id);
            ApplicationUser articleUser =
                await _userManager.FindByIdAsync(article.UserId.ToString()) ?? new ApplicationUser();//??????????????
            double rate = GetAverageRate(article.Marks);
            List<CommentViewModel> listViewComments = new List<CommentViewModel>();
            foreach (CommentModel i in article.Comments)
            {
                var likes = _likeRepository.GetByCommentId(i.Id);
                listViewComments.Add(new CommentViewModel()
                {
                    Id = i.Id,
                    Date = i.Date.ToString(),
                    Comment = i.Comment,
                    ArticleId = i.ArticleId,
                    UserId = i.UserId,
                    Likes = likes.Count(),
                    Name = (await _userManager.FindByIdAsync(i.UserId.ToString())).UserName
                });
            }
            List<CommentViewModel> orderedListViewComents = listViewComments
                .OrderByDescending(c => c.Date)
                .ToList();
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
                Comments = orderedListViewComents,
                UserName = articleUser.UserName,  //если пользователь удален
                Tags = article.Tags.Select(t => t.Tag.Title).ToList()
            };
            return View(viewModel);
        }

        [NonAction]
        private double GetAverageRate(List<MarkModel> marks)
        {
            double rate = 0;
            if (marks != null && marks.Count() > 0)
            {
                rate = marks.Select(p => p.Value).Average();
            }
            return rate;
        }

    }
}
