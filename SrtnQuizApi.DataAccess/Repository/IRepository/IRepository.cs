using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
