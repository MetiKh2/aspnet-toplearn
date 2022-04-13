using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.FourmDTOs;
using TopLearn.DataLayer.Entities.Question;

namespace TopLearn.Core.Services.Interfaces
{
   public interface IFroumService
    {
        #region Question
        int AddQuestion(Question question);
        ShowQuestionVM ShowQuestion(int questionId);
        bool IsQuestionForUser(int questionId,long userId);
        bool IsExisQuestion(int id);
        IEnumerable<Question> GetQuestions(int? courseId,string filter="");
        #endregion

        #region Answer
        bool IsExisAnswer(int id);
        void AddIsCorrcetAnswer(int id);
        void AddAnswer(Answer answer);
        #endregion
    }
}
