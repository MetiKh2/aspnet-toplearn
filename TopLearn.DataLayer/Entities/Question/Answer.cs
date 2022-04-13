using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Question
{
  public  class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        [Required]
        public long UserID { get; set; }
        public User.User User { get; set; }
        [Required(ErrorMessage ="بدنه جواب را وارد کنید")]
        public string BodyAnswer { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }

        public bool IsCorrect { get; set; }
    }
}
