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
    public class LikeRepository : IRepository<LikeModel>
    {
        ApplicationDbContext Context { get; set; }
        public LikeRepository(ApplicationDbContext context)
        {
            Context = context;
        }


        public IQueryable<LikeModel> GetByCommentId(Guid id) 
        {
            return Context.Likes.Where(m => m.CommentId == id);
        }

       

        public LikeModel Get(Guid id)
        {
            return Context.Likes.Find(id);
        }

        public void Create(LikeModel t)
        {
            Context.Likes.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            LikeModel like = Context.Likes.Find(id);
            Context.Likes.Remove(like);
            Context.SaveChanges();
        }
        public LikeModel Get(Guid commentId,Guid userId)
        {
            return Context.Likes.FirstOrDefault(l=> l.UserId == userId && l.CommentId == commentId);  
        }

        public void Update(LikeModel t)
        {
            Context.Likes.Update(t);
            Context.SaveChanges();
        }
    }
}
