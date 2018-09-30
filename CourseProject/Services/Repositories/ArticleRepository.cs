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
    public class ArticleRepository : IRepository<ArticleModel>
    {
        ApplicationDbContext Context { get; set; }
        public ArticleRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public ArticleModel Get(Guid id)
        {
            return Context.Articles.Include(a => a.Comments)
                 .ThenInclude(c => c.Likes)
                 .Include(a => a.Tags)
                 .ThenInclude(t => t.Tag)
                 .Include(a => a.Marks)
                 .FirstOrDefault(a => a.Id == id);

        }
        public IEnumerable<ArticleModel> GetAll()
        {
            return Context.Articles
                .Include(a => a.Comments)
                .ThenInclude(c => c.Likes)
                .Include(a => a.Tags)
                .Include(a => a.Marks)
                .ToList();
        }

        public List<ArticleModel> GetLastModifited(int count)
        {
            return Context.Articles
                .OrderByDescending(a => a.ModifitedDate)
                .Take(count)
                .ToList();
        }

        public List<ArticleModel> GetWithMarks(int count)
        {
            List<ArticleModel> articles = Context.Articles
                .Include(a => a.Marks)
                .ToList();
            return articles
                .OrderByDescending(a => Average(a))
                .Take(count)
                .ToList();

        }

        double Average(ArticleModel a)
        {
            if (a.Marks!=null && a.Marks.Count()>0)
            {
                return a.Marks.Average(m => m.Value);
            }
            return 0;
        }

        public void Create(ArticleModel t)
        {
            Context.Articles.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            ArticleModel article = Get(id);
            Context.Articles.Remove(article);
            Context.SaveChanges();
        }

        public void Update(ArticleModel t)
        {
            Context.Articles.Update(t);
            Context.SaveChanges();
        }

        public IQueryable<ArticleModel> GetRange(List<Guid> id)
        {
           return  Context.Articles.Where(a =>  id.Contains(a.Id) );
        }
        public IQueryable<ArticleModel> GetUserArticle(Guid id)
        {
             return  Context.Articles.Where(a => a.UserId==id);
        }
    }
}
