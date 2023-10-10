using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BOOKING_MOVIE_CORE
{
    public abstract class ApplicationService<T> where T : class
    {
        public readonly DomainService<T> _domainService;
        
        public ApplicationService(DomainService<T> domainService)
        {
            _domainService = domainService;
        }

        public IQueryable<T> GetAll()
        {
            return _domainService.GetAll();
        }

        public virtual T Add(T entity)
        {
            return _domainService.Add(entity, saveChange: true);
        }

        public virtual List<T> AddRange(List<T> entities)
        {
            return _domainService.AddRange(entities, saveChange: true);
        }

        public virtual T Update(T entity)
        {
            return _domainService.Update(entity, saveChange: true);
        }

        public virtual List<T> UpdateRange(List<T> entities)
        {
            return _domainService.UpdateRange(entities, saveChange: true);
        }

        public virtual T Remove_HARD(T entity)
        {
            return _domainService.Delete_HARD(entity, saveChange: true);
        }

        public virtual List<T> RemoveRange_HARD(List<T> entities)
        {
            return _domainService.DeleteRange_HARD(entities, saveChange: true);
        }
        
        public async Task<T> FindAsync(object id)
        {
            return await _domainService.FindAsync(id);
        }
        
        public T Find(params object[] keyValues)
        {
            return _domainService.Find(keyValues);
        }
        
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _domainService.FindBy(predicate);
        }
    }

}