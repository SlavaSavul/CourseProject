using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.HomeViewModels
{
    public class MainPageViewModel
    {
        public List<ArticleListViewModel> LatestModified { get; set; }
        public List<ArticleListViewModel> TopRating { get; set; }
        public List<TagViewModel> Tags { get; set; }


    }
}
