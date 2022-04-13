using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs.CoursesViewModel;
using TopLearn.Core.Generators;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly TopLearnContext _context;
        private readonly IUserService _userService;
        public CourseService(TopLearnContext context,IUserService userService)
        {
            _userService = userService;
            _context = context;
        }

        public void AddComment(CourseComment comment)
        {
            _context.CourseComments.Add(comment);
            _context.SaveChanges();
        }

        public int AddCourse(Course course, IFormFile demoFile, IFormFile imageCourse)
        {
            course.CourseDate = DateTime.Now;
            course.ImageName = "no-photo.jpg";

            if (imageCourse != null && imageCourse.IsImage())
            {
                course.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imageCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Image", course.ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageCourse.CopyTo(stream);
                }

                string resizePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Thumb", course.ImageName);
                ImageResizer imageResizer = new ImageResizer();
                imageResizer.Image_resize(imagePath, resizePath, 160);
            }

            if (demoFile != null)
            {
                course.DemoFile = NameGenerator.GenerateUniqCode() + Path.GetExtension(demoFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes", course.DemoFile);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageCourse.CopyTo(stream);
                }
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return course.CourseID;
        }

        public long AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            episode.EpisodeFile = episodeFile.FileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Episodes", episodeFile.FileName);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                episodeFile.CopyTo(stream);
            }

            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();
            return episode.EpisodeID;

        }

        public void AddGroupe(CourseGroupe groupe)
        {

            _context.CourseGroupes.Add(groupe);
            _context.SaveChanges();
        }

        public void AddVote(long userId,int courseId,bool vote)
        {
            var userVote = _context.CourseVotes.Where(p => p.UserID == userId && p.CourseID == courseId).FirstOrDefault();

            if (userVote!=null)
            {
                userVote.Vote = vote;
            }
            else
            {
                CourseVote courseVote = new CourseVote { 
                UserID=userId,
                CourseID=courseId,
                Vote=vote,
                
                };
                _context.Add(courseVote);
            }

            _context.SaveChanges();
        }

        public bool CourseIsFree(int id)
        {
            return _context.Courses.Where(p => p.CourseID == id).Select(p => p.Price).First() == 0;
        }

        public void DeleteCourse(int id)
        {
            var course = GetCourseByID(id);

            course.IsRemoved = true;
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void DeleteEpisode(long id)
        {
            var episode = _context.CourseEpisodes.Find(id);

            episode.IsRemoved = true;
            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }

        public void DeleteGroupe(int id)
        {
            var groupe = _context.CourseGroupes.Find(id);

            groupe.IsRemoved = true;
            UpdateGroupe(groupe) ;
        }

        public void EditEpisode(CourseEpisode episode, IFormFile file)
        {
            if (file != null)
            {
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Episodes", episode.EpisodeFile);
                File.Delete(deletePath);

                episode.EpisodeFile = file.FileName;
                string newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Episodes", file.FileName);
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }

        public Tuple<List<CourseComment>,int> GetComments(int courseId,int pageId=1)
        {
            int take = 6;
            int skip = (pageId-1)*take;
            
            int pageCount = _context.CourseComments.Where(p => p.CourseID == courseId).Count() / take;
            return Tuple.Create(_context.CourseComments.Include(p=>p.User).Where(p => p.CourseID == courseId).OrderByDescending(p=>p.CommentId).Skip(skip).Take(take).ToList(),pageCount);
        }

        public Course GetCourseByID(int id)
        {
            return _context.Courses.Find(id);
        }

        public DeleteCourseViewModel GetCourseDataForDeleteCourse(int id)
        {
            return _context.Courses.Where(p => p.CourseID == id).Select(p => new DeleteCourseViewModel
            {
                ID = p.CourseID,
                Title = p.CourseTitle
            }).FirstOrDefault();
        }

        public CourseEpisode GetCourseEpisodeByID(long id)
        {
            return _context.CourseEpisodes.Find(id);
        }

        public List<CourseEpisode> GetCourseEpisodesByCourseID(int courseId)
        {
            return _context.CourseEpisodes.Where(p => p.CourseID == courseId).ToList();
        }

        public Course GetCourseForEdit(int id)
        {
            return _context.Courses.Find(id);
        }

        public Course GetCourseForShow(int id)
        {
            return _context.Courses.Include(p => p.CourseEpisodes).Include(p => p.CourseGroupe).Include(p => p.CourseStatus).Include(p => p.CourseLevel).
                Include(p=>p.Teacher).FirstOrDefault(p=>p.CourseID==id);
        }

        public CourseGroupe GetCourseGroupeByID(int id)
        {
            return _context.CourseGroupes.Find(id);
        }

        public List<CourseGroupe> GetCourseGroupes()
        {
            return _context.CourseGroupes.ToList();
        }

        public int GetCourseIDByEpisodeID(long id)
        {
            return _context.CourseEpisodes.Find(id).CourseID;
        }

        public Tuple<List<ShowCourseListItemViewModel>, int> GetCourses(int pageId = 1, string filter = "", string getType = "all", string orderBy = "date",
            long startPrice = 0, long endPrice = 0, List<int> selectedGroupes = null, int take = 1)
        {
            IQueryable<Course> result = _context.Courses;

            

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(p => p.CourseTitle.Contains(filter));
            }

            switch (getType)
            {
                case "all":
                    break;

                case "buy":
                    {
                        result = result.Where(p => p.Price != 0);
                        break;
                    }
                case "free":
                    {
                        result = result.Where(p => p.Price == 0);
                        break;
                    }
            }

            switch (orderBy)
            {
                case "date":
                    {
                        result = result.OrderByDescending(p => p.CourseID);
                        break;
                    }

                case "updatedate":
                    {
                        result = result.OrderByDescending(p => p.UpdateDate);
                        break;
                    }

            }

            if (startPrice > 0)
            {
                result = result.Where(p => p.Price > startPrice);
            }
            if (endPrice > 0)
            {
                result = result.Where(p => p.Price < endPrice);
            }

            if (selectedGroupes != null && selectedGroupes.Any())
            {
                foreach (var item in selectedGroupes)
                {
                    result = result.Where(p => p.SubGroupe == item || p.GropeID == item);
                }
            }

            int skip = (pageId - 1) * take;

            int pageCount = result.Select(p => new ShowCourseListItemViewModel
            {
                ID = p.CourseID,
                ImageName = p.ImageName,
                Price = p.Price,
                Title = p.CourseTitle,
                //   TotalTime = new TimeSpan(p.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).Count() / take;

            var query = result.Select(p => new ShowCourseListItemViewModel
            {
                ID = p.CourseID,
                ImageName = p.ImageName,
                Price = p.Price,
                Title = p.CourseTitle,
                //   TotalTime = new TimeSpan(p.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).OrderByDescending(p=>p.ID).Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }

        public CoursesInIndexViewModel GetCoursesInIndex(string filterTitle = "", int pageId = 1)
        {
            IQueryable<Course> courses = _context.Courses;

            if (!string.IsNullOrEmpty(filterTitle))
            {
                courses = courses.Where(p => p.CourseTitle.Contains(filterTitle));
                int take = 100000;
                int skip = (pageId - 1) * take;
                CoursesInIndexViewModel list = new CoursesInIndexViewModel
                {
                    Courses = courses.OrderByDescending(p => p.CourseID).Select(p => new CoursesViewModel
                    {
                        ID = p.CourseID,
                        DateTime = p.CourseDate,
                        EpisodeCount = p.CourseEpisodes.Count,
                        ImageName = p.ImageName,
                        Title = p.CourseTitle
                    }).Skip(skip).Take(take).ToList(),
                    CurrentPage = pageId,
                    PageCount = courses.Count() / take
                };
                return list;
            }
            else
            {
                int take = 2;
                int skip = (pageId - 1) * take;
                CoursesInIndexViewModel list = new CoursesInIndexViewModel
                {
                    Courses = courses.OrderByDescending(p => p.CourseID).Select(p => new CoursesViewModel
                    {
                        ID = p.CourseID,
                        DateTime = p.CourseDate,
                        EpisodeCount = p.CourseEpisodes.Count,
                        ImageName = p.ImageName,
                        Title = p.CourseTitle
                    }).Skip(skip).Take(take).ToList(),
                    CurrentPage = pageId,
                    PageCount = courses.Count() / take
                };
                return list;
            }
            // Show Item In Page




        }

        public CourseLevel GetLevelByID(int id)
        {
            return _context.CourseLevels.Find(id);
        }

        public List<SelectListItem> GetLevels()
        {
            return _context.CourseLevels.Select(p => new SelectListItem
            {
                Text = p.LevelTitle,
                Value = p.LevelID.ToString()
            }).ToList();
        }

        public List<CourseGroupe> GetParentCourceGroupes()
        {
            return _context.CourseGroupes.Where(p => p.ParentID == null).ToList();
        }

        public List<ShowCourseListItemViewModel> GetPopularCourses()
        {
            return _context.Courses.Include(p => p.OrderDetails).OrderByDescending(p => p.OrderDetails.Count).Where(p=>p.OrderDetails.Any())
                .Select(p => new ShowCourseListItemViewModel
            {
                ID = p.CourseID,
                ImageName = p.ImageName,
                Price = p.Price,
                Title = p.CourseTitle
            }).Take(8).ToList();
        }

        public List<SelectListItem> GetStatues()
        {
            return _context.CourseStatuses.Select(p => new SelectListItem
            {
                Text = p.StatusTitle,
                Value = p.StatusID.ToString()
            }).ToList();
        }

        public CourseStatus GetStatusByID(int id)
        {
            return _context.CourseStatuses.Find(id);

        }

        public List<SelectListItem> GetSubGroupForManageCourse(int parentId)
        {
            return _context.CourseGroupes.Where(p => p.ParentID == parentId).Select(
                p => new SelectListItem
                {
                    Text = p.GroupeTitle,
                    Value = p.GroupeID.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetTeachers()
        {
            return _context.UserInRoles.Where(p => p.RoleID == 2).Include(p => p.User).Select(p => new SelectListItem
            {
                Text = p.User.UserName,
                Value = p.UserID.ToString(),
            }).ToList();
        }

        public int GetUserCourseCount(int courseId)
        {
            return _context.UserCourses.Where(p => p.CourseID == courseId).Count();
        }

        public Tuple<int, int> GetVotesCount(int courseId)
        {
           var votes =_context.CourseVotes.Where(p => p.CourseID == courseId).Select(p => p.Vote).ToList();
            return Tuple.Create(votes.Count(p=>p==true), votes.Count(p => p == false));
        }

        public bool IsExistFile(IFormFile file)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Episodes", file.FileName);
            return File.Exists(path);
        }

        public bool IsUserBuyCourse(string username, int courseId)
        {
            var userId = _userService.GetUserIDByUserName(username);
            return _context.UserCourses.Any(p => p.UserID == userId && p.CourseID == courseId); ;
        }

        public void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.UpdateDate = DateTime.Now;

            if (imgCourse != null && imgCourse.IsImage())
            {
                if (course.ImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Image", course.ImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Thumb", course.ImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }
                course.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Image", course.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                ImageResizer imgResizer = new ImageResizer();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Thumb", course.ImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            if (courseDemo != null)
            {
                if (course.DemoFile != null)
                {
                    string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes", course.DemoFile);
                    if (File.Exists(deleteDemoPath))
                    {
                        File.Delete(deleteDemoPath);
                    }
                }
                course.DemoFile = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes", course.DemoFile);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }

            _context.Courses.Update(course);
            _context.SaveChanges();

        }

        public void UpdateGroupe(CourseGroupe groupe)
        {
            _context.CourseGroupes.Update(groupe);
            _context.SaveChanges();
        }
    }
}
