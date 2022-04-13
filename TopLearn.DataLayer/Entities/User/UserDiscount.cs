using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.DataLayer.Entities.User
{
   public class UserDiscount
    {
        [Key]
        public int UD_ID { get; set; }

        public long UserID { get; set; }
        public long DiscountID { get; set; }

        public User User { get; set; }
        public CourseDiscount CourseDiscount { get; set; }
    }
}
