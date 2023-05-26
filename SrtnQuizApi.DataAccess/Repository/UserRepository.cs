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
    public class UserRepository : Repository<ApplicationUser> , IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ApplicationUser user)
        {
            _db.Update(user);
        }
    }
}
