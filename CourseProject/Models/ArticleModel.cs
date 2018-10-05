using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class ArticleModel
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifitedDate { get; set; }
        public string Speciality { get; set; }
        public string Name { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<ArticleTagModel> Tags { get; set; }
        public List<MarkModel> Marks { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


    }
}
