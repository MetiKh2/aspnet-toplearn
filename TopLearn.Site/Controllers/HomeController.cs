using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        public HomeController(IUserService userService, ICourseService courseService)
        {
            _courseService = courseService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            ViewBag.PopularCourses = _courseService.GetPopularCourses();
            return View(_courseService.GetCourses().Item1);
        }

        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(long id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                 HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                 && HttpContext.Request.Query["Authority"] != "")
            {

                var wallet = _userService.GetWalletByID(id);

                string authority = HttpContext.Request.Query["Authority"];

                var payment = new ZarinpalSandbox.Payment((int)wallet.Amount);

                var res = payment.Verification(authority).Result;

                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                }
            }
            return View();
        }

        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem> { 
           new SelectListItem{
           Text="لطفا انتخاب کنید",
            Value=""
           }
            };
            list.AddRange(_courseService.GetSubGroupForManageCourse(id));
            return Json(new SelectList(list, "Value", "Text"));
        }

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/MyImages/"}{fileName}";


            return Json(new { uploaded = true, url });
        }
        [Route("DownloadFile/{episodeId}")]
        public IActionResult DownloadFile(long episodeId)
        {
            var episode = _courseService.GetCourseEpisodeByID(episodeId);
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Episodes",
            episode.EpisodeFile);
            string fileName = episode.EpisodeFile;
            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filepath);
                return File(file, "application/force-download", fileName);
            }

            if (User.Identity.IsAuthenticated)
            {
                if (_courseService.IsUserBuyCourse(User.Identity.Name,episode.CourseID))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filepath);
                    return File(file, "application/force-download", fileName);
                }
            }

            return BadRequest();
        }
    }
}
