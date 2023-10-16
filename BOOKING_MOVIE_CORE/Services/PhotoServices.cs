using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class PhotoServices : GenericDomainService<Photo>
    {
        public PhotoServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}