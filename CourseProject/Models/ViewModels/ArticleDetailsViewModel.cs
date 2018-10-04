using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.ViewModels
{
    public class ArticleDetailsViewModel 
    {   
        public Guid Id { get; set; }

        [Required]
        public string Data { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifitedDate { get; set; }
        [Required]
        [MaxLength(40)]
        public string Speciality { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
