using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BOOKING_MOVIE_ENTITY.EntitieBases;

namespace BOOKING_MOVIE_ENTITY.Entities
{
    public class MovieCinema : EntitieDate
    {
        public long MovieDateSettingId { get; set; }
        
        [Column("CinemaId")]
        public long CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; }

        public virtual MovieDateSetting MovieDateSetting { get; set; }
        
        public virtual ICollection<MovieRoom> MovieRooms { get; set; }
    }
}