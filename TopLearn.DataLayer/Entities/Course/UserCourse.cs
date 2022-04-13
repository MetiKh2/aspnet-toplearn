using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
   public class UserCourse
    {
        [Key]
        public int UC_ID { get; set; }
        public long UserID { get; set; }
        public int CourseID { get; set; }

        #region Rel
        public User.User User { get; set; }
        public Course Course { get; set; }
        #endregion
    }
}
