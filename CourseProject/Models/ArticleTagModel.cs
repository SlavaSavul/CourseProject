using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class ArticleTagModel
    {
        public Guid Id { get; set; }
        public Guid ArticleIdId { get; set; }
        public ArticleModel Article { get; set; }
        public Guid TagId { get; set; }
        public TagModel Tag { get; set; }
    }
}
