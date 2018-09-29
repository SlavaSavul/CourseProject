using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<ArticleModel> GetIndexedArticles(string keyword);
        IEnumerable<SearchQueryModel> GetSearchQueries();
        IEnumerable<SearchQueryModel> GetSearchQueries(string keyword);
        void Create(SearchQueryModel t);
    }
}
