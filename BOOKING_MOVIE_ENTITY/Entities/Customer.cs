using System;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class Customer : EntitieDate
    {
        public string Address { get; set; }
        
        public string Email { get; set; }
        
        public string Mobile { get; set; }
        
        public string Name { get; set; }
        
        public string Note { get; set; }
        
        public string Sex { get; set; }
        
        public string Code { get; set; }
        
        public long CurrentLoyaltyPoint { get; set; } = 0;
        
        public string PasswordHash { get; set; }
    }
}