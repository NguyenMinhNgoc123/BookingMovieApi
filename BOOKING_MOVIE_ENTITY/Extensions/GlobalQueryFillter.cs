using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ENTITY.Extensions
{
    public static class GlobalQueryFilter
    {
        public static ModelBuilder BuildFillter(ModelBuilder builder)
        {
            builder.Entity<User>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Customer>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Movie>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Invoice>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<InvoiceDetails>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Genre>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Actor>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Cinema>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ComboFood>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Food>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<PaymentMethod>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Room>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Director>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Photo>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Video>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Promotion>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<MovieDirector>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<MovieActor>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<MovieCinema>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<MovieGenres>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<MovieRoom>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<MovieDateSetting>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<MovieTimeSetting>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Rate>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Combo>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Food>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<InvoicePayment>().HasQueryFilter(e => !e.Status.Equals("DELETED"));

            return builder;
        }
    }
}