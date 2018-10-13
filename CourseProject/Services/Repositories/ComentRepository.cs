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

        public void Create(CommentModel comment)
        {
            Context.Comments.Add(comment);
            Context.SaveChanges();
        }

        public IEnumerable<CommentModel> GetAll()
        {
            return Context.Comments.Include(c=>c.Likes);
        }

        public void Delete(Guid id)
        {
            CommentModel coment = Context.Comments.Find(id);
            Context.Comments.Remove(coment);
            Context.SaveChanges();
        }

        public void Update(CommentModel comment)
        {
            Context.Comments.Update(comment);
            Context.SaveChanges();
        }

        public IEnumerable<CommentModel> Find(Expression<Func<CommentModel, bool>> expression)
        {
            return Context.Comments.Include(c => c.Likes).Where(expression);
        }

        public void DeleteByUserId(string id)
        {
            foreach (CommentModel item in Context.Comments.Where(c => c.UserId == id))
            {
                Context.Comments.Remove(item);
            }
        }
    }
}
