using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.DataAccessLayer.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        protected readonly AppDbContext _appDbContext;
        public BaseRepository(AppDbContext appDbContext) => (_appDbContext) = (appDbContext);
        protected DbSet<T> _dbSet => _appDbContext.Set<T>();


        //read

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindByAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }



        //write
        public async Task<T> CreateAsync(T entity)
        {
            var response = await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
