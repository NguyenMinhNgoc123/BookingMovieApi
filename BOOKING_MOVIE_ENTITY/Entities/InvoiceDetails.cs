using System.ComponentModel.DataAnnotations.Schema;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class InvoiceDetails : EntitieDate
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

        public bool? IsPaid { get; set; }

        public long? PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }
        
        public virtual Movie Movie { get; set; }
        
        public virtual Room Room { get; set; }
        
        public virtual Cinema Cinema { get; set; }
        
        public virtual MovieDateSetting MovieDateSetting { get; set; }
        
        public virtual MovieTimeSetting MovieTimeSetting { get; set; }

        public virtual Invoice Invoice { get; set; }
        
        [NotMapped]
        public virtual ComboFood ComboFood { get; set; }
        
        [NotMapped]
        public virtual Food Food { get; set; }
    }
}