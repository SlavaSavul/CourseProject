using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class ComentModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Coment { get; set; }
        public Guid AricleId { get; set; }
        public Guid UserId { get; set; }
    }
}
