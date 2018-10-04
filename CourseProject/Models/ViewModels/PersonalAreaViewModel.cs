using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.ViewModels
{
    public class PersonalAreaViewModel
    {
        public List<ArticleListViewModel> ArticleList { get; set; }
        public string UserName { get; set; }
    }
}
