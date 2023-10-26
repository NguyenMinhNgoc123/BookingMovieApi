using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class InvoiceServices : ApplicationService<Invoice>
    {
        public InvoiceServices(GenericDomainService<Invoice> genericDomainService) : base(genericDomainService)
        {
        }

        public decimal CalculateTotal(decimal totalAllDetailInvoice, decimal discountValue, string discountUnit)
        {
            decimal totalInvoice = 0;
            if (discountUnit != null)
            {
                decimal discountTotal = totalAllDetailInvoice / 100 * discountValue;
                totalInvoice = totalAllDetailInvoice - discountTotal;
            }

            return totalInvoice;
        }
    }
}