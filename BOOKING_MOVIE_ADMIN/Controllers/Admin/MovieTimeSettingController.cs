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
        private InvoiceServices _invoice;
        public MovieTimeSettingController(
            UnitOfWork unitOfWork,
            MovieTimeSettingServices movieTimeSetting,
            InvoiceServices invoice,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _movieTimeSetting = movieTimeSetting;
            _invoice = invoice;
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
            
            var invoice = _invoice
                .GetAll()
                .Where(e => e.InvoiceDetails.Any(o => o.MovieTimeSettingId == id))
                .Where(e => e.PaymentStatus == PAYMENT_STATUS.PAID)
                .FirstOrDefault();
            
            if (invoice != null)
            {
                return BadRequest("TIME_INVOICE_EXIST");
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