using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class MovieServices : ApplicationService<Movie>
    {
        private readonly PhotoServices _photo;

        public MovieServices(
            PhotoServices photo,
            GenericDomainService<Movie> genericDomainService) : base(genericDomainService)
        {
            _photo = photo;

        }
        
        public void AddPosterPhoto(List<Movie> movies)
        {
            var movieIds = movies.Select(e => e.Id);
            var posterPhotos = _photo.GetAll()
                .Where(e => movieIds.Contains(e.ObjectId))
                .Where(e => e.Type == PHOTO.POSTER_MOVIE)
                .ToList();

            if (posterPhotos.Count > 0)
            {
                foreach (var movie in movies)
                {
                    movie.PosterPhoto = posterPhotos.FirstOrDefault(o => o.ObjectId == movie.Id);
                }
            }
        }
    }
}