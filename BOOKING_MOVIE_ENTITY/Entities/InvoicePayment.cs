using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class InvoicePayment : EntitieDate
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