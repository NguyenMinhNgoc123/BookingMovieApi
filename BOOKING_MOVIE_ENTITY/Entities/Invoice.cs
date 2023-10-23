using System;
using System.Collections.Generic;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Invoice : EntitieDate
    {
        public string Code { get; set; }

        public long? CustomerId { get; set; }
        
        public string DiscountUnit { get; set; }

        public decimal DiscountValue { get; set; }

        public bool? IsDisplay { get; set; }

        public string Note { get; set; }

        // Công thức: TotalDetails - DiscountTotal
        public decimal? Total { get; set; }

        public string NotePayment { get; set; }

        public string NoteArrangement { get; set; }

        public decimal? CashBack { get; set; }

        public decimal TotalDetails { get; set; }

        public string PaymentStatus { get; set; }

        public long? BookingId { get; set; }

        public decimal? PaidTotal { get; set; }

        public long? LoyaltyPoint { get; set; }

        // Giảm giá hóa đơn (không tính InvoiceDetails)
        public decimal? DiscountTotal { get; set; }

        public string Type { get; set; }

        public DateTime? PaidAt { get; set; }

        public long? PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }

        public virtual User Cashier { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }

        public virtual ICollection<InvoicePayment> InvoicePayment { get; set; }

    }
}