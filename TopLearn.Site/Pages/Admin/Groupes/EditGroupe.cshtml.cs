using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Site.Pages.Groupes
{
    public class EditGroupeModel : PageModel
    {
        private readonly ICourseService _courseService;
        public EditGroupeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseGroupe Groupe { get; set; }
        public void OnGet(int id)
        {
            Groupe = _courseService.GetCourseGroupeByID(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           

            _courseService.UpdateGroupe(Groupe);

            return RedirectToPage("Index");
        }
    }
}
