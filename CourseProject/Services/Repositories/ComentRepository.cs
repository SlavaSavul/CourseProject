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
    public class ComentRepository : IRepository<CommentModel>
    {
        ApplicationDbContext Context { get; set; }
        public ComentRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public CommentModel Get(Guid id)
        {
            return Context.Comments
                .Include(c=>c.Likes)
                .FirstOrDefault(c=>c.Id== id);
        }
        public IQueryable<CommentModel> GetByArticleId(Guid id)
        {
            return Context.Comments.Where(c=>c.ArticleId == id);
        }

        public void Create(CommentModel t)
        {
            Context.Comments.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            CommentModel coment = Context.Comments.Find(id);
            Context.Comments.Remove(coment);
            Context.SaveChanges();
        }

        public void Update(CommentModel t)
        {
            Context.Comments.Update(t);
            Context.SaveChanges();
        }
    }
}
