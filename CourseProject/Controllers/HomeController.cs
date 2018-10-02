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
using CourseProject.Services;
using HeyRed.MarkdownSharp;

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
        private SearchService _searchService;


        public HomeController(
             SearchService searchService,
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
            _searchService = searchService;
        }

        //public JsonResult AutocompleteSearch(string term)
        //{
        //    IEnumerable<QueryModel> queries = _searchService.GetSearchQueries(term);
        //    var models = queries.Select(a => a.Query);
        //    return Json(models);
        //}

        public IActionResult SearchByKeyword(string keyword)
        {
         //   _searchService.Create(new QueryModel() { Query = keyword });
            IEnumerable<ArticleModel> searchResult = _searchService.GetIndexedArticles(keyword);
            List<ArticleListViewModel> articlesList = CreateArticleList(searchResult.ToList());
            return View("SearchResults",  articlesList);
        }

        public IActionResult SearchByHashtag(string hashtag)
        {
            IEnumerable<ArticleModel> searchResult = _searchService.GetByHashtag(hashtag);
            List<ArticleListViewModel> articleLists = new List<ArticleListViewModel>();
            if (searchResult != null)
            {
                articleLists = CreateArticleList(searchResult.ToList());
            }
            return View("SearchResults", articleLists);
        }

        public IActionResult Index()
        {
            List<ArticleModel> ratingArticles = _articleRepository.GetWithMarks(5);
            List<ArticleModel> lastModifiedArticles = _articleRepository.GetLastModifited(5);
            MainPageViewModel model = new MainPageViewModel();
            model.LatestModified = CreateArticleList(
                lastModifiedArticles);
            model.TopRating = CreateArticleList(
               ratingArticles);
            List<TagViewModel> tags = new List<TagViewModel>();
            foreach (TagModel tag in _tagRepository.GetAllTags())
            {
                tags.Add(new TagViewModel() { Id=tag.Id,Title=tag.Title,Width=tag.ArticleTags.Count()});
            }
            model.Tags = tags;
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
                        Speciality = article.Speciality,
                        Id = article.Id
                    });
                }
            }
            return View(articlesList);
        }
      
        [Authorize]
        [HttpPost]
        public String SaveUpdatedArticle(ArticleDetailsViewModel updatedArticle)
        {
            ArticleModel article = _articleRepository.Get(updatedArticle.Id);
            article.Data = updatedArticle.Data;
            article.Description = updatedArticle.Description;
            article.Speciality = updatedArticle.Speciality;
            article.Name = updatedArticle.Name;
            article.ModifitedDate = DateTime.Now;
            article.Tags = CreateArticleTagList(updatedArticle.Tags, updatedArticle.Id);
            _articleRepository.Update(article);
            return "/Home/PersonalArea";
        }

        [Authorize]
        [HttpPost]
        public async Task<String> CreateArticle(ArticleDetailsViewModel article)
        {
            var currentUser = await GetCurrentUser();
            ArticleModel model = new ArticleModel()
            {
                Data = article.Data,
                CreatedDate = DateTime.Now,
                ModifitedDate= DateTime.Now,
                Description = article.Description,
                Speciality = article.Speciality,
                Name = article.Name,
                UserId = new Guid(currentUser.Id),
                Tags= CreateArticleTagList(article.Tags, article.Id)
            };
            _articleRepository.Create(model);
            return "/Home/PersonalArea";
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
                    Speciality = article.Speciality,
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
            CommentModel comment = _comentRepository.Get(new Guid(id));
            LikeModel likeToThisComment = comment.Likes
                .FirstOrDefault(l => l.UserId == new Guid(userId));
            if (likeToThisComment == null && comment.UserId != new Guid(userId))
            {
                LikeModel like = new LikeModel()
                {
                    CommentId = new Guid(id),
                    UserId = new Guid(userId)
                };
                comment.Likes.Add(like);
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

        [Authorize]
        public async Task SetRate(string articleId, int rate)
        {
            var userId = (await GetCurrentUser()).Id;
            ArticleModel article = _articleRepository.Get(new Guid(articleId));
            MarkModel userMark = article.Marks
                .FirstOrDefault(m=>m.UserId==new Guid(userId));
            if (userMark == null && article.UserId != new Guid(userId))
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

  /*   private async Task<List<CommentViewModel>> CreateCommentViewList(List<CommentModel> comments)
        {
            List<CommentViewModel> list = new List<CommentViewModel>();
            foreach (CommentModel item in comments)
            {
                var user = await FindUserAsync(item.UserId.ToString());
                list.Add(new CommentViewModel()
                {
                    Id = item.Id,
                    Date = item.Date.ToString(),
                    Comment = item.Comment,
                    ArticleId = item.ArticleId,
                    Likes = item.Likes.Count(),
                    Name = user.UserName
                });
            }
            return list;
        }*/

        public async Task<IActionResult> ArticleRead(Guid id)
        {
            ArticleModel article = _articleRepository.Get(id);            
            List<CommentViewModel> commentsViewList = new List<CommentViewModel>();
            foreach (CommentModel item in article.Comments)
            {
                var user = await FindUserAsync(item.UserId.ToString());
                commentsViewList.Add(new CommentViewModel()
                {
                    Id = item.Id,
                    Date = item.Date.ToString(),
                    Comment = item.Comment,
                    ArticleId = item.ArticleId,
                    Likes = item.Likes.Count(),
                    Name = user.UserName
                });
            }
            List<CommentViewModel> orderedCommentsViewList =
                 commentsViewList
                .OrderByDescending(c=>c.Date)
                .ToList();
            Markdown markdown = new Markdown();
            ApplicationUser articleUser =
                await FindUserAsync(article.UserId.ToString());
            ArticleReadViewModel viewModel = new ArticleReadViewModel()
            {
                Id = article.Id,
                Data = markdown.Transform(article.Data),
                Description = article.Description,
                CreatedDate = article.CreatedDate,
                ModifitedDate = article.ModifitedDate,
                Speciality = article.Speciality,
                UserId = article.UserId,
                Name = article.Name,
                Rate = GetAverageRate(article.Marks),
                Comments = orderedCommentsViewList,
                UserName = articleUser.UserName, 
                Tags = article.Tags.Select(t => t.Tag.Title).ToList()
            };
            return View(viewModel);
        }

        [NonAction]
        private List<ArticleTagModel> CreateArticleTagList(List<string> tags, Guid articleId)
        {
            List<ArticleTagModel> articleTagList = new List<ArticleTagModel>();
            if (tags != null)
            {
                foreach (string item in tags)
                {
                    TagModel tag = new TagModel() { Title = item };
                    _tagRepository.Create(tag);
                    articleTagList.Add(new ArticleTagModel()
                    {
                        ArticleId = articleId,
                        TagId = tag.Id
                    });
                }
            }
            return articleTagList;
        }

        [NonAction]
        public List<ArticleListViewModel> CreateArticleList(List<ArticleModel> articleModels)
        {
            List<ArticleListViewModel> articlesLists = new List<ArticleListViewModel>();
            if (articleModels != null)
            {
                foreach (ArticleModel article in articleModels)
                {
                    articlesLists.Add(new ArticleListViewModel()
                    {
                        Name = article.Name,
                        Description = article.Description,
                        Speciality = article.Speciality,
                        Id = article.Id,
                        ModifitedDate = article.ModifitedDate
                        //    Rate = GetAverageRate(article.Marks)
                    });
                }
            }
            return articlesLists;
        }

        [NonAction]
        private async Task SendComment(CommentModel comment)
        {
            ApplicationUser user = await _userManager
                .FindByIdAsync(comment.UserId.ToString());
            CommentViewModel commentForSend = new CommentViewModel()
            {
                Comment = comment.Comment,
                Date = comment.Date.ToString(),
                ArticleId = comment.ArticleId,
                Name = user.UserName,
                Likes = 0
            };
            await _hubContext.Clients
                .All
                .SendAsync("SendComment", commentForSend);
        }

        [NonAction]
        private async Task<ApplicationUser> FindUserAsync(string id)
        {
            ApplicationUser articleUser = await _userManager.FindByIdAsync(id);
            if (articleUser==null)
            {
                articleUser = new ApplicationUser()
                {
                    Id=id,
                    UserName="Пользователь был удален"
                };
            }
            return articleUser;
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

        [NonAction]
        private async Task<ApplicationUser> GetCurrentUser()
        {
            if (User.Identity.Name != null)
            {
                return await _userManager.FindByNameAsync(User.Identity.Name);
            }
            return null;
        }


    }
}
