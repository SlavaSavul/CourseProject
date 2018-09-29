using CourseProject.Data;
using CourseProject.Interfaces;
using CourseProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Services.Repositories
{ 
    public class SearchRepository : ISearchRepository, IRepository<SearchQueryModel>
    {
        ApplicationDbContext Context { get; set; }
        public SearchRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IQueryable ExecuteSqlQuery(string table,string column,string keyword)
        {
          return Context.Articles.FromSql(String.Format(
            @"SELECT *                        
            FROM [CourseProject].[dbo].[{0}]
            WHERE FREETEXT ({1},'{2}')", table, column, keyword));
        }

        public IQueryable ExecuteCommentsSqlQuery(string table, string column, string keyword)
        {
            return Context.Comments.FromSql(String.Format(
             @"SELECT *                        
            FROM [CourseProject].[dbo].[{0}]
            WHERE FREETEXT ({1},'{2}')", table, column, keyword));
        }

        public IQueryable ExecuteTagsSqlQuery(string table, string column, string keyword)
        {
            return Context.Tags.FromSql(String.Format(
             @"SELECT *                        
            FROM [CourseProject].[dbo].[{0}]
            WHERE FREETEXT ({1},'{2}')", table, column, keyword));
        }

        public IEnumerable<SearchQueryModel> GetSearchQueries()
        {
            return Context.Queries;
        }

        public IEnumerable<SearchQueryModel> GetSearchQueries(string keyword)
        {
            return Context.Queries.Where(a=>a.Query.Contains(keyword));
        }



        public SearchQueryModel Get(Guid id)
        {

            return Context.Queries.Find(id);
        }

        public void Create(SearchQueryModel t)
        {
            Context.Queries.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            SearchQueryModel article = Context.Queries.Find(id);
            Context.Queries.Remove(article);
            Context.SaveChanges();
        }

        public void Update(SearchQueryModel t)
        {
            Context.Queries.Update(t);
            Context.SaveChanges();
        }

    }
}
