using SrtnQuizApi.DataAccess.Data;
using SrtnQuizApi.DataAccess.Repository.IRepository;
using SrtnQuizApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.DataAccess.Repository
{
    public class QuizesRepository : Repository<Quiz> , IQuizesRepository
    {
        private readonly ApplicationDbContext _db;

        public QuizesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Quiz quiz)
        {
            _db.Update(quiz);
        }
    }
}
