using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.CoursesViewModel
{
   public class ShowCourseListItemViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public long Price { get; set; }
        public string ImageName { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}
