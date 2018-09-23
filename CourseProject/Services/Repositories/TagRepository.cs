using CourseProject.Data;
using CourseProject.Interfaces;
using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Services.Repositories
{
    public class TagRepository : IRepository<TagsModel>
    {
        ApplicationDbContext Context { get; set; }
        public TagRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public TagsModel Get(Guid id)
        {
            return Context.Tags.Find(id);
        }

        public void Create(TagsModel t)
        {
            Context.Tags.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            TagsModel tag = Context.Tags.Find(id);
            Context.Tags.Remove(tag);
            Context.SaveChanges();
        }

        public void Update(TagsModel t)
        {

            Context.Tags.Update(t);
            Context.SaveChanges();
        }
    }
}

