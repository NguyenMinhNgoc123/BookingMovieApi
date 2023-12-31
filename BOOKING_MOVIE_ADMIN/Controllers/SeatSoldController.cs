using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("{Controller}")]
    [ApiController]
    public class SeatSoldController : movieControllerBase
    {
        private InvoicesDetailServices _invoicesDetail;
        private MovieServices _movie;

        public SeatSoldController(
            InvoicesDetailServices invoicesDetail,
            MovieServices movie,
            UserServices userService) : base(userService)
        {
            _invoicesDetail = invoicesDetail;
            _movie = movie;
        }  

        [HttpGet]
        public IActionResult GetSeatSold([FromQuery] long? movieId, [FromQuery] long? roomId, [FromQuery] long? movieDateSettingId,  [FromQuery] string time)
        {
            if (movieId == null || roomId == null || movieDateSettingId == null || time == null)
            {
                return OkList(new List<InvoiceDetails> { });
            }

            var data = _invoicesDetail
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Invoice.PaymentStatus == PAYMENT_STATUS.PAID || e.Invoice.PaymentStatus == PAYMENT_STATUS.WAITING_10_MINUTE)
                .Where(e => e.ObjectName == OBJECT_NAME_MOVIE.SEAT)
                .Where(e => e.MovieId == movieId)
                .Where(e => e.RoomId == roomId)
                .Where(e => e.MovieDateSettingId == movieDateSettingId)
                .Where(e => e.MovieTimeSetting.Time == time)
                .ToList();

            return OkList(data);
        }
    }
}