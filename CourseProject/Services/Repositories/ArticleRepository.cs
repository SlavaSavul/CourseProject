using CourseProject.Data;
using CourseProject.Interfaces;
using CourseProject.Models;
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
           return Context.Articles.Find(id);
        }
        public IEnumerable<ArticleModel> GetAll()
        {
            return Context.Articles.ToList();
        }

        public void Create(ArticleModel t)
        {
            Context.Articles.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            ArticleModel article = Context.Articles.Find(id);
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
