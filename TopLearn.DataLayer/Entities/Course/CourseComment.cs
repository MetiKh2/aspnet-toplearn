using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
   public class CourseComment
    {
        [Key]
        public int CommentId { get; set; }

        public long UserID { get; set; }
        public int CourseID { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsRemoved { get; set; }

        #region Rel
        public User.User User { get; set; }
        public Course Course { get; set; }
        #endregion
    }
}
