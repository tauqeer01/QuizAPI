using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISubjectsRepository Subjects { get; }
        IQuizesRepository Quizes { get; }
        IUserRepository User { get; }
        IQuestionRepository Question { get; }
        IAnswerRepository Answer { get; }
        IQuizAttemptRepository QuizAttempt { get; }

        void Save();
    }
}
