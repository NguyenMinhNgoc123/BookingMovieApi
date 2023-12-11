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
            builder.Entity<Director>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Room>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Director>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Photo>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Video>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Promotion>().HasQueryFilter(e => !e.Status.Equals("DELETED"));

            return builder;
        }
    }
}