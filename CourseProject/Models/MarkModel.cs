using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class MarkModel
    {
        public Guid Id { get; set; }
        public Guid AricleId { get; set; }
        public Guid UserId { get; set; }
        public int Value { get; set; }
    }
}
