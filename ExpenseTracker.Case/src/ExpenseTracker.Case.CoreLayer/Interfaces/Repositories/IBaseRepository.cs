using ExpenseTracker.Case.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Interfaces.Repositories
{
    public interface IBaseRepository <T> where T : class,IBaseEntity
    {
        //read
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllAsyncQueryable();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
       
        //write
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
