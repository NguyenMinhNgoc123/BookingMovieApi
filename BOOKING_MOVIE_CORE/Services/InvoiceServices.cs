using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class InvoiceServices : GenericDomainService<Invoice>
    {
        public InvoiceServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}