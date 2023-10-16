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

            return builder;
        }
    }
}