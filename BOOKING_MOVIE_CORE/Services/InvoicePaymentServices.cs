using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class InvoicePaymentServices : ApplicationService<InvoicePayment>
    {
        public InvoicePaymentServices(GenericDomainService<InvoicePayment> genericDomainService) : base(genericDomainService)
        {
        }
    }
}