using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Question
{
   public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required(ErrorMessage ="عنوان سوال را وارد کنید")]
        [MaxLength(400)]
        [Display(Name ="عنوان سوال")]
        public string Title { get; set; }
        [Required(ErrorMessage = "متن سوال را وارد کنید")]
        [Display(Name ="متن سوال")]
        public string Body { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Required]
        public long UserID { get; set; }

        public User.User User { get; set; }
        public Course.Course Course { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
