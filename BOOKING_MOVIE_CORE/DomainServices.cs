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
        private readonly Repository<T> _repository;
        private readonly UnitOfWork _unitOfWork;

        protected DomainService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T Add(T entity, bool saveChange = false)
        {
            _repository.Add(entity, saveChange);
            return entity;
        }

        public List<T> AddRange(List<T> entity, bool saveChange = false)
        {
            _repository.AddRange(entity, saveChange);
            return entity;
        }

        public T Update(T entity, bool saveChange = false)
        {
            _repository.Update(entity, saveChange);
            return entity;
        }

        public List<T> UpdateRange(List<T> entity, bool saveChange = false)
        {
            _repository.UpdateRange(entity, saveChange);
            return entity;
        }

        public T Delete_HARD(T entity, bool saveChange = false)
        {
            _repository.Delete_HARD(entity, saveChange);
            return entity;
        }

        public List<T> DeleteRange_HARD(List<T> entity, bool saveChange = false)
        {
            _repository.DeleteRange_HARD(entity, saveChange);
            return entity;
        }

        public async Task<T> FindAsync(object id)
        {
            var entities = await _repository.FindAsync(id);
            return entities;
        }

        public T Find(params object[] keyValues)
        {
            var entities = _repository.Find(keyValues);
            return entities;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var entities = _repository.FindBy(predicate);
            return entities;
        }

        public long GetTempId(T entity)
        {
            return _repository.GetTempId(entity);
        }
    }

}