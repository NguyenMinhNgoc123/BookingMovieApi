using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class movie_context : DbContext
    {
        public movie_context(DbContextOptions<movie_context> options) : base(options)
        {
            
        }
        
        public virtual DbSet<User> User { get; set; }
        
        public void ResetTracker()
        {
            var entries = this.ChangeTracker.Entries().ToList();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}