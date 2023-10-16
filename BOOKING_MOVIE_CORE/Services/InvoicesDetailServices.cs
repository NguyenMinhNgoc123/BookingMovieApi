using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class InvoicesDetailServices : GenericDomainService<InvoiceDetails>
    {
        public InvoicesDetailServices(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}