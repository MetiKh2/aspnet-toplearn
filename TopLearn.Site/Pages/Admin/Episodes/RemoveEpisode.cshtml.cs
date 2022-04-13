using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Pages.Admin.Episodes
{
    public class RemoveEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;
        public RemoveEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public long ID { get; set; }
        public void OnGet(int id)
        {
           ID = id;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.DeleteEpisode(ID);
            var courseId = _courseService.GetCourseIDByEpisodeID(ID);

            return Redirect("/Admin/Episodes/Index/"+courseId);
        }
    }
}
