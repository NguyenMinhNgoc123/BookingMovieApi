using System;
using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ADMIN.Controllers.Admin
{
    [Route("Admin/Movie")]
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
        
        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult GetMovieAllUser()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .Include(e => e.MovieGenres)
                .ThenInclude(e => e.Genre)
                .Include(e => e.MovieActors)
                .ThenInclude(e => e.Actor)
                .Include(e => e.MovieDirectors)
                .ThenInclude(e => e.Director)
                .Include(e => e.MovieDateSettings)
                    .ThenInclude(e => e.MovieCinemas)
                    .ThenInclude(e => e.Cinema)
                .Include(e => e.MovieDateSettings)
                    .ThenInclude(e => e.MovieCinemas)
                    .ThenInclude(e => e.MovieRooms)
                    .ThenInclude(e => e.Room)
                .Include(e => e.MovieDateSettings)
                    .ThenInclude(e => e.MovieCinemas)
                    .ThenInclude(e => e.MovieRooms)
                    .ThenInclude(e => e.MovieTimeSettings)
                .OrderByDescending(e => e.Created)
                .ToList();
            
            if (data.Count > 0)
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
        
        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public IActionResult GetMovieDetail([FromRoute] long id)
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Include(e => e.MovieDateSettings)
                .ThenInclude(e => e.MovieCinemas)
                .ThenInclude(e => e.MovieRooms)
                .ThenInclude(e => e.MovieTimeSettings)
                .Include(e => e.MovieActors)
                .ThenInclude(e => e.Actor)
                .Include(e => e.MovieDirectors)
                .ThenInclude(e => e.Director)
                .Include(e => e.MovieGenres)
                .ThenInclude(e => e.Genre)
                .OrderByDescending(e => e.Created)
                .FirstOrDefault();
            
            if (data != null)
            {
                var posterPhotos = _photo.GetAll()
                    .Where(e => data.Id == e.ObjectId)
                    .Where(e => e.Type == PHOTO.POSTER_MOVIE || e.Type == PHOTO.BACKDROP_MOVIE)
                    .ToList();

                if (posterPhotos.Count > 0)
                {
                    data.PosterPhoto = posterPhotos.Where(e => e.Type == PHOTO.POSTER_MOVIE)
                        .FirstOrDefault(o => o.ObjectId == data.Id);
                    data.BackdropPhoto = posterPhotos.Where(e => e.Type == PHOTO.BACKDROP_MOVIE)
                        .FirstOrDefault(o => o.ObjectId == data.Id);
                }
            }
            
            return Ok(data);
        }

        [Authorize(Policy = "User")]
        [HttpPost()]
        public IActionResult CreateMovieAllUser([FromBody] Movie body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (body.MovieActors.Count > 0)
            {
                var movieActorIds = body.MovieActors.Select(e => e.ActorId).ToList();
                var movieActors = _actor.GetAll().Where(e => movieActorIds.Contains(e.Id)).ToList();

                if (movieActorIds.Count() != movieActors.Count)
                {
                    return BadRequest("ACTOR_NOT_EXIST");
                }
            }
            
            if (body.MovieGenres.Count > 0)
            {
                var movieCategoryIds = body.MovieGenres.Select(e => e.GenreId).ToList();
                var movieCategories = _genre.GetAll().Where(e => movieCategoryIds.Contains(e.Id)).ToList();

                if (movieCategoryIds.Count() != movieCategories.Count)
                {
                    return BadRequest("CATEGORY_NOT_EXIST");
                }
            }
            
            if (body.MovieDirectors.Count > 0)
            {
                var movieDirectorIds = body.MovieDirectors.Select(e => e.DirectorId).ToList();
                var movieDirector = _genre.GetAll().Where(e => movieDirectorIds.Contains(e.Id)).ToList();

                if (movieDirectorIds.Count() != movieDirector.Count)
                {
                    return BadRequest("MOVIE_ACTOR_NOT_EXIST");
                }
            }
            
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var createMovie = new Movie()
                {
                    Created = DateTime.Now,
                    CreatedBy = CurrentUserEmail,
                    Status = OBJECT_STATUS.ENABLE,
                    Name = body.Name,
                    Overview = body.Overview,
                    MovieStatus = body.StartDate <= DateTime.Now && body.EndDate >= DateTime.Now
                        ? MOVIE_STATUS.PREMIERING
                        : body.StartDate >= DateTime.Now
                            ? MOVIE_STATUS.COMING_SOON
                            : MOVIE_STATUS.EXPIRED,
                    YearOfRelease = body.YearOfRelease,
                    Time = body.Time,
                    Country = body.Country,
                    Rate = body.Rate,
                    Description = body.Description,
                    ReleaseDate = body.ReleaseDate,
                    PremiereDate = body.PremiereDate,
                    StartDate = body.StartDate,
                    EndDate = body.EndDate
                };
                
                _movie.Add(createMovie);
                
                var movieActors = new List<MovieActor>();
                if (body.MovieActors.Count > 0)
                {
                    movieActors = body.MovieActors.Select(e =>
                    {
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.MovieId = createMovie.Id;
                        return e;
                    }).ToList();
                }

                var movieCategories = new List<MovieGenres>();
                if (body.MovieGenres.Count > 0) 
                {
                    movieCategories = body.MovieGenres.Select(e =>
                    {
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.MovieId = createMovie.Id;

                        return e;
                    }).ToList();
                }
            
                var movieDirector = new List<MovieDirector>();
                if (body.MovieGenres.Count > 0) 
                {
                    movieDirector = body.MovieDirectors.Select(e =>
                    {
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.MovieId = createMovie.Id;

                        return e;
                    }).ToList();
                }
                
                var movieVideo = new List<Video>();
                if (body.Videos?.Count > 0)
                {
                    var objectId = createMovie.Id;
                    movieVideo = body.Videos.Select(e =>
                    {
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.ObjectId = objectId;
                        e.Type = "MOVIE";
                        e.Name = e.Name;
                        e.url = e.url;
                        e.Status = OBJECT_STATUS.ENABLE;
                        
                        return e;
                    }).ToList();
                }
                
                _movieActor.AddRange(movieActors);
                _movieGenres.AddRange(movieCategories);
                _movieDirector.AddRange(movieDirector);
                _video.AddRange(movieVideo);
                _movieDateSetting.CreateMovieDateSettings(body.MovieDateSettings.ToList(), createMovie.Id, CurrentUserEmail);
                
                transaction.Commit();
            }
            
            return Ok();
        }

        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovieAllUser([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieCheck = _movie
                .GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (movieCheck == null)
            {
                return BadRequest("MOVIE_NOT_EXIST");
            }

            movieCheck.Updated = DateTime.Now;
            movieCheck.UpdatedBy = CurrentUserEmail;
            movieCheck.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _movie.Update(movieCheck);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [HttpPut("updateImage/{id}")]
        [Authorize(Policy = "User")]
        public IActionResult UpdateImageCustomer([FromRoute] long id, [FromBody] Photo body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var movieExist = _movie.GetAll()
                .AsNoTracking()
                .Where(o => o.Id == id)
                .FirstOrDefault();

            if (movieExist == null)
            {
                return BadRequest("MOVIe_NOT_EXIST");
            }

            var oldImage = _photo.GetAll()
                .Where(e => e.ObjectId == id)
                .Where(e => e.Type == body.Type)
                .FirstOrDefault();
            
            body.Status = OBJECT_STATUS.ENABLE;
            body.Type = body.Type;
            body.url = body.url;
            body.ObjectId = id;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                if (oldImage != null)
                {
                    body.UpdatedBy = CurrentUserEmail;
                    body.Updated = DateTime.Now;
                    oldImage.Status = OBJECT_STATUS.DELETED;
                    _photo.Update(oldImage);
                }   
                else
                {
                    body.CreatedBy = CurrentUserEmail;
                    body.Created = DateTime.Now;
                }
                
                _photo.Update(body);

                transaction.Commit();
            }

            return Ok(body);
        }
        
        [HttpGet("updateStatusMovie")]
        [Authorize(Policy = "User")]
        public IActionResult UpdateChartMovieStatus()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var movieExist = _movie.GetAll()
                .OrderByDescending(e => e.Created)
                .ToList();

            var newMovies = movieExist.Select(e =>
            {
                if (e.StartDate <= DateTime.Now && e.EndDate >= DateTime.Now)
                {
                    if (e.MovieStatus != MOVIE_STATUS.PREMIERING)
                    {
                        e.Updated = DateTime.Now;
                        e.UpdatedBy = CurrentUserEmail;
                        e.MovieStatus = MOVIE_STATUS.PREMIERING;
                    }
                }

                if (e.EndDate <= DateTime.Now)
                {
                    if (e.MovieStatus != MOVIE_STATUS.EXPIRED)
                    {
                        e.Updated = DateTime.Now;
                        e.UpdatedBy = CurrentUserEmail;
                        e.MovieStatus = MOVIE_STATUS.EXPIRED;
                    }
                }
                
                return e;
            }).ToList();
            
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _movie.UpdateRange(newMovies);
                transaction.Commit();
            }

            return Ok();
        }
        
        
        [Authorize(Policy = "User")]
        [HttpPut("{id}")]
        public IActionResult UpdateMovieAllUser([FromRoute] long id, [FromBody] Movie body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = _movie.GetAll().Where(e => e.Id == id).FirstOrDefault();
            if (movie == null)
            {
                return BadRequest("MOVIE_NOT_EXIST");
            }
            
            if (body.MovieActors.Count > 0)
            {
                var movieActorIds = body.MovieActors.Select(e => e.ActorId).ToList();
                var movieActors = _actor.GetAll().Where(e => movieActorIds.Contains(e.Id)).ToList();

                if (movieActorIds.Count() != movieActors.Count)
                {
                    return BadRequest("ACTOR_NOT_EXIST");
                }
            }
            
            if (body.MovieGenres.Count > 0)
            {
                var movieCategoryIds = body.MovieGenres.Select(e => e.GenreId).ToList();
                var movieCategories = _genre.GetAll().Where(e => movieCategoryIds.Contains(e.Id)).ToList();

                if (movieCategoryIds.Count() != movieCategories.Count)
                {
                    return BadRequest("CATEGORY_NOT_EXIST");
                }
            }
            
            if (body.MovieDirectors.Count > 0)
            {
                var movieDirectorIds = body.MovieDirectors.Select(e => e.DirectorId).ToList();
                var movieDirector = _genre.GetAll().Where(e => movieDirectorIds.Contains(e.Id)).ToList();

                if (movieDirectorIds.Count() != movieDirector.Count)
                {
                    return BadRequest("MOVIE_ACTOR_NOT_EXIST");
                }
            }
            
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var createMovie = new Movie()
                {
                    Id = movie.Id,
                    Created = DateTime.Now,
                    CreatedBy = CurrentUserEmail,
                    Status = OBJECT_STATUS.ENABLE,
                    Name = body.Name,
                    Overview = body.Overview,
                    MovieStatus = body.StartDate <= DateTime.Now && body.EndDate >= DateTime.Now
                        ? MOVIE_STATUS.PREMIERING
                        : body.StartDate >= DateTime.Now
                            ? MOVIE_STATUS.COMING_SOON
                            : MOVIE_STATUS.EXPIRED,
                    YearOfRelease = body.YearOfRelease,
                    Time = body.Time,
                    Country = body.Country,
                    Rate = body.Rate,
                    Description = body.Description,
                    ReleaseDate = body.ReleaseDate,
                    PremiereDate = body.PremiereDate,
                    StartDate = body.StartDate,
                    EndDate = body.EndDate
                };
                
                _movie.Update(createMovie);

                var movieGenre = _movieGenres.GetAll().Where(e => e.MovieId == id).ToList();
                var oldMovieDirector = _movieDirector.GetAll().Where(e => e.MovieId == id).ToList();
                var movieActor = _movieActor.GetAll().Where(e => e.MovieId == id).ToList();
                
                foreach (var e in movieGenre)
                {
                    e.Status = OBJECT_STATUS.DELETED;
                    e.Updated = DateTime.Now;
                    e.UpdatedBy = CurrentUserEmail;
                }
                foreach (var e in oldMovieDirector)
                {
                    e.Status = OBJECT_STATUS.DELETED;
                    e.Updated = DateTime.Now;
                    e.UpdatedBy = CurrentUserEmail;
                }
                foreach (var e in movieActor)
                {
                    e.Status = OBJECT_STATUS.DELETED;
                    e.Updated = DateTime.Now;
                    e.UpdatedBy = CurrentUserEmail;
                }

                _movieGenres.UpdateRange(movieGenre);
                _movieDirector.UpdateRange(oldMovieDirector);
                _movieActor.UpdateRange(movieActor);

                var movieActors = new List<MovieActor>();
                if (body.MovieActors.Count > 0)
                {
                    movieActors = body.MovieActors.Select(e =>
                    {
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.MovieId = createMovie.Id;
                        return e;
                    }).ToList();
                }

                var movieCategories = new List<MovieGenres>();
                if (body.MovieGenres.Count > 0) 
                {
                    movieCategories = body.MovieGenres.Select(e =>
                    {
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.MovieId = createMovie.Id;

                        return e;
                    }).ToList();
                }
            
                var movieDirector = new List<MovieDirector>();
                if (body.MovieGenres.Count > 0) 
                {
                    movieDirector = body.MovieDirectors.Select(e =>
                    {
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.MovieId = createMovie.Id;

                        return e;
                    }).ToList();
                }
                
                _movieActor.AddRange(movieActors);
                _movieGenres.AddRange(movieCategories);
                _movieDirector.AddRange(movieDirector);
                _movieDateSetting.CreateMovieDateSettings(body.MovieDateSettings.ToList(), createMovie.Id, CurrentUserEmail);
                
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}