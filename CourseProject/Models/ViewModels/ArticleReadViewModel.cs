using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.ViewModels
{
    public class ArticleReadViewModel
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifitedDate { get; set; }
        public string Specialty { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<CommentViewModel> Coments { get; set; }
        public double Rate { get; set; }
        public string UserName { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}
