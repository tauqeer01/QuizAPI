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
    public class SubjectsRepository : Repository<Subjects> , ISubjectsRepository
    {
        private readonly ApplicationDbContext _db;

        public SubjectsRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Subjects subject)
        {
            _db.Update(subject);
        }
    }
}
