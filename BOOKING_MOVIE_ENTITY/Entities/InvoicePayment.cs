using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class InvoicePayment : EntitieDate
    {
        public long InvoiceId { get; set; }

        public long InvoiceMethodId { get; set; }

        public decimal? Total { get; set; }

        public virtual PaymentMethod InvoiceMethod { get; set; }
    }
}