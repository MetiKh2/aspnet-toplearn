using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
   public class CourseStatus
    {
        [Key]
        public int StatusID { get; set; }

        [Display(Name = "حالت")]
        [Required]
        [MaxLength(200)]
        public string StatusTitle { get; set; }



        #region Relations
        public List<Course> Courses { get; set; }
        #endregion
    }
}
