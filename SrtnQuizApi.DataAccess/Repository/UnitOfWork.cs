using SrtnQuizApi.DataAccess.Data;
using SrtnQuizApi.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Subjects = new SubjectsRepository(_db);
            Quizes = new QuizesRepository(_db);
            User = new UserRepository(_db); 
            Question= new QuestionRepository(_db);
            Answer=new AnswerRepository(_db);   
            QuizAttempt= new QuizAttemptRepository(_db);
        }

        public ICategoryRepository Category {get; private set;}

        public ISubjectsRepository Subjects { get; private set;}

        public IQuizesRepository Quizes { get; private set;}

        public IUserRepository User { get; private set; }

        public IQuestionRepository Question { get; private set; }

        public IAnswerRepository Answer { get; private set; }

        public IQuizAttemptRepository QuizAttempt { get; private set; }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
