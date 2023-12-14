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
    [Route("Admin/MovieDateSetting")]
    [ApiController]
    public class MovieDateSettingController : movieControllerBase
    {
        private UnitOfWork _unitOfWork;
        private MovieDateSettingServices _movieDateSetting;
        public MovieDateSettingController(
            UnitOfWork unitOfWork,
            MovieDateSettingServices movieDateSetting,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _movieDateSetting = movieDateSetting;
        }
        
        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovieDateSetting([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieDateSetting = _movieDateSetting.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (movieDateSetting == null)
            {
                return BadRequest("MOVIE_DATE_SETTING_NOT_EXIST");
            }

            movieDateSetting.Updated = DateTime.Now;
            movieDateSetting.UpdatedBy = CurrentUserEmail;
            movieDateSetting.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _movieDateSetting.Update(movieDateSetting);
                transaction.Commit();
            }

            return Ok();
        }
    }
}