using CourseProject.Data;
using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Services.Repositories
{
    public class MarkRepository
    {
        ApplicationDbContext Context { get; set; }
        public MarkRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public void AddArcticle(MarkModel mark)
        {
            Context.Marks.Add(mark);
            Context.SaveChanges();
        }
    }
}
