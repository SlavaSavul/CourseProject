using CourseProject.Data;
using CourseProject.Interfaces;
using CourseProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace CourseProject.Services.Repositories
{ 
    public class SearchRepository : ISearchRepository , IRepository<QueryModel>
    {
        ApplicationDbContext Context { get; set; }
        public SearchRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IQueryable<ArticleModel> ExecuteSqlQuery(string table,string column,string keyword)
        {
          return Context.Articles.FromSql(String.Format(
            @"SELECT *                        
            FROM [CourseProjectDB].[dbo].[{0}]
            WHERE FREETEXT ({1},'{2}')", table, column, keyword));
        }

        public IQueryable<CommentModel> ExecuteCommentsSqlQuery(string table, string column, string keyword)
        {
            return Context.Comments.FromSql(String.Format(
             @"SELECT *                        
            FROM [CourseProjectDB].[dbo].[{0}]
            WHERE FREETEXT ({1},'{2}')", table, column, keyword));
        }

        public IQueryable<TagModel> ExecuteTagsSqlQuery(string table, string column, string keyword)
        {
            return Context.Tags.Include(t=>t.ArticleTags).FromSql(String.Format(
             @"SELECT *                        
            FROM [CourseProjectDB].[dbo].[{0}]
            WHERE FREETEXT ({1},'{2}')", table, column, keyword));
        }

        public IEnumerable<QueryModel> GetSearchQueries()
        {
            return Context.Queries;
        }

        public IEnumerable<QueryModel> GetSearchQueries(string keyword)
        {
            return Context.Queries.Where(a => a.Query.Contains(keyword));
        }

        public QueryModel Get(Guid id)
        {

            return Context.Queries.Find(id);
        }

        public IEnumerable<QueryModel> GetAll()
        {
            return Context.Queries;
        }

        public IEnumerable<QueryModel> Find(Expression<Func<QueryModel, bool>> expression)
        {
            return Context
                .Queries
                .Where(expression);
        }

        public void Create(QueryModel query)
        {
            QueryModel existingQuery = Context.Queries.FirstOrDefault(q => q.Query == query.Query);
            if (existingQuery != null)
            {
                query.Id = existingQuery.Id;
            }
            else
            {
                Context.Queries.Add(query);
                Context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            QueryModel article = Context.Queries.Find(id);
            Context.Queries.Remove(article);
            Context.SaveChanges();
        }

        public void Update(QueryModel t)
        {
            Context.Queries.Update(t);
            Context.SaveChanges();
        }

    }
}
