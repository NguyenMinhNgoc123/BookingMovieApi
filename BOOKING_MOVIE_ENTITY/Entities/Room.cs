using System;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Room : EntitieDate
    {
        public string Name { get; set; }
        
        public decimal Price { get; set; }
    }
}