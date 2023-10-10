using System;
using BOOKING_MOVIE_ENTITY.EntitieBases;
using Newtonsoft.Json;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class User : EntitieDate
    {
        public string CreatedBy { get; set; }
        
        public string Email { get; set; }
        
        public string Name { get; set; }
        
        [JsonIgnore]
        public string PasswordHash { get; set; }
        
        public string Status { get; set; }
        
        public long? PhotoId { get; set; }
        
        public Boolean IsAdmin { get; set; }

        public string Mobile { get; set; }
    }
}