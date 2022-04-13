using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Order
{
  public  class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]

        public int Count { get; set; }
        [Required]

        public int Price { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }
        [ForeignKey("CourseID")]
        public Course.Course Course { get; set; }

    }
}
