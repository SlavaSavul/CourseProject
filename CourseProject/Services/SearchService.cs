using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Services
{
    public class SearchService:ISearchService
    {
        private SearchRepository _searchRepository;
        private ArticleRepository _articleRepository;
        private readonly ComentRepository _comentRepository;
        private readonly TagRepository _tagRepository;
        private readonly ArticleTagRepository _articleTagRepository;

        public SearchService(
            SearchRepository searchRepository,
            ArticleRepository articleRepository,
            ComentRepository comentRepository,
            LikeRepository likeRepository,
            TagRepository tagRepository,
            ArticleTagRepository articleTagRepository)
        {
            _searchRepository = searchRepository;
            _articleRepository = articleRepository;
            _comentRepository = comentRepository;
            _tagRepository = tagRepository;
            _articleTagRepository = articleTagRepository;
        }

        public IEnumerable<ArticleModel> GetIndexedArticles(string keyword)
        {
            List<ArticleModel> result = new List<ArticleModel>();
            if (keyword != null) { 
            result.AddRange((IQueryable<ArticleModel>)_searchRepository.ExecuteSqlQuery("Articles", "Data", keyword));
            result.AddRange((IQueryable<ArticleModel>)_searchRepository.ExecuteSqlQuery("Articles", "Description", keyword));
            result.AddRange((IQueryable<ArticleModel>)_searchRepository.ExecuteSqlQuery("Articles", "Name", keyword));
            result.AddRange((IQueryable<ArticleModel>)_searchRepository.ExecuteSqlQuery("Articles", "Specialty", keyword));
            result.AddRange(GetComments(keyword));
            result.AddRange(GetTags(keyword));
            }
            return result;
        }

        private IEnumerable<ArticleModel> GetComments(string keyword)
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            IQueryable<CommentModel> comments = (IQueryable<CommentModel>)_searchRepository.ExecuteCommentsSqlQuery("Comments", "Comment", keyword);
            foreach (CommentModel item in comments)
            {
                articles.Add(_articleRepository.Get(item.Id));
            }
            return articles;
        }

        private IEnumerable<ArticleModel> GetTags(string keyword)
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            IQueryable<TagModel> tags = (IQueryable<TagModel>)_searchRepository.ExecuteTagsSqlQuery("Tags", "Title", keyword);
            foreach (TagModel item in tags)
            {
                articles.Add(_articleRepository.Get(item.Id));
            }
            return articles;
        }


        public IEnumerable<SearchQueryModel> GetSearchQueries()
        {
            return _searchRepository.GetSearchQueries();
        }

        public IEnumerable<SearchQueryModel> GetSearchQueries(string keyword)
        {
            return _searchRepository.GetSearchQueries(keyword);
        }


        public void Create(SearchQueryModel t)
        {
            _searchRepository.Create(t);
        }
    }
}
