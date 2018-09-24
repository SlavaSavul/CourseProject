using CourseProject.Data;
using CourseProject.Interfaces;
using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Services.Repositories
{
    public class ComentRepository : IRepository<ComentModel>
    {
        ApplicationDbContext Context { get; set; }
        public ComentRepository(ApplicationDbContext context)
        {
            Context = context;
        }

     

        public ComentModel Get(Guid id)
        {
            return Context.Coments.Find(id);
        }
        public IQueryable<ComentModel> GetByArticleId(Guid id)
        {
            return Context.Coments.Where(c=>c.AricleId==id);
        }

        public void Create(ComentModel t)
        {
            Context.Coments.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            ComentModel coment = Context.Coments.Find(id);
            Context.Coments.Remove(coment);
            Context.SaveChanges();
        }

        public void Update(ComentModel t)
        {
            Context.Coments.Update(t);
            Context.SaveChanges();
        }
    }
}
