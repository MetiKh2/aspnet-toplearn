using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.CoursesViewModel;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Course
        CoursesInIndexViewModel GetCoursesInIndex(string filterTitle = "", int pageId = 1);
        List<CourseGroupe> GetCourseGroupes();
        List<CourseGroupe> GetParentCourceGroupes();
        List<SelectListItem> GetSubGroupForManageCourse(int parentId);
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatues();
        int AddCourse(Course course, IFormFile demoFile, IFormFile imageCourse);
        Course GetCourseForEdit(int id);
        void UpdateCourse(Course course, IFormFile imgCourseUp, IFormFile demoUp);
        DeleteCourseViewModel GetCourseDataForDeleteCourse(int id);
        Course GetCourseByID(int id);
        void DeleteCourse(int id);
        Tuple<List<ShowCourseListItemViewModel>, int> GetCourses(int pageId=1, string filter = "", string getType = "all", string orderBy = "date"
            , long startPrice = 0, long endPrice = 0, List<int> selectedGroupes = null, int take = 8);

        Course GetCourseForShow(int id);

        CourseLevel GetLevelByID(int id);
        CourseStatus GetStatusByID(int id);
        bool IsUserBuyCourse(string username,int courseId);

        void AddGroupe(CourseGroupe groupe);
        void UpdateGroupe(CourseGroupe groupe);

        CourseGroupe GetCourseGroupeByID(int id);
        void DeleteGroupe(int id);

        int GetUserCourseCount(int courseId);
        bool CourseIsFree(int id);
            
        #endregion

        #region Episodes
        long AddEpisode(CourseEpisode episode, IFormFile file);
        bool IsExistFile(IFormFile file);
        List<CourseEpisode> GetCourseEpisodesByCourseID(int courseId);
        CourseEpisode GetCourseEpisodeByID(long id);
        void EditEpisode(CourseEpisode episode, IFormFile file);
        void DeleteEpisode(long id);
        int GetCourseIDByEpisodeID(long id);
        #endregion
        #region Comment
        void AddComment(CourseComment comment);
         Tuple<List<CourseComment>, int> GetComments(int courseId, int pageId = 1);

        List<ShowCourseListItemViewModel> GetPopularCourses();

        #endregion


        #region Vote
        void AddVote(long userId, int courseId, bool vote);
        Tuple<int, int> GetVotesCount(int courseId);
        #endregion

    }
}
