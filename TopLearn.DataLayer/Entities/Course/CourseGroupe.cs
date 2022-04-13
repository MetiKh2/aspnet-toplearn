using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseGroupe
    {
        [Key]
        public int GroupeID { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupeTitle { get; set; }
     
        [Display(Name = "حذف شده ؟")]
        public bool IsRemoved { get; set; }

        [Display(Name = "گروه اصلی")]
        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public List<CourseGroupe> CourseGroups { get; set; }

        [InverseProperty("CourseGroupe")]
        public List<Course> Courses { get; set; }
        [InverseProperty("CourseSubGroupe")]
        public List<Course> SubGroupes { get; set; }


    }
}
