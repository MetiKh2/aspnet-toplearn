using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseLevel
    {
        [Key]
        public int LevelID { get; set; }
        [Display(Name = "عنوان سطح")]
        [Required]
        [MaxLength(200)]
        public string LevelTitle { get; set; }



        #region Relations
        public List<Course> Courses { get; set; }
        #endregion
    }
}
