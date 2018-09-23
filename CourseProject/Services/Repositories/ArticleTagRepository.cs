using CourseProject.Data;
using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Services.Repositories
{
    public class ArticleTagRepository
    {
        ApplicationDbContext Context { get; set; }
        public ArticleTagRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public void AddArcticle(ArticleTagsModel articleTags)
        {
            Context.ArticleTags.Add(articleTags);
            Context.SaveChanges();
        }
    }
}
