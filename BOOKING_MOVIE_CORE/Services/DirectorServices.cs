using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class DirectorServices : GenericDomainService<Director>
    {
        public DirectorServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}