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
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<MovieActor> MovieActors { get; set; }
        public virtual DbSet<MovieGenres> MovieGenres { get; set; }
        public virtual DbSet<MovieDirector> MovieDirectors { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }
        public virtual DbSet<Video> Video { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<InvoicePayment> InvoicePayment { get; set; }
        public virtual DbSet<Cinema> Cinema { get; set; }
        public virtual DbSet<MovieCinema> MovieCinema { get; set; }
        public virtual DbSet<MovieDateSetting> MovieDateSettings { get; set; }
        public virtual DbSet<MovieRoom> MovieRooms { get; set; }
        public virtual DbSet<MovieTimeSetting> MovieTimeSetting { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<ComboFood> ComboFood { get; set; }
        public virtual DbSet<CustomerPromotion> CustomerPromotion { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }

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
            
            modelBuilder.Entity<MovieDateSetting>(entity =>
            {
                entity.HasMany(e => e.MovieCinemas)
                    .WithOne(e => e.MovieDateSetting);
                entity.ToTable("MovieDateSetting");
            });
            
            modelBuilder.Entity<MovieRoom>(entity =>
            {
                entity.HasMany(e => e.MovieTimeSettings);
                entity.ToTable("MovieRoom");
            });
            
            modelBuilder.Entity<MovieCinema>(entity =>
            {
                entity.HasMany(e => e.MovieRooms);
                entity.ToTable("MovieCinema");
            });
            
            modelBuilder.Entity<MovieRoom>(entity =>
            {
                entity.HasMany(e => e.MovieTimeSettings);
                entity.ToTable("MovieRoom");
            });
            
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasMany(e => e.InvoiceDetails);
                entity.HasMany(e => e.InvoicePayment);
                entity.ToTable("Invoice");
            });
        }
    }
}