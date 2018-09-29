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

       

        public void Create(TagModel tag)
        {
            TagModel existTag = Context.Tags.FirstOrDefault(t => t.Title == tag.Title);
            if (existTag != null)
            {
                tag.Id = existTag.Id;
            }
            else
            {
                Context.Tags.Add(tag);
                Context.SaveChanges();
            }
           
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

