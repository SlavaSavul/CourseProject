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
    public class ArticleTagRepository : IRepository<ArticleTagModel>
    {
        ApplicationDbContext Context { get; set; }
        public ArticleTagRepository(ApplicationDbContext context)
        {
            Context = context;
        }     

        public ArticleTagModel Get(Guid id)
        {
            return Context.ArticleTags.Find(id);
        }

        public IEnumerable<ArticleTagModel> GetAll()
        {
            return Context.ArticleTags.Include(t=>t.Article);
        }

        public void Create(ArticleTagModel articleTag)
        {
            Context.ArticleTags.Add(articleTag);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            ArticleTagModel articleTag = Context.ArticleTags.Find(id);
            Context.ArticleTags.Remove(articleTag);
            Context.SaveChanges();
        }
       
        public void Update(ArticleTagModel articleTag)
        {
            Context.ArticleTags.Update(articleTag);
            Context.SaveChanges();
        }

        public IEnumerable<ArticleTagModel> Find(Expression<Func<ArticleTagModel, bool>> expression)
        {
            return Context.ArticleTags.Include(t => t.Article).Where(expression);
        }
    }
}
