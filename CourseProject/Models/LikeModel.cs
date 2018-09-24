using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class LikeModel
    {
        public Guid Id { get; set; }
        public Guid AricleId { get; set; }//change name to CommentId and update DB
        public Guid UserId { get; set; }
    }
}
