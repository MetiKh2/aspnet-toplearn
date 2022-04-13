using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        public int StatusID { get; set; }

        [Required]
        public int LevelID { get; set; }

        [Required]
        public int GropeID { get; set; }

        public int? SubGroupe { get; set; }

        [Required]
        public long TeacherID { get; set; }


        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseTitle { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescription { get; set; }
        [Required]
        public DateTime CourseDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [Display(Name = "حذف شده؟")]
        public bool IsRemoved { get; set; }

        [Display(Name = "تصویر دوره")]
        [MaxLength(400)]
        public string ImageName { get; set; }
      
        [Display(Name = "دمو دوره")]
        [MaxLength(400)]
        public string DemoFile { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long Price { get; set; }

        [MaxLength(600)]
        public string Tags { get; set; }

        #region Relations
        public CourseLevel CourseLevel { get; set; }
        public CourseStatus CourseStatus { get; set; }

        public List<CourseEpisode> CourseEpisodes { get; set; }

        [ForeignKey("TeacherID")]
        public User.User Teacher { get; set; }

        [ForeignKey("GropeID")]
        public CourseGroupe CourseGroupe { get; set; }
        [ForeignKey("SubGroupe")]
        public CourseGroupe CourseSubGroupe { get; set; }

        public List<UserCourse> UserCourses { get; set; }
        public List<CourseComment> CourseComments { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<CourseVote> CourseVotes { get; set; }
        public List<Question.Question> Questions { get; set; }
        #endregion
    }
}
