using System;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieTimeSetting : EntitieDate
    {
        public TimeSpan Time { get; set; }
        
        public long MovieDateSettingId { get; set; }
        
        public long MovieRoomId { get; set; }
        
        public long MovieCinemaId { get; set; }

        public virtual MovieDateSetting MovieDateSetting { get; set; }
        
        public virtual MovieCinema MovieCinema { get; set; }

        public virtual MovieRoom MovieRoom { get; set; }
    }
}