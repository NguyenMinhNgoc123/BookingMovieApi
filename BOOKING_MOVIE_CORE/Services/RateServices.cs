using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class RateServices : GenericDomainService<Rate>
    {
        public RateServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}