using CourseProject.Data;
using CourseProject.Interfaces;
using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Services.Repositories
{
    public class TagRepository : IRepository<TagModel>
    {
        ApplicationDbContext Context { get; set; }
        public TagRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public TagModel Get(Guid id)
        {
            return Context.Tags.Find(id);
        }

       

        public void Create(TagModel t)
        {
            Context.Tags.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            TagModel tag = Context.Tags.Find(id);
            Context.Tags.Remove(tag);
            Context.SaveChanges();
        }

        public void Update(TagModel t)
        {

            Context.Tags.Update(t);
            Context.SaveChanges();
        }
    }
}

