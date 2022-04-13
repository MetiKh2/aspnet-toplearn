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
    public class AddEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;
        public AddEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode Episode { get; set; }
        public void OnGet(int id)
        {
            Episode = new CourseEpisode();
            Episode.CourseID = id;
        }

        public IActionResult OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid|| fileEpisode==null)
            {
                return Page();
            }
            if (_courseService.IsExistFile(fileEpisode))
            {
                ViewData["IsExistFile"] = true;
                return Page();
            }

            _courseService.AddEpisode(Episode,fileEpisode);
            return Redirect("/Admin/Episodes/Index/" + Episode.CourseID);
        }
    }
}
