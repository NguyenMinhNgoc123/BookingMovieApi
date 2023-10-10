using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BOOKING_MOVIE_ENTITY;

namespace BOOKING_MOVIE_CORE
{
    public abstract class DomainService<T> where T : class
    {
        public readonly Repository<T> Repository;
        public readonly UnitOfWork _unitOfWork;

        public DomainService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Repository = _unitOfWork.GetRepository<T>();
        }

        public IQueryable<T> GetAll()
        {
            return Repository.GetAll();
        }

        public T Add(T entity, bool saveChange = false)
        {
            Repository.Add(entity, saveChange);
            return entity;
        }

        public List<T> AddRange(List<T> entity, bool saveChange = false)
        {
            Repository.AddRange(entity, saveChange);
            return entity;
        }

        public T Update(T entity, bool saveChange = false)
        {
            Repository.Update(entity, saveChange);
            return entity;
        }

        public List<T> UpdateRange(List<T> entity, bool saveChange = false)
        {
            Repository.UpdateRange(entity, saveChange);
            return entity;
        }

        public T Delete_HARD(T entity, bool saveChange = false)
        {
            Repository.Delete_HARD(entity, saveChange);
            return entity;
        }

        public List<T> DeleteRange_HARD(List<T> entity, bool saveChange = false)
        {
            Repository.DeleteRange_HARD(entity, saveChange);
            return entity;
        }

        public async Task<T> FindAsync(object id)
        {
            var entities = await Repository.FindAsync(id);
            return entities;
        }

        public T Find(params object[] keyValues)
        {
            var entities = Repository.Find(keyValues);
            return entities;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var entities = Repository.FindBy(predicate);
            return entities;
        }

        public long GetTempId(T entity)
        {
            return Repository.GetTempId(entity);
        }
    }

}