using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class TagModel
    {
        [Key]
        public string Title { get; set; }
        public Guid ArticleId { get; set; }
        public ArticleModel Article { get; set; }
    }
}


