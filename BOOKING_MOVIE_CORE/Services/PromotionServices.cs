using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class PromotionServices : GenericDomainService<Promotion>
    {
        public PromotionServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}