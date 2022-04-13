using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Course
{
   public class CourseDiscount
    {
        [Key]
        public int DiscountID { get; set; }

        [Required]
        [MaxLength(150)]
        public string DiscountCode { get; set; }

        [Required]
        public int DiscountPercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? UsableCount { get; set; }
        public List<UserDiscount> UserDiscounts { get; set; }


    }

}
