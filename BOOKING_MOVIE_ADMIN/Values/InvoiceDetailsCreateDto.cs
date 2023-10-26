using BOOKING_MOVIE_ENTITY.EntitieBases;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_ADMIN.Values
{
    public class InvoiceDetailsCreateDto : EntitieDate
    {
        public long? InvoiceId { get; set; }
        
        public long? MovieId { get; set; }
        
        public long? RoomId { get; set; }

        public long? CinemaId { get; set; }

        public long? MovieDateSettingId { get; set; }

        public long? MovieTimeSettingId { get; set; }
        
        public long? ObjectId { get; set; }

        public string ObjectName { get; set; }

        public string ObjectCode { get; set; }

        public decimal ObjectPrice { get; set; }

        public string DiscountUnit { get; set; }

        public decimal? DiscountValue { get; set; }

        public decimal Total { get; set; }

        public decimal Quantity { get; set; }

        public long? PromotionId { get; set; }
    }
}