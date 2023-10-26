using System;
using System.Runtime.Serialization;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieTimeSetting : EntitieDate
    {
        public string Time { get; set; }
        
        public decimal Price { get; set; }
        public long MovieRoomId { get; set; }
        
        public virtual MovieRoom MovieRoom { get; set; }
    }
}