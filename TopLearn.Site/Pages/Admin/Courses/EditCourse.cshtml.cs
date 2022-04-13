using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Site.Pages.Admin.Courses
{
    public class EditCourseModel : PageModel
    {
        private readonly ICourseService _courseService;
        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; }
        public void OnGet(int id)
        {
            Course = _courseService.GetCourseForEdit(id);

            var parentGroups = _courseService.GetParentCourceGroupes();
            ViewData["ParentGroupes"] = new SelectList(parentGroups, "GroupeID", "GroupeTitle",Course.GropeID);

            List<SelectListItem> listItems = new List<SelectListItem> { 
            new SelectListItem{ 
            Text="لطفا انتخاب کنید",Value=""
            }
            };
            listItems.AddRange(_courseService.GetSubGroupForManageCourse(Course.GropeID));
            string selectedSubGroup = null;
            if (Course.SubGroupe != null)
            {
                selectedSubGroup = Course.SubGroupe.ToString();
            }
            ViewData["SubGroupes"] = new SelectList(listItems, "Value", "Text", selectedSubGroup);

            ViewData["Teachers"] = new SelectList(_courseService.GetTeachers(), "Value", "Text",Course.TeacherID);

            ViewData["Statues"] = new SelectList(_courseService.GetStatues(), "Value", "Text",Course.StatusID);

            ViewData["Levels"] = new SelectList(_courseService.GetLevels(), "Value", "Text",Course.LevelID);
        }
        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.UpdateCourse(Course, imgCourseUp, demoUp);

            return RedirectToPage("Index");
        }
    }
}
