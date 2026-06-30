using Microsoft.EntityFrameworkCore;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class GenericRepo <T> : IGenericRepo<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
           var res= await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return res.Entity;

        }

        public  bool DeleteAsync(T entity)
        {
            var res=dbContext.Set<T>().Remove(entity);
             dbContext.SaveChanges();
            return res.State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
           
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           var res= await dbContext.Set<T>().ToListAsync();
            await dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<IEnumerable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var res= await dbContext.Set<T>().Where(predicate).ToListAsync();
            await dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var res= await dbContext.Set<T>().FindAsync(id);
            return res;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State=EntityState.Modified;
            var res=  dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync();
            return res.Entity;
        }
    }
}
