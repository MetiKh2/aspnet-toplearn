using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.DataLayer.Entities.User
{
    public class User
    {
        public User()
        {

        }

        [Key]
        public long UserID { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserAvatar { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }

        [Display(Name = "کد فعالسازی")]
      
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ActiveCode { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterTime { get; set; }

        [Display(Name = "تاریخ  حذف")]
        public DateTime? RemovedDate { get; set; }

        public bool IsRemoved { get; set; }

        #region Relations
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual List<Wallet.Wallet> Wallets { get; set; }
        public virtual List<Order.Order> Orders { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<UserDiscount> UserDiscounts { get; set; }
        public List<CourseComment> CourseComments { get; set; }
        public List<CourseVote> CourseVotes { get; set; }
        public List<Question.Question> Questions { get; set; }

        #endregion
    }
}
