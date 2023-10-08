using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOOKING_MOVIE_ENTITY.EntitieBases
{
    public class EntitieDate
    {
        public long Id { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Created { get; set; }

        public string Status { get; set; }
    }
}