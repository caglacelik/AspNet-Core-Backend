using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // readonly objelere sadece constructorda ya da definitionlarında değer atanabilir 
        // başka bir yerde set edilmesi -- compile error 
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            // AsNoTracking() çekilen dataları memorye almıyor 
            // Anlık durumları track etmiyor
            // Sadece data çekme fonk olduğundan tracking işlemine gerek yok
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            // remove async methodu yok ağır iş yok
            // entity of state -- deleted flag -- enum
            // _context.Entry(entity).State = EntityState.Deleted;
           
            _dbSet.Remove(entity); 
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            // where IQueryable döner
            return _dbSet.Where(expression);
        }
    }
}
