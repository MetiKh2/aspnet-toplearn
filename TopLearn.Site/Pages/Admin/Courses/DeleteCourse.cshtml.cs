using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.CoursesViewModel;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Pages.Admin.Courses
{
    public class DeleteCourseModel : PageModel
    {
        private readonly ICourseService _courseService;
        public DeleteCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public DeleteCourseViewModel  DeleteCourse { get; set; }
        public void OnGet(int id)
        {
            DeleteCourse = _courseService.GetCourseDataForDeleteCourse(id);
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.DeleteCourse(id);


            return RedirectToPage("Index");
        }
    }
}
