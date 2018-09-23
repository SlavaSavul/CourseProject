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
           return Context.Atricles.Find(id);
        }

        public void Create(ArticleModel t)
        {
            Context.Atricles.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            ArticleModel article = Context.Atricles.Find(id);
            Context.Atricles.Remove(article);
            Context.SaveChanges();
        }

        public void Update(ArticleModel t)
        {
            Context.Atricles.Update(t);
            Context.SaveChanges();
        }

        public IQueryable<ArticleModel> GetRange(List<Guid> id)
        {
           return  Context.Atricles.Where(a =>  id.Contains(a.Id) );
        }
        public IQueryable<ArticleModel> GetUserArticle(Guid id)
        {
           return  Context.Atricles.Where(a => a.UserId==id);
        }
    }
}
