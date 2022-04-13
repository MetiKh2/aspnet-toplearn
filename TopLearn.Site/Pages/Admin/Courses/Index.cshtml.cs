using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.CoursesViewModel;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Pages.Admin.Courses
{
    [PermissionChecker(1002)]
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public CoursesInIndexViewModel Courses { get; set; }
        public void OnGet(int pageId=1,string filterTitle="")
        {
            Courses = _courseService.GetCoursesInIndex(filterTitle,pageId);
        }
    }
}
