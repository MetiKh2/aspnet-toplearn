using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Site.Pages.Admin.Episodes
{
    public class EditEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;
        public EditEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode Episode { get; set; }

        public void OnGet(int id)
        {
            Episode = _courseService.GetCourseEpisodeByID(id);
        }

        public IActionResult OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _courseService.EditEpisode(Episode,fileEpisode);

            return Redirect("/Admin/Episodes/index/"+Episode.CourseID);
        }
    }
}
