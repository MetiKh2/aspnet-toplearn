using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.DataLayer.Context;

namespace TopLearn.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private readonly TopLearnContext _context;
        public CourseApiController(TopLearnContext context)
        {
            _context = context;
        }

        [HttpGet("Search")]
        [Produces("application/json")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var courseTitles = _context.Courses.Where(p => p.CourseTitle.Contains(term)).Select(p => p.CourseTitle).ToList();
                return Ok(courseTitles);
            }
            catch 
            {

                return BadRequest();
            }
        }


    }
}
