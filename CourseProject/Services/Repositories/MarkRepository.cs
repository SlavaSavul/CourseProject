using CourseProject.Data;
using CourseProject.Interfaces;
using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CourseProject.Services.Repositories
{
    public class MarkRepository : IRepository<MarkModel>
    {
        ApplicationDbContext Context { get; set; }
        public MarkRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public MarkModel Get(Guid id)
        {
            return Context.Marks.Find(id);
        }

        public IEnumerable<MarkModel> GetAll()
        {
            return Context.Marks; 
        }

        public MarkModel Get(Guid ArticeId, Guid userId)
        {
            return Context.Marks.FirstOrDefault(m=>m.UserId==userId && m.ArticleId == ArticeId);
        }

        public IEnumerable<MarkModel> Find(Expression<Func<MarkModel, bool>> expression)
        {
            return Context.Marks.Where(expression);
        }

        public void Create(MarkModel mark)
        {
            Context.Marks.Add(mark);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            MarkModel mark = Context.Marks.Find(id);
            Context.Marks.Remove(mark);
            Context.SaveChanges();
        }

        public void Update(MarkModel mark)
        {
            Context.Marks.Update(mark);
            Context.SaveChanges();
        }
    }
}
