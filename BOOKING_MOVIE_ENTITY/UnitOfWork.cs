using System.Linq;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace BOOKING_MOVIE_ENTITY
{
    public class UnitOfWork
    {
        private readonly movie_context _context;

        public UnitOfWork(movie_context context)
        {
            _context = context;
        }

        public Repository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        // public bool CheckTrackedEntity<T>(T entity) where T: class {
        //     var trackedEntry = _context.Set<T>().Local.FirstOrDefault(o => o == entity);
        //     return trackedEntry != null;
        // }

        public void ResetTracker() 
        {
            _context.ResetTracker();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}