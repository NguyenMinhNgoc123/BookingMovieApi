using System;
using System.Collections.Generic;
using BOOKING_MOVIE_ENTITY.EntitieBases;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_ADMIN.Values
{
    public class InvoiceCreateDto : EntitieDate
    {
        public string Code { get; set; }

        public long? CustomerId { get; set; }
        
        public string DiscountUnit { get; set; }

        public decimal? DiscountValue { get; set; }

        public string Note { get; set; }

        public string NotePayment { get; set; }

        public string NoteArrangement { get; set; }

        public decimal? CashBack { get; set; }

        public string PaymentStatus { get; set; }

        public long? BookingId { get; set; }

        public decimal? DiscountTotal { get; set; }

        public long? PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<InvoiceDetailsCreateDto> InvoiceDetails { get; set; }

        public virtual ICollection<InvoicePaymentCreateDto> InvoicePayment { get; set; }

    }
}