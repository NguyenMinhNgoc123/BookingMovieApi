using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("")]
    [ApiController]
    public class MovieController : movieControllerBase
    {
        private readonly MovieServices _movie;
        private readonly UnitOfWork _unitOfWork;
        private readonly MovieActorServices _movieActor;
        private readonly MovieGenresServices _movieGenres;
        private readonly MovieDirectorServices _movieDirector;
        private readonly MovieCinemaServices _movieCinema;
        private readonly MovieRoomServices _movieRoom;
        private readonly MovieDateSettingServices _movieDateSetting;
        private readonly ActorServices _actor;
        private readonly GenreServices _genre;
        private readonly DirectorServices _director;
        private readonly PhotoServices _photo;
        private readonly VideoServices _video;
        private readonly InvoicesDetailServices _invoicesDetail;

        public MovieController(
            MovieServices movieServices,
            UserServices userService,
            UnitOfWork unitOfWork,
            MovieActorServices movieActor,
            MovieGenresServices movieGenres,
            MovieDirectorServices movieDirector,
            MovieCinemaServices movieCinema,
            MovieRoomServices movieRoom,
            MovieDateSettingServices movieDateSetting,
            ActorServices actor,
            GenreServices genre,
            DirectorServices director,
            PhotoServices photo,
            VideoServices video,
            InvoicesDetailServices invoicesDetail
        ) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _movieActor = movieActor;
            _movieGenres = movieGenres;
            _movieDirector = movieDirector;
            _movie = movieServices;
            _movieCinema = movieCinema;
            _movieRoom = movieRoom;
            _movieDateSetting = movieDateSetting;
            _actor = actor;
            _genre = genre;
            _director = director;
            _photo = photo;
            _video = video;
            _invoicesDetail = invoicesDetail;
        }

        [Authorize(Policy = "Customer")]
        [HttpGet("movieBooking")]
        public IActionResult GetMovieBooking([FromQuery] long? cinemaId)
        {
            var data = _movie
                .GetAll()
                .AsNoTracking();

            if (cinemaId != null)
            {
                data.Where(e => e.MovieDateSettings.Any(elm => elm.MovieCinemas.Any(o => o.CinemaId == cinemaId)));
            }
            
            var movies = data.OrderByDescending(e => e.Created).ToList();
            
            if (movies.Count > 0)
            {
                var movieIds = movies.Select(e => e.Id);
                var posterPhotos = _photo.GetAll()
                    .Where(e => movieIds.Contains(e.ObjectId))
                    .Where(e => e.Type == PHOTO.POSTER_MOVIE || e.Type == PHOTO.BACKDROP_MOVIE)
                    .ToList();

                if (posterPhotos.Count > 0)
                {
                    foreach (var elm in movies)
                    {
                        elm.PosterPhoto = posterPhotos.Where(e => e.Type == PHOTO.POSTER_MOVIE).FirstOrDefault(o => o.ObjectId == elm.Id);
                        elm.BackdropPhoto = posterPhotos.Where(e => e.Type == PHOTO.BACKDROP_MOVIE).FirstOrDefault(o => o.ObjectId == elm.Id);
                    }
                }
            }
            
            return OkList(movies);
        }
        
        [AllowAnonymous]
        [HttpGet("movie/{id}")]
        public IActionResult GetMovieDetail([FromRoute] long id)
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .Include(e => e.MovieActors)
                .ThenInclude(e => e.Actor)
                .Include(e => e.MovieGenres)
                .ThenInclude(e => e.Genre)
                .FirstOrDefault(e => e.Id == id);

            if (data != null)
            {
                var posterPhotos = _photo.GetAll()
                    .Where(e => e.ObjectId == data.Id)
                    .Where(e => e.Type == PHOTO.POSTER_MOVIE || e.Type == PHOTO.BACKDROP_MOVIE)
                    .ToList();

                if (posterPhotos.Count > 0)
                {
                    data.PosterPhoto = posterPhotos.Where(e => e.Type == PHOTO.POSTER_MOVIE).FirstOrDefault(o => o.ObjectId == data.Id);
                    data.BackdropPhoto = posterPhotos.Where(e => e.Type == PHOTO.BACKDROP_MOVIE).FirstOrDefault(o => o.ObjectId == data.Id);

                }
            }
            
            return Ok(data);
        }
        
        [AllowAnonymous]
        [HttpGet("movie/{id}/similar")]
        public IActionResult GetMovieDetailSimilar([FromRoute] long id)
        {
            var movieGenres = _movieGenres.GetAll()
                .Where(e => e.MovieId == id)
                .Include(e => e.Genre)
                .ToList();
            var genreIds = movieGenres.Select(e => e.GenreId).ToList();

            var data = _movie
                .GetAll()
                .Where(e => e.Id != id)
                .Where(e => e.MovieGenres.Any(o => genreIds.Contains(o.GenreId)))
                .AsNoTracking()
                .Include(e => e.MovieActors)
                .ThenInclude(e => e.Actor)
                .Include(e => e.MovieGenres)
                .ThenInclude(e => e.Genre)
                .ToList();

            if (data != null)
            {
                var movieIds = data.Select(e => e.Id);
                var posterPhotos = _photo.GetAll()
                    .Where(e => movieIds.Contains(e.ObjectId))
                    .Where(e => e.Type == PHOTO.POSTER_MOVIE || e.Type == PHOTO.BACKDROP_MOVIE)
                    .ToList();

                if (posterPhotos.Count > 0)
                {
                    foreach (var elm in data)
                    {
                        elm.PosterPhoto = posterPhotos.Where(e => e.Type == PHOTO.POSTER_MOVIE).FirstOrDefault(o => o.ObjectId == elm.Id);
                        elm.BackdropPhoto = posterPhotos.Where(e => e.Type == PHOTO.BACKDROP_MOVIE).FirstOrDefault(o => o.ObjectId == elm.Id);
                    }
                }
            }
            
            return OkList(data);
        }
        
        
        [AllowAnonymous]
        [HttpGet("movie/{id}/credits")]
        public IActionResult GetMovieDetailCredits([FromRoute] long id)
        {
            var movieGenres = _movieActor.GetAll()
                .Where(e => e.MovieId == id)
                .Include(e => e.Actor)
                .ToList();
            var actorIds = movieGenres.Select(e => e.ActorId).ToList();

            var data = _actor.GetAll().Where(e => actorIds.Contains(e.Id)).ToList();

            if (data.Count > 0)
            {
                var profilePhoto = _photo.GetAll()
                    .Where(e => actorIds.Contains(e.ObjectId))
                    .Where(e => e.Type == PHOTO.PROFILE_ACTOR)
                    .ToList();

                if (profilePhoto.Count > 0)
                {
                    foreach (var elm in data)
                    {
                        elm.ProfilePhoto = profilePhoto.FirstOrDefault(o => o.ObjectId == elm.Id);
                    }
                }
            }
            
            return OkList(data);
        }

        [AllowAnonymous]
        [HttpGet("movie/{id}/reviews")]
        public IActionResult GetMovieDetailReview([FromRoute] long id)
        {
            var movieGenres = _movieGenres.GetAll()
                .Where(e => e.MovieId == id)
                .Include(e => e.Genre)
                .ToList();
            var genreIds = movieGenres.Select(e => e.GenreId).ToList();

            var data = _movie
                .GetAll()
                .Where(e => e.Id != id)
                .Where(e => e.MovieGenres.Any(o => genreIds.Contains(o.GenreId)))
                .AsNoTracking()
                .Include(e => e.MovieActors)
                .ThenInclude(e => e.Actor)
                .Include(e => e.MovieGenres)
                .ThenInclude(e => e.Genre)
                .ToList();

            if (data != null)
            {
                var movieIds = data.Select(e => e.Id);
                var posterPhotos = _photo.GetAll()
                    .Where(e => movieIds.Contains(e.ObjectId))
                    .Where(e => e.Type == PHOTO.POSTER_MOVIE || e.Type == PHOTO.BACKDROP_MOVIE)
                    .ToList();

                if (posterPhotos.Count > 0)
                {
                    foreach (var elm in data)
                    {
                        elm.PosterPhoto = posterPhotos.Where(e => e.Type == PHOTO.POSTER_MOVIE).FirstOrDefault(o => o.ObjectId == elm.Id);
                        elm.BackdropPhoto = posterPhotos.Where(e => e.Type == PHOTO.BACKDROP_MOVIE).FirstOrDefault(o => o.ObjectId == elm.Id);
                    }
                }
            }
            
            return OkList(data);
        }
        
        [AllowAnonymous]
        [HttpGet("movie/{id}/videos")]
        public IActionResult GetMovieDetailVideo([FromRoute] long id)
        {
            var data = _video
                .GetAll()
                .Where(e => e.ObjectId == id)
                .Where(e => e.Type == VIDEO.MOVIE)
                .AsNoTracking()
                .ToList();

            return OkList(data);
        }

        [AllowAnonymous]
        [HttpGet("movie/popular")]
        public IActionResult GetMoviePopular()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            var movieIds = data.Select(e => e.Id);
            var posterPhotos = _photo.GetAll()
                .Where(e => movieIds.Contains(e.ObjectId))
                .Where(e => e.Type == PHOTO.POSTER_MOVIE)
                .ToList();

            if (posterPhotos.Count > 0)
            {
                foreach (var elm in data)
                {
                    elm.PosterPhoto = posterPhotos.FirstOrDefault(o => o.ObjectId == elm.Id);
                }
            }
            
            return OkList(data);
        }
        
        [AllowAnonymous]
        [HttpGet("trending/all/day")]
        public IActionResult GetMovieTrendingAllDay()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();
            
            _movie.AddPosterPhoto(data);

            return OkList(data);
        }
        
        [AllowAnonymous]
        [HttpGet("trending/movie/day")]
        public IActionResult GetMovieTrendingMovieDay()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .Include(e => e.MovieGenres)
                .ThenInclude(e => e.Genre)
                .OrderByDescending(e => e.Created)
                .ToList();
            
            var movieIds = data.Select(e => e.Id);
            var posterPhotos = _photo.GetAll()
                .Where(e => movieIds.Contains(e.ObjectId))
                .Where(e => e.Type == PHOTO.BACKDROP_MOVIE || e.Type == PHOTO.POSTER_MOVIE)
                .ToList();

            if (posterPhotos.Count > 0)
            {
                foreach (var elm in data)
                {
                    elm.BackdropPhoto = posterPhotos.Where(e => e.Type == PHOTO.BACKDROP_MOVIE).FirstOrDefault(o => o.ObjectId == elm.Id);
                    elm.PosterPhoto = posterPhotos.Where(e => e.Type == PHOTO.POSTER_MOVIE).FirstOrDefault(o => o.ObjectId == elm.Id);
                }
            }
            return OkList(data);
        }
        
        [AllowAnonymous]
        [HttpGet("movie/top_rated")]
        public IActionResult GetMovieTopRated()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Rate >= 8)
                .OrderByDescending(e => e.Created)
                .ToList();

            var movieIds = data.Select(e => e.Id);
            var posterPhotos = _photo.GetAll()
                .Where(e => movieIds.Contains(e.ObjectId))
                .Where(e => e.Type == PHOTO.POSTER_MOVIE)
                .ToList();

            if (posterPhotos.Count > 0)
            {
                foreach (var elm in data)
                {
                    elm.PosterPhoto = posterPhotos.FirstOrDefault(o => o.ObjectId == elm.Id);
                }
            }
            
            return OkList(data);
        }
        
        [AllowAnonymous]
        [HttpGet("movie/upcoming")]
        public IActionResult GetMovieUpcoming()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .Where(e => e.ReleaseDate >= DateTime.Now)
                .OrderByDescending(e => e.Created)
                .ToList();

            _movie.AddPosterPhoto(data);
            
            return OkList(data);
        }
        
        
        [AllowAnonymous]
        [HttpGet("tv/popular")]
        public IActionResult GetTvPopular()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            return OkList(data);
        }
        
        [AllowAnonymous]
        [HttpGet("tv/trending/all/day")]
        public IActionResult GetTvTrendingAllDay()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            return OkList(data);
        }
        
        [AllowAnonymous]
        [HttpGet("trending/tv/day")]
        public IActionResult GetTvTrendingMovieDay()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            return OkList(data);
        }
        
        [AllowAnonymous]
        [HttpGet("tv/on_the_air")]
        public IActionResult GetTvOnTheAir()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            return OkList(data);
        }
        
        [AllowAnonymous]
        [HttpGet("tv/top_rated")]
        public IActionResult GetTvTopRated()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            return OkList(data);
        }
        
        
        [AllowAnonymous]
        [HttpGet("movie/search/{keyword}")]
        public IActionResult GetSearchMulti([FromRoute] string keyword, [FromQuery] string query)
        {
            var movies = _movie
                .GetAll()
                .AsNoTracking();

            if (query != null)
            {
                movies = movies.Where(e => EF.Functions.Like(e.Name, $"%{query}%") ||
                                           EF.Functions.Like(e.Country, $"%{query}%") ||
                                           EF.Functions.Like(e.Description, $"%{query}%") ||
                                           e.MovieActors.Any(o => EF.Functions.Like(o.Actor.Name, $"%{query}%")) ||
                                           e.MovieDirectors.Any(elm =>
                                               EF.Functions.Like(elm.Director.Name, $"%{query}%")) ||
                                           e.MovieGenres.Any(elm => EF.Functions.Like(elm.Genre.Name, $"%{query}%"))
                );
            }

            if (keyword != "multi" && keyword != "keyword" && keyword != null)
            {
                movies = movies.Where(e => e.MovieGenres.Any(elm => elm.Genre.Id == Int64.Parse(keyword)));
            }

            var data = movies
                .OrderByDescending(e => e.Created)
                .ToList();
            
            var movieIds = data.Select(e => e.Id);
            var posterPhotos = _photo.GetAll()
                .Where(e => movieIds.Contains(e.ObjectId))  
                .Where(e => e.Type == PHOTO.POSTER_MOVIE)
                .ToList();

            if (posterPhotos.Count > 0)
            {
                foreach (var elm in data)
                {
                    elm.PosterPhoto = posterPhotos.FirstOrDefault(o => o.ObjectId == elm.Id);
                }
            }
            
            return OkList(data);
        }

        [AllowAnonymous]
        [HttpGet("movie/discover")]
        public IActionResult GetDiscoverMovie([FromQuery] string query, [FromQuery] string sortBy, [FromQuery] string startReleaseDate, [FromQuery] string withGenres)
        {
            var movies = _movie
                .GetAll()
                .AsNoTracking();

            if (startReleaseDate != null)
            {
                string inputFormat = "yyyy-MM-dd";
                DateTime parsedDate = DateTime.ParseExact(startReleaseDate, inputFormat, CultureInfo.InvariantCulture);

                movies = movies.Where(e => e.ReleaseDate >= parsedDate);
            }
            
            if (query != null)
            {
                movies = movies.Where(e => EF.Functions.Like(e.Name, $"%{query}%") ||
                                           EF.Functions.Like(e.Country, $"%{query}%") ||
                                           EF.Functions.Like(e.Description, $"%{query}%") ||
                                           e.MovieActors.Any(o => EF.Functions.Like(o.Actor.Name, $"%{query}%")) ||
                                           e.MovieDirectors.Any(elm =>
                                               EF.Functions.Like(elm.Director.Name, $"%{query}%")) ||
                                           e.MovieGenres.Any(elm => EF.Functions.Like(elm.Genre.Name, $"%{query}%"))
                );
            }
            
            if (withGenres != null)
            {
                string[] stringArray = withGenres.Split(',');

                long[] genresParams = Array.ConvertAll(stringArray, long.Parse);

                movies = movies.Where(e => e.MovieGenres.Any(o => genresParams.Contains(o.GenreId)));
            }

            
            if (sortBy == SORT_BY.POPULARITY)
            {
                var invoiceDetails = _invoicesDetail
                    .GetAll()
                    .GroupBy(detail => detail.MovieId)
                    .Select(group => new
                    {
                        MovieId = group.Key,
                        Numb = group.Count()
                    })
                    .OrderByDescending(item => item.Numb)
                    .ToList();

                var movieIdInvoiceDetails = invoiceDetails.Select(e => e.MovieId).ToList();
                movies = movies.OrderByDescending(e => movieIdInvoiceDetails.IndexOf(e.Id));
            }

            if (sortBy == SORT_BY.RATING)
            {
                movies = movies.OrderByDescending(e => e.Rate);
            }
            
            if (sortBy == SORT_BY.MOST_RECENT)
            {
                movies = movies.OrderByDescending(e => e.ReleaseDate);
            }

            if (sortBy == null)
            {
                movies = movies.OrderByDescending(e => e.Created);
            }
            
            var data = movies
                .ToList();
            
            var movieIds = data.Select(e => e.Id);
            var posterPhotos = _photo.GetAll()
                .Where(e => movieIds.Contains(e.ObjectId))
                .Where(e => e.Type == PHOTO.POSTER_MOVIE)
                .ToList();

            if (posterPhotos.Count > 0)
            {
                foreach (var elm in data)
                {
                    elm.PosterPhoto = posterPhotos.FirstOrDefault(o => o.ObjectId == elm.Id);
                }
            }
            
            return OkList(data);
        }
    }
}