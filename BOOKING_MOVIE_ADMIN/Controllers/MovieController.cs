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

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : movieControllerBase
    {
        private readonly MovieServices _movie;
        private readonly UnitOfWork _unitOfWork;
        private readonly MovieActorServices _movieActor;
        private readonly MovieCategoriesServices _movieCategories;
        private readonly MovieDirectorServices _movieDirector;
        private readonly MovieCinemaServices _movieCinema;
        private readonly MovieRoomServices _movieRoom;
        private readonly MovieDateSettingServices _movieDateSetting;
        private readonly ActorServices _actor;
        private readonly CategoryServices _category;
        private readonly DirectorServices _director;

        public MovieController(
            MovieServices movieServices,
            UserServices userService,
            UnitOfWork unitOfWork,
            MovieActorServices movieActor,
            MovieCategoriesServices movieCategories,
            MovieDirectorServices movieDirector,
            MovieCinemaServices movieCinema,
            MovieRoomServices movieRoom,
            MovieDateSettingServices movieDateSetting,
            ActorServices actor,
            CategoryServices category,
            DirectorServices director
            ) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _movieActor = movieActor;
            _movieCategories = movieCategories;
            _movieDirector = movieDirector;
            _movie = movieServices;
            _movieCinema = movieCinema;
            _movieRoom = movieRoom;
            _movieDateSetting = movieDateSetting;
            _actor = actor;
            _category = category;
            _director = director;
        }

        [Authorize("User")]
        [HttpGet]
        public IActionResult GetMovieAllUser()
        {
            var data = _movie
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            return OkList(data);
        }
        
        [Authorize("User")]
        [HttpPost]
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
            
            if (body.MovieCategories.Count > 0)
            {
                var movieCategoryIds = body.MovieCategories.Select(e => e.CategoryId).ToList();
                var movieCategories = _category.GetAll().Where(e => movieCategoryIds.Contains(e.Id)).ToList();

                if (movieCategoryIds.Count() != movieCategories.Count)
                {
                    return BadRequest("CATEGORY_NOT_EXIST");
                }
            }
            
            if (body.MovieDirectors.Count > 0)
            {
                var movieDirectorIds = body.MovieDirectors.Select(e => e.DirectorId).ToList();
                var movieDirector = _category.GetAll().Where(e => movieDirectorIds.Contains(e.Id)).ToList();

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
                    Name = body.Name,
                    MovieStatus = body.MovieStatus,
                    YearOfRelease = body.YearOfRelease,
                    Time = body.Time,
                    Country = body.Country,
                    Rate = body.Rate,
                    Description = body.Description,
                    ReleaseDate = body.ReleaseDate,
                    PremiereDate = body.PremiereDate,
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

                var movieCategories = new List<MovieCategories>();
                if (body.MovieCategories.Count > 0) 
                {
                    movieCategories = body.MovieCategories.Select(e =>
                    {
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.MovieId = createMovie.Id;

                        return e;
                    }).ToList();
                }
            
                var movieDirector = new List<MovieDirector>();
                if (body.MovieCategories.Count > 0) 
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
                _movieCategories.AddRange(movieCategories);
                _movieDirector.AddRange(movieDirector);
                
                _movieDateSetting.CreateMovieDateSettings(body.MovieDateSettings.ToList(), createMovie.Id, CurrentUserEmail);
                
                transaction.Commit();
            }
            
            return Ok();
        }

        [Authorize("User")]
        [HttpDelete("{id}")]
        public IActionResult CreateMovieAllUser([FromRoute] long id,[FromBody] Movie body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieCheck = _movie.GetAll()
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

            if (movieCheck == null)
            {
                return BadRequest("MOVIE_NOT_EXIST");
            }

            body.Updated = DateTime.Now;
            body.UpdatedBy = CurrentUserEmail;
            body.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _movie.Update(body);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}