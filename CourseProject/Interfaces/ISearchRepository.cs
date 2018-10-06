using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Interfaces
{
    public interface ISearchRepository
    {
        IQueryable<ArticleModel> ExecuteSqlQuery(string table, string column, string keyword);
        IQueryable<CommentModel> ExecuteCommentsSqlQuery(string table, string column, string keyword);
        IQueryable<TagModel> ExecuteTagsSqlQuery(string table, string column, string keyword);
        IEnumerable<QueryModel> GetSearchQueries();
        IEnumerable<QueryModel> GetSearchQueries(string keyword);
    }
}
