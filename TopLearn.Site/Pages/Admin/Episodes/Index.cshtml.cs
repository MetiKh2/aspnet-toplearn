using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Site.Pages.Admin.Episodes
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseEpisode> Episodes { get; set; }
        public void OnGet(int id)
        {
            Episodes = _courseService.GetCourseEpisodesByCourseID(id);
            ViewData["CourseId"] = id;
        }
    }
}
