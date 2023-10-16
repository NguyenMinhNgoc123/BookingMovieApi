using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class PaymentMethod : EntitieDate
    {
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public long InvoiceId { get; set; }
    }
}