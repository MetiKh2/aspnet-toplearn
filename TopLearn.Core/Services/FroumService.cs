using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.FourmDTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Question;

namespace TopLearn.Core.Services
{
    public class FroumService : IFroumService
    {
        private readonly TopLearnContext _db;
        public FroumService(TopLearnContext db)
        {
            _db = db;
        }

        public void AddAnswer(Answer answer)
        {
            _db.Answers.Add(answer);
            _db.SaveChanges();
        }

        public void AddIsCorrcetAnswer(int id)
        {
            var answer = _db.Answers.Find(id);
            answer.IsCorrect =!answer.IsCorrect;
            _db.Answers.Update(answer);
            _db.SaveChanges();
        }

        public int AddQuestion(Question question)
        {
            question.CreateDate = DateTime.Now;
            question.UpdateDate = DateTime.Now;
            _db.Questions.Add(question);
            _db.SaveChanges();
            return question.QuestionId;
        }

        public IEnumerable<Question> GetQuestions(int? courseId, string filter = "")
        {
            IQueryable<Question> result = _db.Questions.OrderByDescending(p=>p.CreateDate).Where(p=>EF.Functions.Like(p.Title,$"%{filter}%"));
            if (courseId!=null)
            {
                result = result.Where(p => p.CourseID == courseId);
            }
            return result.Include(p=>p.Course).Include(p=>p.User).ToList(); 
        }

        public bool IsExisAnswer(int id)
        {
            return _db.Answers.Any(p => p.AnswerId == id);
        }

        public bool IsExisQuestion(int id)
        {
            return _db.Questions.Any(p=>p.QuestionId==id);
        }

        public bool IsQuestionForUser(int questionId, long userId)
        {
            return _db.Questions.Any(p=>p.QuestionId==questionId&&p.UserID==userId);
        }

        public ShowQuestionVM ShowQuestion(int questionId)
        {
            var result = new ShowQuestionVM ();
            result.Question = _db.Questions.Include(p => p.User).FirstOrDefault(p=>p.QuestionId==questionId);
            result.Answers = _db.Answers.Where(p => p.QuestionId == questionId).Include(p=>p.User).ToList();
            return result;
                }
    }
}
