using System;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BOOKING_MOVIE_ADMIN.Controllers.Admin
{
    [Route("Admin/MovieCinema")]
    [ApiController]
    public class MovieCinemaController : movieControllerBase
    {
        private UnitOfWork _unitOfWork;
        private MovieCinemaServices _movieCinema;
        public MovieCinemaController(
            UnitOfWork unitOfWork,
            MovieCinemaServices movieCinema,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _movieCinema = movieCinema;
        }
        
        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovieCinema([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieCinema = _movieCinema.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (movieCinema == null)
            {
                return BadRequest("MOVIE_CINEMA_NOT_EXIST");
            }

            movieCinema.Updated = DateTime.Now;
            movieCinema.UpdatedBy = CurrentUserEmail;
            movieCinema.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _movieCinema.Update(movieCinema);
                transaction.Commit();
            }

            return Ok();
        }
    }
}