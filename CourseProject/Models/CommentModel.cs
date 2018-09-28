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
       // public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public List<LikeModel> Likes { get; set; }



        public Guid ArticleId { get; set; }
        public ArticleModel Article { get; set; }
    }
}
