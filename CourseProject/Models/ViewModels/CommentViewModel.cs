using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Coment { get; set; }
        public Guid AricleId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Likes { get; set; }


    }
}
