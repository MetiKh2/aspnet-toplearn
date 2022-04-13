using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Question;

namespace TopLearn.Site.Controllers
{
    public class FroumController : Controller
    {
        private readonly IFroumService _froumService;
        private readonly IUserService _userService;
        public FroumController(IFroumService froumService,IUserService userService)
        {
            _userService = userService;
            _froumService = froumService;
        }
        public IActionResult Index(int? courseId, string filter="")
        {
            ViewBag.CourseId = courseId;
            return View(_froumService.GetQuestions(courseId,filter)) ;
        }
        #region CreateQuestion
        [Authorize]
        public IActionResult CreateQuestion(int id)
        {
            Question question = new Question
            {
                CourseID = id
            };
            return View(question);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateQuestion(Question question)
        {
            //question.UpdateDate = DateTime.Now;
            //question.CreateDate = DateTime.Now;
            question.UserID = _userService.GetUserIDByUserName(User.Identity.Name);
            if (!ModelState.IsValid)
            {
                return View(question);
            }
            int questionId = _froumService.AddQuestion(question);
            return Redirect($"/Froum/ShowQuestion/{questionId}");
        }
        #endregion

        #region Show Question
        public IActionResult ShowQuestion(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (_froumService.IsQuestionForUser(id, _userService.GetUserIDByUserName(User.Identity.Name))) ViewBag.questionForUser = true;
            }
            return View(_froumService.ShowQuestion(id));
        }
        #endregion

        #region Answer
        [HttpPost]
        [Authorize]
        public IActionResult CreateAnswer(int id,string body)
        {
            var userId= _userService.GetUserIDByUserName(User.Identity.Name);
            if (!string.IsNullOrEmpty(body))
            {
                var sanitizer = new HtmlSanitizer();
                body= sanitizer.Sanitize(body);

                _froumService.AddAnswer(new Answer { 
                BodyAnswer=body,
                QuestionId=id,
                CreateDate=DateTime.Now,
                UserID=userId,
                });
            }
            return Redirect($"/Froum/ShowQuestion/{id}");            
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddIsCorrectAnswer(int questionId,int answerId)
        {
            if (!_froumService.IsExisAnswer(answerId) || !_froumService.IsExisQuestion(questionId)) return NotFound();
            if (!_froumService.IsQuestionForUser(questionId, _userService.GetUserIDByUserName(User.Identity.Name))) return NotFound();
            _froumService.AddIsCorrcetAnswer(answerId);
            return Redirect($"/Froum/ShowQuestion/{questionId}");
        }
        #endregion
    }
}
