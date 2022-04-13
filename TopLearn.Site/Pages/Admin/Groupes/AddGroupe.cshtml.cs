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
    public class AddGroupeModel : PageModel
    {
        private readonly ICourseService _courseService;
        public AddGroupeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseGroupe Groupe { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Groupe.ParentID = id;
            
            _courseService.AddGroupe(Groupe);

            return RedirectToPage("Index");
        }
    }
}
