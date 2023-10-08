using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class movie_context : DbContext
    {
        public movie_context(DbContextOptions<movie_context> options) : base(options)
        {
        }
    }
}