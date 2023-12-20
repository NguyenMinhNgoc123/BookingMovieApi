using BOOKING_MOVIE_ENTITY.EntitieBases;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_ADMIN.Values
{
    public class InvoicePaymentCreateDto : EntitieDate
    {
        public long InvoiceId { get; set; }

        public long InvoiceMethodId { get; set; }
        public decimal? Total { get; set; }
        
        public string NameAtm { get; set; }
        
        public string NumberAtm { get; set; }
        
        public string NameShortCutAtm { get; set; }
        
        public string NotePayment { get; set; }

        public virtual PaymentMethod InvoiceMethod { get; set; }
    }
}