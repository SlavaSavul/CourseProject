using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Interfaces
{
    public interface ISearchRepository
    {
        IQueryable ExecuteSqlQuery(string table, string column, string keyword);
        IQueryable ExecuteCommentsSqlQuery(string table, string column, string keyword);
        IQueryable ExecuteTagsSqlQuery(string table, string column, string keyword);
        IEnumerable<SearchQueryModel> GetSearchQueries();
        IEnumerable<SearchQueryModel> GetSearchQueries(string keyword);
    }
}
