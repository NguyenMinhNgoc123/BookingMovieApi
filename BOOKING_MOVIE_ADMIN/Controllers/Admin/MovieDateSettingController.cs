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
        private InvoiceServices _invoice;

        public MovieDateSettingController(
            UnitOfWork unitOfWork,
            MovieDateSettingServices movieDateSetting,
            InvoiceServices invoice,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _movieDateSetting = movieDateSetting;
            _invoice = invoice;
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

            var invoice = _invoice
                .GetAll()
                .Where(e => e.InvoiceDetails.Any(o => o.MovieDateSettingId == id))
                .Where(e => e.PaymentStatus == PAYMENT_STATUS.PAID)
                .FirstOrDefault();
            
            if (invoice != null)
            {
                return BadRequest("DATE_INVOICE_EXIST");
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