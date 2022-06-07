using Core.Repositories;
using Core.Services;
using Core.UnıtOfWorks;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWork _unıtOfWork;

        public Service(IGenericRepository<T> genericRepository, IUnitOfWork unıtOfWork)
        {
            _genericRepository = genericRepository;
            _unıtOfWork = unıtOfWork;
        }


        public async Task<T> AddAsync(T entity)
        {
            await _genericRepository.AddAsync(entity);
            await _unıtOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _genericRepository.AddRangeAsync(entities);
            await _unıtOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _genericRepository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _genericRepository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasEntity = await _genericRepository.GetByIdAsync(id);

            if(hasEntity == null)
            {
                throw new ClientSideException($"{ typeof(T).Name} not found");
            }
            return hasEntity;
        }

        public async Task RemoveAsync(T entity)
        {
            _genericRepository.Remove(entity);
            await _unıtOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _genericRepository.RemoveRange(entities);
            await _unıtOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _genericRepository.Update(entity);
            await _unıtOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _genericRepository.Where(expression);
        }
    }
}
