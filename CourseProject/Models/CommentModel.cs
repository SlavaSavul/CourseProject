using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public Guid Article { get; set; }
        public Guid UserId { get; set; }
    }
}
