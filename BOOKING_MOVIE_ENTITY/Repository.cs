using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BOOKING_MOVIE_ENTITY
{
    public class Repository<T> where T : class
    {
        private readonly movie_context _context;

        public Repository(movie_context context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Find(params object[] keyValues)
        {
            var entity = _context.Set<T>().Find(keyValues);
            return entity;
        }

        public T Add(T entity, bool saveChange = false)
        {
            _context.Set<T>().Add(entity);
            if (saveChange)
            {
                _context.SaveChanges();
            }

            return entity;
        }

        public async Task<T> AddAsync(T entity, bool saveChange = false)
        {
            await _context.Set<T>().AddAsync(entity);
            if (saveChange)
            {
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        public List<T> AddRange(List<T> entity, bool saveChange = false)
        {
            _context.Set<T>().AddRange(entity);
            if (saveChange)
            {
                _context.SaveChanges();
            }

            return entity;
        }

        public T Update(T entity, bool saveChange = false)
        {
            _context.Set<T>().Update(entity);
            if (saveChange)
            {
                _context.SaveChanges();
            }

            return entity;
        }

        public List<T> UpdateRange(List<T> entity, bool saveChange = false)
        {
            _context.Set<T>().UpdateRange(entity);
            if (saveChange)
            {
                _context.SaveChanges();
            }

            return entity;
        }

        public T Delete_HARD(T entity, bool saveChange = false)
        {
            _context.Set<T>().Remove(entity);
            if (saveChange)
            {
                _context.SaveChanges();
            }

            return entity;
        }

        public List<T> DeleteRange_HARD(List<T> entity, bool saveChange = false)
        {
            _context.Set<T>().RemoveRange(entity);
            if (saveChange)
            {
                _context.SaveChanges();
            }

            return entity;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public long GetTempId(T entity)
        {
            return (long)_context.Entry(entity).Property("Id").CurrentValue;
        }

        public LocalView<T> Local()
        {
            return _context.Set<T>().Local;
        }

        public EntityEntry<T> Entry(T rs)
        {
            return _context.Entry(rs);
        }
    }
}