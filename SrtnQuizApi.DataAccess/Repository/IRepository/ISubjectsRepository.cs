using SrtnQuizApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.DataAccess.Repository.IRepository
{
    public interface ISubjectsRepository : IRepository<Subjects>
    {
        void Update(Subjects subject);

        void Save();
    }
}
