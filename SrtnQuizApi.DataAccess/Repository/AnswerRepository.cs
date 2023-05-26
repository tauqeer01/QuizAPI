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
    public class AnswerRepository : Repository<Answer> , IAnswerRepository
    {
        private readonly ApplicationDbContext _db;

        public AnswerRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Answer answer)
        {
            _db.Update(answer);
        }
    }
}
