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
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseGroupe> Groupes { get; set; }
        public void OnGet(int id)
        {
            Groupes = _courseService.GetCourseGroupes();
        }
    }
}
