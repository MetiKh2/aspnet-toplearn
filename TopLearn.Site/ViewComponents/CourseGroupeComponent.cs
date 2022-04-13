using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.ViewComponents
{
    public class CourseGroupeComponent:ViewComponent
    {
        private readonly ICourseService _courseService;
        public CourseGroupeComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IViewComponentResult Invoke()
        {
            return View("CourseGroupe",_courseService.GetCourseGroupes());
        }
    }
}
