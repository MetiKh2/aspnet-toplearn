using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Order
{
   public class Order
    {
        [Key]
        public int OrderID  { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public int SumAmount { get; set; }


        public bool IsFinaly { get; set; }
       
        [Required]
        public DateTime CreateDate { get; set; }


        public  List<OrderDetail> OrderDetails { get; set; }
        public  User.User User { get; set; }
    }
}
