using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseEpisode
    {
        [Key]
        public long EpisodeID { get; set; }

        public int CourseID { get; set; }

        [Display(Name = "عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EpisodeTitle { get; set; }

        public bool IsRemoved { get; set; }

        [Display(Name = "فایل بخش")]       
        public string EpisodeFile { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }

        [Display(Name = "زمان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan EpisodeTime { get; set; }


        #region Relations
        public Course Course { get; set; }
        #endregion
    }
}
