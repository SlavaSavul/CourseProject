using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class ArticleTagsModel
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public Guid TagsId { get; set; }
    }
}
