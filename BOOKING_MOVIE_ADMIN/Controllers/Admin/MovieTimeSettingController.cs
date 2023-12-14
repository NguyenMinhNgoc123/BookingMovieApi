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
    [Route("Admin/MovieTimeSetting")]
    [ApiController]
    public class MovieTimeSettingController : movieControllerBase
    {
        private UnitOfWork _unitOfWork;
        private MovieTimeSettingServices _movieTimeSetting;
        
        public MovieTimeSettingController(
            UnitOfWork unitOfWork,
            MovieTimeSettingServices movieTimeSetting,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _movieTimeSetting = movieTimeSetting;
        }
        
        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovieTimeSetting([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieTimeSetting = _movieTimeSetting.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (movieTimeSetting == null)
            {
                return BadRequest("MOVIE_TIME_SETTING_NOT_EXIST");
            }

            movieTimeSetting.Updated = DateTime.Now;
            movieTimeSetting.UpdatedBy = CurrentUserEmail;
            movieTimeSetting.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _movieTimeSetting.Update(movieTimeSetting);
                transaction.Commit();
            }

            return Ok();
        }
    }
}