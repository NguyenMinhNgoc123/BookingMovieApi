using System.Linq;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace BOOKING_MOVIE_ENTITY
{
    public class UnitOfWork
    {
        public readonly movie_context Context;

        public UnitOfWork(movie_context context)
        {
            Context = context;
        }

        public Repository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(Context);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public bool CheckTrackedEntity<T>(T entity) where T: class {
            var trackedEntry = Context.Set<T>().Local.FirstOrDefault(o => o == entity);
            return trackedEntry != null;
        }

        public void ResetTracker() 
        {
            Context.ResetTracker();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }

}