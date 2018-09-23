using CourseProject.Data;
using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Services.Repositories
{
    public class ComentRepository
    {
        ApplicationDbContext Context { get; set; }
        public ComentRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public void AddArcticle(ComentModel coment)
        {
            Context.Coments.Add(coment);
            Context.SaveChanges();
        }
    }
}
