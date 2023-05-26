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
    public class QuizAttemptRepository :Repository<QuizAttempt> , IQuizAttemptRepository
    {
        private readonly ApplicationDbContext _db;

        public QuizAttemptRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
    }
}
