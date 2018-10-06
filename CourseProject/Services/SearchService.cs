using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Services
{
    public class SearchService : ISearchService
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
            if (keyword != null)
            {
                result.AddRange(_searchRepository.ExecuteSqlQuery("Articles", "Data", keyword));
                result.AddRange(_searchRepository.ExecuteSqlQuery("Articles", "Description", keyword));
                result.AddRange(_searchRepository.ExecuteSqlQuery("Articles", "Name", keyword));
                result.AddRange(_searchRepository.ExecuteSqlQuery("Articles", "Speciality", keyword));
                result.AddRange(GetArticleByComments(keyword));
                result.AddRange(GetArticleByTags(keyword));
            }
            return result.Distinct();
        }

        private IEnumerable<ArticleModel> GetArticleByComments(string keyword)
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            IQueryable<CommentModel> comments = _searchRepository
                .ExecuteCommentsSqlQuery("Comments", "Comment", keyword);
            foreach (CommentModel item in comments)
            {
                articles.Add(_articleRepository.Get(item.ArticleId));
            }
            return articles;
        }

        private IEnumerable<ArticleModel> GetArticleByTags(string keyword)
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            IQueryable<TagModel> tags = _searchRepository.ExecuteTagsSqlQuery("Tags", "Title", keyword);
            foreach (TagModel tag in tags)
            {
                foreach (ArticleTagModel articleTag in tag.ArticleTags)
                {
                    articles.Add(_articleRepository.Get(articleTag.ArticleId));
                }
            }
            return articles;
        }

        public IEnumerable<ArticleModel> GetByHashtag(string hashtag)
        {
            TagModel tag = _tagRepository.GetByHashtag(hashtag);
            if (tag!=null)
            {
                IEnumerable<ArticleModel> articles = tag
               .ArticleTags
               .Select(t => t.Article);
                return articles;
            }
            return null;
        }


        public IEnumerable<QueryModel> GetSearchQueries()
        {
            return _searchRepository.GetSearchQueries();
        }

        public IEnumerable<QueryModel> GetSearchQueries(string keyword)
        {
            return _searchRepository.GetSearchQueries(keyword);
        }

        public void Create(QueryModel t)
        {
            _searchRepository.Create(t);
        }

    }
}
