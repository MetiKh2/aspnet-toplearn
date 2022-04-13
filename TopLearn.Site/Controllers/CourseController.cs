using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Site.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public CourseController(ICourseService courseService, IOrderService orderService, IUserService userService)
        {
            _userService = userService;
            _orderService = orderService;
            _courseService = courseService;
        }
        public IActionResult Index(int pageId = 1, string filter = "", string getType = "all", string orderBy = "date",
            long startPrice = 0, long endPrice = 0, List<int> selectedGroupes = null, int take = 1000)
        {
            ViewBag.Groupes = _courseService.GetCourseGroupes();
            ViewBag.selectedGroups = selectedGroupes;
            ViewBag.pageId = pageId;
            ViewBag.filter = filter;


            return View(_courseService.GetCourses(pageId, filter, getType, orderBy, startPrice, endPrice, selectedGroupes, take));
        }


        [Route("ShowCourse/{id}")]

        public IActionResult ShowCourse(int id,long episode=0)
        {
            var course = _courseService.GetCourseForShow(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.statusTitle = _courseService.GetStatusByID(course.StatusID).StatusTitle;
            ViewBag.levelTitle = _courseService.GetLevelByID(course.LevelID).LevelTitle;
            ViewBag.Students = _courseService.GetUserCourseCount(id);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.IsBuy = _courseService.IsUserBuyCourse(User.Identity.Name, id);
            }
            else
            {
                ViewBag.IsBuy = false;
            }

            if (episode!=0&&User.Identity.IsAuthenticated)
            {
                var courseEpisode = _courseService.GetCourseEpisodeByID(episode);

                if (course.CourseEpisodes.All(e => e.EpisodeID != episode))
                {
                    return NotFound();
                }
                if (!course.CourseEpisodes.First(p=>p.EpisodeID==episode).IsFree)
                {
                    if (!_courseService.IsUserBuyCourse(User.Identity.Name,id))
                    {
                        return NotFound();
                    }

                }

                var ep = course.CourseEpisodes.First(p => p.EpisodeID == episode);
                ViewBag.Episode = ep;
                string filePath = "";
                string checkFilePath = "";
                if (ep.IsFree)
                {
                    filePath = "/FreeEpisodes/" + ep.EpisodeFile.Replace(".rar",".mp4");
                    checkFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FreeEpisodes", ep.EpisodeFile.Replace(".rar", ".mp4"));
                }
                else
                {
                    filePath = "/BuyEpisodes/" + ep.EpisodeFile.Replace(".rar", ".mp4");
                    checkFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BuyEpisodes", ep.EpisodeFile.Replace(".rar", ".mp4"));
                }

                if (!System.IO.File.Exists(checkFilePath))
                {
                    string targetPath=Directory.GetCurrentDirectory();
                    if (ep.IsFree)
                    {
                        targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FreeEpisodes");
                    }
                    else
                    {
                        targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BuyEpisodes");

                    }
                    string rarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Episodes",ep.EpisodeFile);
                    var archive = ArchiveFactory.Open(rarPath);

                    var Entries = archive.Entries.OrderBy(x => x.Key.Length);
                    foreach (var item in Entries)
                    {
                        if (Path.GetExtension(item.Key)==".mp4")
                        {
                            item.WriteTo(System.IO.File.Create(Path.Combine(targetPath,ep.EpisodeFile.Replace(".rar",".mp4"))));
                        }
                    }


                }

                ViewBag.onlineFilePath = filePath;

            }



            return View(course);
        }

        [Authorize]
        public IActionResult BuyCourse(int id)
        {
            var orderId = _orderService.AddOrder(User.Identity.Name, id);

            return Redirect("/UserPanel/MyOrders/ShowOrders/" + orderId);
        }

        public IActionResult AddComment(CourseComment comment)
        {
            comment.IsRemoved = false;
            comment.DateTime = DateTime.Now;
            comment.UserID = _userService.GetUserIDByUserName(User.Identity.Name);
            _courseService.AddComment(comment);

            return View("ShowComments", _courseService.GetComments(comment.CourseID));
        }

        public IActionResult ShowComments(int id, int pageId = 1)
        {
            return View(_courseService.GetComments(id, pageId));
        }


        public IActionResult ShowVotes(int id)
        {
            if (!_courseService.CourseIsFree(id))
            {
                if (!_courseService.IsUserBuyCourse(User.Identity.Name,id))
                {
                    ViewBag.NotAccess = true;
                }
            }

            return PartialView(_courseService.GetVotesCount(id));
        }

        [Authorize]
        public IActionResult AddVote(int id, bool vote)
        {

                _courseService.AddVote(_userService.GetUserIDByUserName(User.Identity.Name), id, vote);
                return PartialView("ShowVotes", _courseService.GetVotesCount(id));
       }
    }
}
