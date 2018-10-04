using CourseProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class PersonalAreaModel
    {
        public List<ArticleListViewModel> ArticleList { get; set; }//not ArticleListViewModel
        //public string UserName{ get; set; }
        public Guid AspNetUserId { get; set; }
        public Guid Id { get; set; }
        public ApplicationUser AspNetUser { get; set; }




      
    }
}
