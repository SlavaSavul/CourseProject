using CourseProject.Data;
using CourseProject.Interfaces;
using CourseProject.Models;
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

        public LikeModel Get(string id)
        {
            return Context.Likes.Find(id);
        }

        public void Create(LikeModel t)
        {
            Context.Likes.Add(t);
            Context.SaveChanges();
        }

        public void Delete(LikeModel t)
        {
            Context.Likes.Remove(t);
            Context.SaveChanges();
        }

        public void Update(LikeModel t)
        {
            Context.Likes.Update(t);
            Context.SaveChanges();
        }
    }
}
