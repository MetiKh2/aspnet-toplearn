using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Site.Pages.Admin.Courses
{
    [PermissionChecker(1003)]
    public class AddCourseModel : PageModel
    {
      private readonly  ICourseService _courseService;
        public AddCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; }
        public void OnGet()
        {
            var parentGroups = _courseService.GetParentCourceGroupes();
            ViewData["ParentGroupes"] = new SelectList(parentGroups, "GroupeID", "GroupeTitle");

            ViewData["SubGroupes"] = new SelectList(_courseService.GetSubGroupForManageCourse(parentGroups.First().GroupeID), "Value", "Text");
        
            ViewData["Teachers"] = new SelectList(_courseService.GetTeachers(), "Value", "Text");

            ViewData["Statues"] = new SelectList(_courseService.GetStatues(), "Value", "Text");

            ViewData["Levels"] = new SelectList(_courseService.GetLevels(), "Value", "Text");

        }

        public IActionResult OnPost(IFormFile courseImg,IFormFile demoFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _courseService.AddCourse(Course,demoFile,courseImg);
            return RedirectToPage("Index");
        }
    }
}
