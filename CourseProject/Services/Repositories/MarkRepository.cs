using CourseProject.Data;
using CourseProject.Interfaces;
using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Create(MarkModel t)
        {
            Context.Marks.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            MarkModel mark = Context.Marks.Find(id);
            Context.Marks.Remove(mark);
            Context.SaveChanges();
        }

        public void Update(MarkModel t)
        {
            Context.Marks.Update(t);
            Context.SaveChanges();
        }
    }
}
