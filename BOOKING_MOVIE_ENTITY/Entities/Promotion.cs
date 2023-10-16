using System;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Promotion : EntitieDate
    {
        public string Description { get; set; }

        public decimal DiscountValue { get; set; }

        public string DiscountUnit { get; set; }

        public long CustomerId { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }
    }
}