﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.ViewModels
{
    public class ArticleListViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public string Specialty { get; set; }

    }
}
