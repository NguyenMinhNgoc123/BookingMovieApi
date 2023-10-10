using System;
using Newtonsoft.Json;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class User
    {
        public long Id { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string Email { get; set; }
        
        public string Name { get; set; }
        
        [JsonIgnore]
        public string PasswordHash { get; set; }
        
        public string Status { get; set; }
        
        public long? PhotoId { get; set; }
        
        public string SecurityStamp { get; set; }

        public Boolean IsAdmin { get; set; }

        public string Type { get; set; }
        
        public string Mobile { get; set; }

        public bool? IsSalonRootUser { get; set; }
    }
}