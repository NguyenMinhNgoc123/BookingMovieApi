using System.Linq;
using BOOKING_MOVIE_ENTITY.Extensions;
using BOOKING_MOVIE_ENTITY.Helper;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class movie_context : DbContext
    {
        public movie_context(DbContextOptions<movie_context> options) : base(options)
        {
            
        }
        
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<MovieActor> MovieActors { get; set; }
        public virtual DbSet<MovieCategories> MovieCategories { get; set; }
        public virtual DbSet<MovieDirector> MovieDirectors { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }
        public virtual DbSet<Video> Video { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<InvoicePayment> InvoicePayment { get; set; }

        public void ResetTracker()
        {
            var entries = this.ChangeTracker.Entries().ToList();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = GlobalQueryFilter.BuildFillter(modelBuilder);
        }
    }
}