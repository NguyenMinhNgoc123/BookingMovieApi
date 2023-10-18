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

        public MovieController(
            MovieServices movieServices,
            UserServices userService,
            UnitOfWork unitOfWork,
            MovieActorServices movieActor,
            MovieCategoriesServices movieCategories,
            MovieDirectorServices movieDirector,
            MovieCinemaServices movieCinema,
            MovieRoomServices movieRoom,
            MovieDateSettingServices movieDateSetting
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
            
            body.Created = DateTime.Now;
            body.CreatedBy = CurrentUserEmail;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _movie.Add(body);
                
                var movieActors = new List<MovieActor>();
                if (body.MovieActors.Count > 0)
                {
                    movieActors = body.MovieActors.Select(e =>
                    {
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.MovieId = body.Id;
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
                        e.MovieId = body.Id;

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
                        e.MovieId = body.Id;

                        return e;
                    }).ToList();
                }
                
                _movieActor.AddRange(movieActors);
                _movieCategories.AddRange(movieCategories);
                _movieDirector.AddRange(movieDirector);
                
                _movieDateSetting.CreateMovieDateSettings(body.MovieDateSettings.ToList(), CurrentUserEmail);
                
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