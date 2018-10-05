using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.ViewModels
{
    public class ArticleDetailsViewModel 
    {   
        [Required(ErrorMessage = "RequiredField")]
        public string Data { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "DescriptionLengthError")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "SpecialityLengthError")]
        [Display(Name = "Speciality")]
        public string Speciality { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "NameLengthError")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Tags")]
        public List<string> Tags { get; set; }

        public Guid Id { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifitedDate { get; set; }
        public Guid UserId { get; set; }
    }
}
