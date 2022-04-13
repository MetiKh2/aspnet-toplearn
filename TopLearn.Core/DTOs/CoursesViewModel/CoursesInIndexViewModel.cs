using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.CoursesViewModel
{
   public class CoursesInIndexViewModel
    {
        public List<CoursesViewModel> Courses { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

    }
    public class CoursesViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int EpisodeCount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
