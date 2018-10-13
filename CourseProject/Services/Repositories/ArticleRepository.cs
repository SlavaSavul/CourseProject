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

        public void Create(ArticleModel article)
        {
            Context.Articles.Add(article);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            ArticleModel article = Get(id);
            Context.Articles.Remove(article);
            Context.SaveChanges();
        }
        
        public void Update(ArticleModel article)
        {
            Context.Articles.Update(article);
            Context.SaveChanges();
        }

        public IEnumerable<ArticleModel> Find(Expression<Func<ArticleModel, bool>> expression)
        {
            return Context.Articles.Where(expression);
        } 

        public List<ArticleModel> GetLastModifited()
        {
            return Context.Articles
                .OrderByDescending(a => a.ModifitedDate)
                .ToList();
        }

        public List<ArticleModel> GetWithTopRating()
        {
            List<ArticleModel> articles = Context.Articles
                .Include(a => a.Marks)
                .ToList();
            return articles
                .OrderByDescending(a => Average(a))
                .ToList();
        }

        double Average(ArticleModel article)
        {
            if (article.Marks!=null && article.Marks.Count()>0)
            {
                return article.Marks.Average(m => m.Value);
            }
            return 0;
        }
    }
}
