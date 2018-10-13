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
    public class LikeRepository : IRepository<LikeModel>
    {
        ApplicationDbContext Context { get; set; }
        public LikeRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<LikeModel> GetAll()
        {
            return Context.Likes; 
        }

        public IEnumerable<LikeModel> Find(Expression<Func<LikeModel, bool>> expression)
        {
            return Context.Likes.Where(expression);
        }

        public LikeModel Get(Guid id)
        {
            return Context.Likes.Find(id);
        }

        public void Create(LikeModel like)
        {
            Context.Likes.Add(like);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            LikeModel like = Context.Likes.Find(id);
            Context.Likes.Remove(like);
            Context.SaveChanges();
        }

        public void Update(LikeModel like)
        {
            Context.Likes.Update(like);
            Context.SaveChanges();
        }
    }
}
