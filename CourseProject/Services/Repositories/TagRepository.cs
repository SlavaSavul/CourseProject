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
    public class TagRepository : IRepository<TagModel>
    {
        ApplicationDbContext Context { get; set; }
        public TagRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public TagModel Get(Guid id)
        {
            return Context.Tags
                .Include(t => t.ArticleTags)
                .FirstOrDefault(t=>t.Id==id);
        }

        public TagModel GetByHashtag(string hashtag)
        {
           return Context.Tags
                .Include(t=>t.ArticleTags)
                .ThenInclude(a=>a.Article)
                .FirstOrDefault(t => t.Title == hashtag);
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
        public List<TagModel> GetAllTags()
        {
            List<TagModel> tags = new List<TagModel>();
            tags = Context.Tags.Include(t=>t.ArticleTags).ToList();
            return tags;
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

